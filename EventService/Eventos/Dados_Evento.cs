using System.Runtime.CompilerServices;
using EventService.Enderecos;

namespace EventService.Eventos
{
    public class Dados_Evento
    {
        public string nome;
        public string descricao;
        public Local local;
        public Artista artista;

        public Dados_Evento(string nome, string descricao, Artista artista, Local local)
        {
            this.nome = nome;
            this.descricao = descricao;
            this.artista = artista;
            this.local = local;
        }
    }
}
