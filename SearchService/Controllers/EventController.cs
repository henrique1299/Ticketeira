using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchService.Eventos;
using SearchService.Enderecos;
using SearchService.BancoDeDados;

namespace SearchService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        [HttpGet("{event_id}")]
        public string GetEvent(int event_id)
        {
            return PostgreDB.GetEventoById(event_id);
        }

        [HttpGet("search")]
        public async Task<string> GetEventByName([FromQuery] string? keyword, [FromQuery] string? start_date, [FromQuery] string? end_date)
        {
            return await PostgreDB.GetEventoByName(keyword);
        }

        [HttpGet]
        public string GetEvents()
        {
            return PostgreDB.GetEventos();
        }
    }
}
