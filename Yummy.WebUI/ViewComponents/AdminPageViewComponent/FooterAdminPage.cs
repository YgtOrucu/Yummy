using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class FooterAdminPage : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/AdminPageViewComponent/FooterAdminPage.cshtml");
        }
    }
}
