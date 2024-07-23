using Microsoft.AspNetCore.Mvc;
using SinemaSite.Models;
using Newtonsoft.Json;
using MySqlX.XDevAPI;

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
            ViewBag.Users = JsonConvert.SerializeObject(users);
            return View();
        }

        [SendUserInfo]
        [AdminOnly]
        public IActionResult AddSinema()
        {
            return View();
        }

        [SendUserInfo]
        [HttpPost]
        public IActionResult AddSinema(SinemaAddingModel sinema)
        {
            if (ModelState.IsValid ) 
            {
                var existingSinema = _db.Sinemas.Where(x => x.Isim == sinema.SinemaAdi || x.Adres == sinema.SinemaAdresi).FirstOrDefault();
                if (existingSinema == null)
                {
                    Sinema yeniSinema = new Sinema();
                    yeniSinema.Isim = sinema.SinemaAdi;
                    yeniSinema.Adres = sinema.SinemaAdresi;
                    yeniSinema.OlusturulmaTarihi = DateTime.Now;
                    _db.Sinemas.Add(yeniSinema);
                    _db.SaveChanges();
                    ViewBag.Success = "Sinema başarıyla eklendi.";
                    return View(sinema);
                }
                else
                {
                    ModelState.AddModelError("", "Bu sinema zaten mevcut");
                }
            }
            return View();
        }

        [SendUserInfo]
        [AdminOnly]
        public IActionResult AddSalon()
        {
            var sinemalar = _db.Sinemas.ToList();
            ViewBag.Sinemalar = sinemalar;
            return View();
        }

        [HttpPost]
        [SendUserInfo]
        [AdminOnly]
        public IActionResult AddSalon(SalonAddingModel salon)
        {
            var sinemalar = _db.Sinemas.ToList();
            ViewBag.Sinemalar = sinemalar;
            if (ModelState.IsValid)
            {
                var existingSalon = _db.Salons.Where(x => x.SinemaId == salon.SinemaId && x.SalonTipi == salon.SalonTipi && x.SalonNumarasi == salon.SalonNumarasi && x.ToplamKoltuk == salon.ToplamKoltuk).FirstOrDefault();
                if (existingSalon == null)
                {
                    Salon yeniSalon = new Salon();
                    yeniSalon.ToplamKoltuk = salon.ToplamKoltuk;
                    yeniSalon.SinemaId = salon.SinemaId;
                    yeniSalon.SalonNumarasi = salon.SalonNumarasi;
                    yeniSalon.SalonTipi = salon.SalonTipi;
                    yeniSalon.OlusturulmaTarihi = DateTime.Now;
                    _db.Salons.Add(yeniSalon);
                    _db.SaveChanges();
                    ViewBag.Success = "Salon başarıyla eklendi.";
                    return View(salon);
                }
                else
                {
                    ModelState.AddModelError("", "Bu salon zaten mevcut");
                }
            }
            return View();
        }

        [SendUserInfo]
        [AdminOnly]
        public IActionResult Salons()
        {
            var sinemalar = _db.Sinemas
                .Select(s => new
                {
                    s.Id,
                    s.Isim,
                    s.Adres,
                    s.OlusturulmaTarihi,
                    s.SilinmeTarihi,
                    s.GuncellemeTarihi,
                    Salons = s.Salons.Select(sa => new
                    {
                        sa.Id,
                        sa.ToplamKoltuk,
                        sa.SalonNumarasi,
                        sa.SalonTipi,
                        sa.OlusturulmaTarihi,
                        sa.SilinmeTarihi,
                        sa.GuncellemeTarihi,
                    }).ToList()
                })
                .ToList();
            ViewBag.Sinemalar = JsonConvert.SerializeObject(sinemalar);
            return View();
        }

        [HttpGet("GetUserInfo/{id}")]
        public IActionResult GetUserInfo(int id)
        {
            var user = _db.Kullanicis.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return Json(user);
        }
    }
}
