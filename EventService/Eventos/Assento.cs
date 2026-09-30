namespace EventService.Eventos
{
    public class Assento
    {

        public string Id;
        public string Codigo;
        public string Setor;

        public Assento(string Id, string Codigo, string Setor)
        {
            this.Id = Id;
            this.Codigo = Codigo;
            this.Setor = Setor;
        }
    }
}
