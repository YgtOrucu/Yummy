using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.FeatureDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class FeatureUIPage : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.GetAsync("Features");

            if (responseBody.IsSuccessStatusCode)
            {
                var value = await responseBody.Content.ReadFromJsonAsync<List<ResultFeatureDto>>();
                return View("~/Views/Shared/Components/UIPageViewComponent/FeatureUIPage.cshtml",value);
            }
            return View("~/Views/Shared/Components/UIPageViewComponent/FeatureUIPage.cshtml");
        }
    }
}
