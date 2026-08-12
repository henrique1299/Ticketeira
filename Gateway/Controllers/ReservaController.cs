using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservaController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<IActionResult> ReservarEvento([FromBody] Reserva reserva)
        {
            string url = "http://EventService:8080/reserva";

            HttpResponseMessage response = await client.PostAsJsonAsync(url, reserva);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return Ok(new { Mensagem = jsonResponse });
        }
    }
}
