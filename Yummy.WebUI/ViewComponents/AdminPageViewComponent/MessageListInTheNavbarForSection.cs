using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.MessageDto.MessageDtoForAdminThema.MessageListForNavbarSection;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class MessageListInTheNavbarForSection : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageListInTheNavbarForSection(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responsemessage = await client.GetAsync("Messages/MessageListByIsReadFalse");
            if (responsemessage.IsSuccessStatusCode)
            {
                var JsonData = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<MessageListByIsReadFalseDto>>(JsonData);
                return View("~/Views/Shared/Components/AdminPageViewComponent/MessageListInTheNavbarForSection.cshtml", values);
            }
            return View("~/Views/Shared/Components/AdminPageViewComponent/MessageListInTheNavbarForSection.cshtml");
        }
    }
}
