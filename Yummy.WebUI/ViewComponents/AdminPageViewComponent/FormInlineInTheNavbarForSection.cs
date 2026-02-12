using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class FormInlineInTheNavbarForSection : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/AdminPageViewComponent/FormInlineInTheNavbarForSection.cshtml");
        }
    }
}
