

using System.Runtime.ConstrainedExecution;

namespace Gateway
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

    public class Evento
    { 
        public int event_id { get; private set; }
        public string nomeEvento { get; set; }
        public string descricaoEvento { get; set; }
        public string nomeArtista { get; set; }
        public string descricaoArtista { get; set; }

        public string nomeLocal { get; set; }
        public int capacidadeLocal { get; set; }

        public string rua { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string UF { get; set; }
        public string pais { get; set; }
        public string cep { get; set; }



    }
}
