using Microsoft.AspNetCore.Mvc;
using EventService.Eventos;
using EventService.Clientes;
using EventService.Pagamentos;

namespace EventService.Eventos
{
    public class Ingresso
    {
        public DateTime data_reserva;
        public Cliente cliente;
        public Assento assento;
        public Evento evento;
        public Pagamento pagamento;

        public Ingresso()
        {

        }
    }
}
