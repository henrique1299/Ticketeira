using Microsoft.AspNetCore.Mvc;
using EventService.Eventos;
using EventService.BancoDeDados;

namespace EventService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public string InserirIngresso([FromBody] Ingresso ingresso)
        {

            //var resultado = PostgreDB.InserirIngresso(ingresso);

            return "EventService return inserir ingresso";
        }
    }
}
