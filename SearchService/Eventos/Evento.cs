using SearchService.Enderecos;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace SearchService.Eventos
{
    public class Evento
    {
        public int Id { get; set; }
        public Dados_Evento dados { get; set; }

        public Evento() { }

        public Evento(int Id, string nome, string descricao, DateTime data, Artista artista, Local local)
        {
            this.Id = Id;
            this.dados = new Dados_Evento(nome, descricao, artista, local);
        }

        public static Evento get_evento(int Id)
        {

            return null;
        }

    }
    public class EventoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }

    public class EventoDocument
    {
        [JsonPropertyName("after")]
        public EventoAfter After { get; set; }

        [JsonPropertyName("op")]
        public string Op { get; set; }
    }

    public class EventoAfter
    {
        [JsonPropertyName("idevento")]
        public int IdEvento { get; set; }

        [JsonPropertyName("nomeevento")]
        public string NomeEvento { get; set; }

        [JsonPropertyName("descricaoevento")]
        public string DescricaoEvento { get; set; }

        [JsonPropertyName("nomeartista")]
        public string NomeArtista { get; set; }

        [JsonPropertyName("descricaoartista")]
        public string DescricaoArtista { get; set; }

        [JsonPropertyName("nomelocal")]
        public string NomeLocal { get; set; }

        public EventoAfter() { }
    }
}
