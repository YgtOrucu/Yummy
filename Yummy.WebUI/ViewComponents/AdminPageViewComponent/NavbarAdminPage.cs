using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class NavbarAdminPage : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/AdminPageViewComponent/NavbarAdminPage.cshtml");
        }
    }
}
