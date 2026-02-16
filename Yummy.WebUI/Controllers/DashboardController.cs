using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult DashboardView()
        {
            return View();
        }
    }
}
