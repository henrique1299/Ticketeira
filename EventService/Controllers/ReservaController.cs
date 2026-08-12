using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventService.Eventos;
using EventService.Enderecos;
using EventService.BancoDeDados;

namespace EventService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        [HttpPost]
        public string GetEvent([FromBody] Reserva reserva)
        {
            return "EventService return Reserva";
        }

    }
}
