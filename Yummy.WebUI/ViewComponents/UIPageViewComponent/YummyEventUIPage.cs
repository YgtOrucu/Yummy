using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.YummyEventsDto;

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
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.GetAsync("YummyEvents");

            if (responseBody.IsSuccessStatusCode)
            {
                var values = await responseBody.Content.ReadFromJsonAsync<List<ResultYummyEventDto>>();
                return View("~/Views/Shared/Components/UIPageViewComponent/YummyEventUIPage.cshtml", values);
            }
            return View("~/Views/Shared/Components/UIPageViewComponent/YummyEventUIPage.cshtml");
        }
    }
}
