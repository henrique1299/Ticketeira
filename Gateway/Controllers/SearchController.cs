using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<IActionResult> BuscarEventos([FromQuery] string keyword, [FromQuery] string? start_date, [FromQuery] string? end_date)
        {
            string url = "http://SearchService:8080/events/search?keyword=" + keyword+"&start_date="+start_date+"&end_date="+end_date;

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return Ok(new { Mensagem = jsonResponse });
        }
    }
}
