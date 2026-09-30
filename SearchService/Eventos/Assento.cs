namespace SearchService.Eventos
{
    public class Assento
    {

        private string Id;
        private string Codigo;
        private string Setor;

        public Assento() { }

        public Assento(string Id, string Codigo, string Setor)
        {
            this.Id = Id;
            this.Codigo = Codigo;
            this.Setor = Setor;
        }
    }
}
