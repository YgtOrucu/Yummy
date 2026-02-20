using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.ContactDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class ContactUIPage : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactUIPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.GetAsync("Contacts");

            if (responseBody.IsSuccessStatusCode)
            {
                var values = await responseBody.Content.ReadFromJsonAsync<List<ResultContactDto>>();
                return View("~/Views/Shared/Components/UIPageViewComponent/ContactUIPage.cshtml", values);
            }
            return View("~/Views/Shared/Components/UIPageViewComponent/ContactUIPage.cshtml");
        }
    }
}
