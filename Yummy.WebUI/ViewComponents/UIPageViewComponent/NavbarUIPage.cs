using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class NavbarUIPage : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/UIPageViewComponent/NavbarUIPage.cshtml");
        }
    }
}
