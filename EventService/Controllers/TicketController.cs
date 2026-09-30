using Microsoft.AspNetCore.Mvc;
using EventService.Eventos;
using EventService.BancoDeDados;

namespace EventService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {

        [HttpPost]
        public async Task<string> GerarIngresso([FromBody] Ticket ticket)
        {
            await RedisDB.GerarIngresso(ticket);

            return "Ticket Gerado.";
        }
    }
}
