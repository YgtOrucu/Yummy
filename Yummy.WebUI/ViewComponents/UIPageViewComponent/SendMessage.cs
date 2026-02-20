using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.MessageDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class SendMessage : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new CreateMessageDto();
            return View("~/Views/Shared/Components/UIPageViewComponent/SendMessage.cshtml", model);
        }
    }
}
