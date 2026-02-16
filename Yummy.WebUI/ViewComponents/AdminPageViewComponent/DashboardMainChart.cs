using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.DashboardDto;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class DashboardMainChart : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardMainChart(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Dashboards/GetDashboardStatistics");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<ResultMainChartDto>();
                return View("~/Views/Shared/Components/AdminPageViewComponent/DashboardMainChart.cshtml", values);
            }
            return View("~/Views/Shared/Components/AdminPageViewComponent/DashboardMainChart.cshtml");
        }
    }
}
