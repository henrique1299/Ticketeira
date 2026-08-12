namespace EventService.Eventos
{
    public class Artista
    {

        private string nome;
        private string descricao;

        public Artista(string nome, string descricao)
        {
            this.nome = nome;
            this.descricao = descricao;
        }
    }

    public class ArtistaDto
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }   
}
