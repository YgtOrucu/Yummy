using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class TestimonialUIPage:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Views/Shared/Components/UIPageViewComponent/TestimonialUIPage.cshtml");
        }
    }

}
