using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class ReservationUIPage : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/UIPageViewComponent/ReservationUIPage.cshtml");
        }
    }
}
