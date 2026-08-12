using System.Runtime.CompilerServices;
using SearchService.Enderecos;

namespace SearchService.Eventos
{
    public class Dados_Evento
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public Local local { get; set; }
        public Artista artista { get; set; }

        public Dados_Evento(string nome, string descricao, Artista artista, Local local)
        {
            this.nome = nome;
            this.descricao = descricao;
            this.artista = artista;
            this.local = local;
        }
    }
}
