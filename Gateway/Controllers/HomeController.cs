using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<IActionResult> BuscarEventos()
        {
            string url = "http://SearchService:8080/event";

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return Ok(new { Mensagem = jsonResponse });
        }
    }
}
