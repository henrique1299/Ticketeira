using Dapper;
using Npgsql;
using SearchService.Enderecos;
using SearchService.Eventos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using StackExchange.Redis;

namespace SearchService.BancoDeDados
{
    public class PostgreDB
    {
        private static readonly string _connectionString = "Host=postgres_db;Port=5432;Database=ticketeira_db;Username=postgres;Password=sua_senha";
        private static NpgsqlConnection connect() => new NpgsqlConnection(_connectionString);

        private static readonly Lazy<ConnectionMultiplexer> LazyConnection =
        new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("redis:6379");
        });

        public static ConnectionMultiplexer RedisConnection => LazyConnection.Value;

        public PostgreDB()
        {

        }

        public static string GetEventoById(int event_id)
        {
            IDatabase dbRedis = RedisConnection.GetDatabase();

            string valorChave = dbRedis.StringGet(event_id.ToString());

            if (!string.IsNullOrEmpty(valorChave))
            {
                return "Redis: " + valorChave;
            }

            using var connection = connect();

            string sql = @"
                SELECT 
                    eventos.id AS Id, 
                    eventos.nome AS Nome, 
                    eventos.descricao AS Descricao, 
                    TO_CHAR(eventos.data, 'YYYY-MM-DD') AS Data,
                    artista.id AS Id,
                    artista.nome AS Nome, 
                    artista.descricao AS Descricao,
                    local.id AS Id,
                    local.nome AS Nome, 
                    local.cidade AS Cidade
                FROM shows eventos
                INNER JOIN Artistas artista ON eventos.artista = artista.id
                INNER JOIN Locais local ON eventos.local = local.id
                WHERE eventos.id = @Id
            ";

            var resultado = connection.Query<EventoDto, ArtistaDto, LocalDto, Evento>(
                sql,
                (eventoDto, artistaDto, localDto) =>
                {
                    return new Evento(
                        eventoDto.Id, 
                        eventoDto.Nome, 
                        eventoDto.Descricao,
                        eventoDto.Data, 
                        new Artista(artistaDto.Nome, artistaDto.Descricao), 
                        new Local(
                            localDto.Id, 
                            localDto.Nome, 
                            new Endereco(
                                localDto.Logradouro, 
                                localDto.Numero, 
                                localDto.Cidade,
                                localDto.Estado, 
                                localDto.Pais, 
                                new CEP(localDto.Cep)
                            ), 
                            localDto.Capacidade
                        )
                    );
                },
                new { Id = event_id },
                splitOn: "Id,Id"
            );

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string jsonString = JsonSerializer.Serialize(resultado, options);

            dbRedis.StringSet(event_id.ToString(), jsonString);

            return jsonString;
        }

        public static string GetEventoByName(string keyword = "")
        {

            using var dbConnection = connect();

            string sql = @"
                 SELECT 
                    eventos.id AS Id, 
                    eventos.nome AS Nome, 
                    eventos.descricao AS Descricao, 
                    TO_CHAR(eventos.data, 'YYYY-MM-DD') AS Data,
                    artista.id AS Id,
                    artista.nome AS Nome, 
                    artista.descricao AS Descricao,
                    local.id AS Id,
                    local.nome AS Nome, 
                    local.cidade AS Cidade
                FROM eventos eventos
                INNER JOIN Artistas artista ON eventos.artista = artista.id
                INNER JOIN Locais local ON eventos.local = local.id
                WHERE eventos.nome ILIKE @Keyword
            ";

            var resultado = dbConnection.Query<EventoDto, ArtistaDto, LocalDto, Evento>(
                sql,
                (eventoDto, artistaDto, localDto) =>
                {
                    return new Evento(
                        eventoDto.Id,
                        eventoDto.Nome,
                        eventoDto.Descricao,
                        eventoDto.Data,
                        new Artista(artistaDto.Nome, artistaDto.Descricao),
                        new Local(
                            localDto.Id,
                            localDto.Nome,
                            new Endereco(
                                localDto.Logradouro,
                                localDto.Numero,
                                localDto.Cidade,
                                localDto.Estado,
                                localDto.Pais,
                                new CEP(localDto.Cep)
                            ),
                            localDto.Capacidade
                        )
                    );
                },
                new { Keyword = $"%{keyword}%" },
                splitOn: "Id,Id"
            );

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string jsonString = JsonSerializer.Serialize(resultado, options);

            return jsonString;

        }

        public static string GetEventos()
        {
            using var connection = connect();

            string sql = @"
                SELECT 
                    eventos.id AS Id, 
                    eventos.nome AS Nome, 
                    eventos.descricao AS Descricao, 
                    TO_CHAR(eventos.data, 'YYYY-MM-DD') AS Data,
                    artista.id AS Id,
                    artista.nome AS Nome, 
                    artista.descricao AS Descricao,
                    local.id AS Id,
                    local.nome AS Nome, 
                    local.cidade AS Cidade
                FROM eventos eventos
                INNER JOIN Artistas artista ON eventos.artista = artista.id
                INNER JOIN Locais local ON eventos.local = local.id
            ";

            var resultado = connection.Query<EventoDto, ArtistaDto, LocalDto, Evento>(
                sql,
                (eventoDto, artistaDto, localDto) =>
                {
                    return new Evento(
                        eventoDto.Id,
                        eventoDto.Nome,
                        eventoDto.Descricao,
                        eventoDto.Data,
                        new Artista(artistaDto.Nome, artistaDto.Descricao),
                        new Local(
                            localDto.Id,
                            localDto.Nome,
                            new Endereco(
                                localDto.Logradouro,
                                localDto.Numero,
                                localDto.Cidade,
                                localDto.Estado,
                                localDto.Pais,
                                new CEP(localDto.Cep)
                            ),
                            localDto.Capacidade
                        )
                    );
                },
                splitOn: "Id,Id"
            );

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string jsonString = JsonSerializer.Serialize(resultado, options);

            return jsonString;
        }
    
    }
}
