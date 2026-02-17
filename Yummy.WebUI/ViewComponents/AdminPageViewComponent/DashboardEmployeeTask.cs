using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.EmployeeTaskDto;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class DashboardEmployeeTask : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardEmployeeTask(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("EmployeeTasks");
            if(responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<List<ResultEmployeeTaskDto>>();
                return View("~/Views/Shared/Components/AdminPageViewComponent/DashboardEmployeeTask.cshtml", value);
            }
            return View("~/Views/Shared/Components/AdminPageViewComponent/DashboardEmployeeTask.cshtml");
        }
    }
}
