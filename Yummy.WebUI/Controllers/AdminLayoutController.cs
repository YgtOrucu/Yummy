using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult AdminLayout()
        {
            return View();
        }
    }
}
