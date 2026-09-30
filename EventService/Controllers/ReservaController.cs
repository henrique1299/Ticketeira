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
        public async Task<string> ReservarIngresso([FromBody] Reserva reserva)
        {
            foreach (var assento in reserva.assentos)
            {
                string key = $"reserva:{reserva.evento_id}:{reserva.cliente_id}:{assento}";

                if (await RedisDB.ChecarReservas(key))
                    return "Erro. Assento já reservado.";
            }

            Reserva_Json reserva_json = new Reserva_Json
            {
                cliente_id = reserva.cliente_id,
                evento_id = reserva.evento_id
            };
            foreach (var assento in reserva.assentos)
            {
                string key = $"reserva:{reserva.evento_id}:{reserva.cliente_id}:{assento}";
                
                reserva_json.assento = assento;

                await RedisDB.ReservarIngresso(reserva_json, key);
            }
            return "";
        }

    }
}
