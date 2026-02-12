using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class SidebarAdminPage : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/AdminPageViewComponent/SidebarAdminPage.cshtml");
        }
    }
}
