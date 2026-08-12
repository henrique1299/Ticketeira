using Dapper;
using Npgsql;
using EventService.Enderecos;
using EventService.Eventos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using StackExchange.Redis;

namespace EventService.BancoDeDados
{
    public class PostgreDB
    {
        private static readonly string _connectionString = "Host=postgres_db;Port=5432;Database=EventService_db;Username=postgres;Password=sua_senha";
        private static NpgsqlConnection connect() => new NpgsqlConnection(_connectionString);

        public PostgreDB()
        {

        }

        public static string InserirIngresso(Ingresso ingresso)
        {
            using var connection = connect();

            string sql = @"
                INSERT INTO Ingressos (data, cliente, codigo, setor, show)
                VALUES (@Data, @Cliente, @Codigo, @Setor, @Show)
            ";

            var resultado = connection.Query(
                sql,
                new { Data = ingresso.data_reserva, Cliente = ingresso.cliente.dados.nome, Codigo = ingresso.assento.Codigo, Setor = ingresso.assento.Setor, Show = ingresso.evento.dados.nome }
            );

            string jsonString = JsonSerializer.Serialize(resultado);

            return jsonString;
        }

        public static string GetEventoByName(string nome = "")
        {
            using (var redis = ConnectionMultiplexer.Connect("redis:6379"))
            {
                IDatabase db = redis.GetDatabase();

                db.StringSet("chaveTest", "Olá do C# com Redis!");

                string valor = db.StringGet("chaveTest");
            }

            Console.ReadKey();

            return "";
        }
    }
}
