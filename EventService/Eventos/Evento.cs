using System.Runtime.CompilerServices;
using EventService.Enderecos;

namespace EventService.Eventos
{
    public class Evento
    {
        public int Id;
        public Dados_Evento dados;

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
}
