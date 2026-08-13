using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;

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
            string url = "http://SearchService:8080/event/" + event_id;

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return Ok(new { Mensagem = jsonResponse });
        }

        [HttpPost]
        public async Task<IActionResult> CriarEvento([FromBody] Evento evento)
        {
            string url = "http://EventService:8080/event";

            var json = JsonSerializer.Serialize(evento);
            var jsonContent = new StringContent(json, Encoding.UTF8);
            jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync(url, jsonContent);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
        
            return Ok(new { Mensagem = jsonResponse });
        }

        [HttpPut("{event_id}")]
        public async Task<IActionResult> AtualizarEvento([FromRoute] int event_id, [FromBody] Evento evento)
        {
            string url = "http://EventService:8080/event/" + event_id;
            var json = JsonSerializer.Serialize(evento);
            var jsonContent = new StringContent(json, Encoding.UTF8);
            jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PutAsync(url, jsonContent);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return Ok(new { Mensagem = jsonResponse });
        }

        [HttpDelete("{event_id}")]
        public async Task<IActionResult> ExcluirEvento([FromRoute] int event_id)
        {
            string url = "http://EventService:8080/event/" + event_id;

            HttpResponseMessage response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return Ok(new { Mensagem = jsonResponse });
        }
    }
}
