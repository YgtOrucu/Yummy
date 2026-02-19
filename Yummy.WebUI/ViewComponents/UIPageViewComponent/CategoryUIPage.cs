using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.CategoryDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class CategoryUIPage : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.GetAsync("Categories");

            if (responseBody.IsSuccessStatusCode)
            {
                var value = await responseBody.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();
                return View("~/Views/Shared/Components/UIPageViewComponent/CategoryUIPage.cshtml", value);
            }
            return View("~/Views/Shared/Components/UIPageViewComponent/CategoryUIPage.cshtml");
        }
    }
}
