using Microsoft.AspNetCore.Mvc;

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
            return View("~/Views/Shared/Components/UIPageViewComponent/ChefUIPage.cshtml");
        }
    }
}
