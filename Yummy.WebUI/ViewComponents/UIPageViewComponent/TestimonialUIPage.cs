using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.TestimonialDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class TestimonialUIPage : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.GetAsync("Testimonials");

            if (responseBody.IsSuccessStatusCode)
            {
                var value = await responseBody.Content.ReadFromJsonAsync<List<ResultTestimonialDto>>();
                return View("~/Views/Shared/Components/UIPageViewComponent/TestimonialUIPage.cshtml", value);
            }
            return View("~/Views/Shared/Components/UIPageViewComponent/TestimonialUIPage.cshtml");
        }
    }

}
