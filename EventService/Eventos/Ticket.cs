

namespace EventService.Eventos
{
    public class Ticket
    {
        public DateTime data { get; set; }

        public string cliente_id { get; set; }

        public string codigo { get; set; }

        public string setor { get; set; }

        public string evento_id { get; set; }

    }

    public class Reserva
    {
        public string evento_id { get; set; }

        public string cliente_id { get; set; }


        public List<string> assentos { get; set; }


    }

    public class Reserva_Json
    {
        public string evento_id { get; set; }

        public string cliente_id { get; set; }

        public string assento { get; set; }

    }
}
