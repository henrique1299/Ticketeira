namespace EventService.Eventos
{
    public class Assento
    {

        public int Id;
        public int Codigo;
        public int Setor;

        public Assento(int Id, int Codigo, int Setor)
        {
            this.Id = Id;
            this.Codigo = Codigo;
            this.Setor = Setor;
        }
    }
}
