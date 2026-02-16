using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.DashboardDto;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class DashboardWidgets : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardWidgets(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Dashboards");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<ResultStatisticalDataDto>();
                return View("~/Views/Shared/Components/AdminPageViewComponent/DashboardWidgets.cshtml", values);
            }
            return View("~/Views/Shared/Components/AdminPageViewComponent/DashboardWidgets.cshtml");
        }
    }
}
