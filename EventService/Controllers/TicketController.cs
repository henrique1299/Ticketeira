using Microsoft.AspNetCore.Mvc;
using EventService.Eventos;

namespace EventService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {

        [HttpPost]
        public string GetEvent([FromBody] Ticket ticket)
        {
            return "EventService return Ticket";
        }
    }
}
