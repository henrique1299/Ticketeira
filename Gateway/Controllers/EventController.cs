using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet("{event_id}")]
        public async Task<IActionResult> BuscarEvento([FromRoute] int event_id)
        {
            string url = "http://SearchService:8080/events/" + event_id;

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return Ok(new { Mensagem = jsonResponse });
        }
    }
}
