using Microsoft.AspNetCore.Mvc;
using EventService.Eventos;
using EventService.BancoDeDados;

namespace EventService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        [HttpPost]
        public string InserirEvento([FromBody] Evento evento)
        {

            //var resultado = PostgreDB.InserirIngresso(ingresso);

            return "EventService return inserir ingresso";
        }

        [HttpPut("{event_id}")]
        public string AtualizarEvento([FromRoute] int event_id, [FromBody] Evento evento)
        {

            //var resultado = PostgreDB.AtualizarIngresso(ingresso);

            return "EventService return atualizar ingresso";
        }

        [HttpDelete("{event_id}")]
        public string DeletarEvento([FromRoute] int event_id)
        {
            //var resultado = PostgreDB.DeletarIngresso(ingresso);
            return "EventService return deletar ingresso";
        }

    }
}
