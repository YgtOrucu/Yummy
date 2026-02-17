using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.DashboardDto;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class DashboardGetChefs : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardGetChefs(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Dashboards/ResultGetChef");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultGetChef>>();
                return View("~/Views/Shared/Components/AdminPageViewComponent/DashboardGetChefs.cshtml", values);
            }
            return View("~/Views/Shared/Components/AdminPageViewComponent/DashboardGetChefs.cshtml");
        }
    }
}
