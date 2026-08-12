using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<IActionResult> ReservarEvento([FromBody] Ticket ticket)
        {
            string url = "http://EventService:8080/ticket";

            HttpResponseMessage response = await client.PostAsJsonAsync(url, ticket);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return Ok(new { Mensagem = jsonResponse });
        }
    }
}
