namespace SearchService.Eventos
{
    public class Artista
    {

        public string nome { get; set; }
        public string descricao { get; set; }

        public Artista() { }
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
