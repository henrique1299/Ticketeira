using Dapper;
using Npgsql;
using EventService.Enderecos;
using EventService.Eventos;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks;
using EventService.Clientes;

namespace EventService.BancoDeDados
{
    public class RedisDB
    {

       private static readonly string _connectionString = "Host=postgres;Port=5432;Database=ticketeira_db;Username=postgres;Password=pF*vitEA0z*xZ-ENF91a";

        private static NpgsqlConnection connect() => new NpgsqlConnection(_connectionString);

        private static readonly Lazy<ConnectionMultiplexer> LazyConnection =
        new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("redis:6379");
        });

        public static ConnectionMultiplexer RedisConnection => LazyConnection.Value;
        public RedisDB()
        {

        }

        public static async Task<bool> ReservarIngresso(Reserva_Json reserva_json, string key)
        {
            IDatabase dbRedis = RedisConnection.GetDatabase();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await dbRedis.StringSetAsync(key, JsonSerializer.Serialize(reserva_json, options), TimeSpan.FromMinutes(10));
            
            return false;
        }

        public static async Task<bool> ChecarReservas(string key)
        {
            IDatabase dbRedis = RedisConnection.GetDatabase();

            var endpoint = RedisConnection.GetEndPoints()[0];
            var server = RedisConnection.GetServer(endpoint);

            string evento_id = key.Split(":")[1];
            string cliente_id = key.Split(":")[2];
            string assento = key.Split(":")[3];

            var pattern = $"reserva:{evento_id}:*";
            var keys = server.Keys(pattern: pattern).ToArray();

            if (keys.Length == 0) 
                return false;
            
            RedisValue[] values = await dbRedis.StringGetAsync(keys);

            foreach (var val in values)
            {
                var reserva = JsonSerializer.Deserialize<Reserva_Json>(val.ToString());
                if (reserva == null)
                    continue;

                if (reserva.assento == assento && reserva.cliente_id != cliente_id)
                    return true;
            }
            
            return false;
        }
    
        public static async Task<bool> GerarIngresso(Ticket ticket)
        {
            using var connection = connect();

            string sql = @"
                INSERT INTO ingressos(data, cliente, codigo, setor, evento) 
                VALUES(@data, @cliente_id, @codigo, @setor, @evento_id)
            ";

            var resultado = await connection.ExecuteAsync(sql, ticket);
            
            return true;
        }
    }
}
