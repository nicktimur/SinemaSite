using Microsoft.AspNetCore.Mvc;
using SinemaSite.Models;

namespace SinemaSite.Controllers
{

    [AdminOnly]
    public class AdminController : Controller
    {


        private readonly CinemadbContext _db;

        public AdminController(CinemadbContext db)
        {
            _db = db;
        }

        [SendUserInfo]
        [AdminOnly]
        public IActionResult Index()
        {
            return View();
        }

        [SendUserInfo]
        [AdminOnly]
        public IActionResult Settings()
        {
            var users = _db.Kullanicis.ToList();
            ViewBag.Users = users;
            return View();
        }

    }
}
