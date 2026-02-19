using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.HeroDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class HeroUIPage : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HeroUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.GetAsync("Heroes");

            if (responseBody.IsSuccessStatusCode)
            {
                var value = await responseBody.Content.ReadFromJsonAsync<List<ResultHeroDto>>();
                return View("~/Views/Shared/Components/UIPageViewComponent/HeroUIPage.cshtml", value);
            }
            return View("~/Views/Shared/Components/UIPageViewComponent/HeroUIPage.cshtml");
        }
    }
}
