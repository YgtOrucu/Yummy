using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.UIPageViewComponent
{
    public class GalleryUIPage : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/UIPageViewComponent/GalleryUIPage.cshtml");
        }
    }
}
