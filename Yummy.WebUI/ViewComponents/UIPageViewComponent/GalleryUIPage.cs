using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.GalleryDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class GalleryUIPage : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GalleryUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.GetAsync("Galleries");

            if (responseBody.IsSuccessStatusCode)
            {
                var values = await responseBody.Content.ReadFromJsonAsync<List<ResultGalleryDto>>();
                return View("~/Views/Shared/Components/UIPageViewComponent/GalleryUIPage.cshtml", values);
            }
            return View("~/Views/Shared/Components/UIPageViewComponent/GalleryUIPage.cshtml");
        }
    }
}
