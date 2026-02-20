using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.ChefDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class ChefUIPage : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChefUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.GetAsync("Chefs");

            if (responseBody.IsSuccessStatusCode)
            {
                var values = await responseBody.Content.ReadFromJsonAsync<List<ResultChefDto>>();
                return View("~/Views/Shared/Components/UIPageViewComponent/ChefUIPage.cshtml", values);
            }
            return View("~/Views/Shared/Components/UIPageViewComponent/ChefUIPage.cshtml");
        }
    }
}
