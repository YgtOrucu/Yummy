using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.HeroDto;

namespace Yummy.WebUI.Controllers
{
    public class HeroController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HeroController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> HeroList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7287/api/Heroes");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultHeroDto>>();
                    return View(values);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }
    }
}
