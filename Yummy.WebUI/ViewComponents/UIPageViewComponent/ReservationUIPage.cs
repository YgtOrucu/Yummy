using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.ReservationDto;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class ReservationUIPage : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new CreateReservationDto();
            return View("~/Views/Shared/Components/UIPageViewComponent/ReservationUIPage.cshtml", model);
        }
    }
}
