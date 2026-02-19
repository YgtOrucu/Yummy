using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.Controllers
{
    public class YummyHomePageController : Controller
    {
        public IActionResult YummyHomePage()
        {
            return View();
        }
    }
}
