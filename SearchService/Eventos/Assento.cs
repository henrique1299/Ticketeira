namespace SearchService.Eventos
{
    public class Assento
    {

        private int Id;
        private int Codigo;
        private int Setor;

        public Assento() { }

        public Assento(int Id, int Codigo, int Setor)
        {
            this.Id = Id;
            this.Codigo = Codigo;
            this.Setor = Setor;
        }
    }
}
