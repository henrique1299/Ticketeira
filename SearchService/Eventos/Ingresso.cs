using Microsoft.AspNetCore.Mvc;
using SearchService.Eventos;
using SearchService.Clientes;
using SearchService.Pagamentos;

namespace SearchService
{
    public class Ingresso
    {
        private DateTime data_reserva;
        private Cliente cliente;
        private Assento assento;
        private Evento evento;
        private Pagamento pagamento;

        public Ingresso()
        {

        }
    }
}
