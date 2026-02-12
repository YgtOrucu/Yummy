using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class HeadAdminPage : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/AdminPageViewComponent/HeadAdminPage.cshtml");
        }
    }
}
