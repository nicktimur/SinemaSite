using Microsoft.AspNetCore.Mvc;

namespace SinemaSite.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
