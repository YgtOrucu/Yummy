using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.AboutDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class AboutUIPage : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.GetAsync("Abouts");

            if (responseBody.IsSuccessStatusCode)
            {
                var value = await responseBody.Content.ReadFromJsonAsync<List<ResultAboutDto>>();
                return View("~/Views/Shared/Components/UIPageViewComponent/AboutUIPage.cshtml", value);
            }
            return View("~/Views/Shared/Components/UIPageViewComponent/AboutUIPage.cshtml");
        }
    }
}
