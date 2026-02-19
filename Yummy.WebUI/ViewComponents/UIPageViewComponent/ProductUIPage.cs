using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.ProductDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class ProductUIPage : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.GetAsync("Products");

            if (responseBody.IsSuccessStatusCode)
            {
                var value = await responseBody.Content.ReadFromJsonAsync<List<ResultProductDto>>();
                return View("~/Views/Shared/Components/UIPageViewComponent/ProductUIPage.cshtml",value);
            }
            return View("~/Views/Shared/Components/UIPageViewComponent/ProductUIPage.cshtml");
        }
    }
}
