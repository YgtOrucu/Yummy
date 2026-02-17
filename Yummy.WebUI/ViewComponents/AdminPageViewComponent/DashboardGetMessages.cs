using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.DashboardDto;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class DashboardGetMessages : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardGetMessages(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Dashboards/ResultGetMessage");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultGetMessage>>();
                return View("~/Views/Shared/Components/AdminPageViewComponent/DashboardGetMessages.cshtml", values);
            }
            return View("~/Views/Shared/Components/AdminPageViewComponent/DashboardGetMessages.cshtml");
        }
    }
}
