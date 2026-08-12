

namespace EventService.Eventos
{
    public class Ticket
    {
        public int event_id { get; set; }

        public List<string> tickets { get; set; }

        public int user_id { get; set; }

        public List<string> payment_details{ get; set; }
    }

    public class Reserva
    {
        public int event_id { get; set; }

        public List<string> tickets { get; set; }

        public int user_id { get; set; }

    }
}
