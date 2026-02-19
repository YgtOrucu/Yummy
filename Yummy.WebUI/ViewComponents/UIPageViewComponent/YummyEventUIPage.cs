using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class YummyEventUIPage : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public YummyEventUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Views/Shared/Components/UIPageViewComponent/YummyEventUIPage.cshtml");
        }
    }
}
