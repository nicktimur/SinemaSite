using Microsoft.AspNetCore.Mvc;
using SinemaSite.Models;
using Newtonsoft.Json;
using MySqlX.XDevAPI;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        [SendUserInfo]
        [AdminOnly]
        public IActionResult Settings(AdminUserEditViewModel kullanici)
        {
            var users = _db.Kullanicis.ToList();
            ViewBag.Users = JsonConvert.SerializeObject(users);
            var user = _db.Kullanicis.Where(x => x.Id == kullanici.Id).FirstOrDefault();
            if (user is not null)
            {
                try
                {
                    user.KullaniciAdi = kullanici.KullaniciAdi ?? user.KullaniciAdi;
                    user.Isim = kullanici.Isim ?? user.Isim;
                    user.Soyisim = kullanici.Soyisim ?? user.Soyisim;
                    user.Email = kullanici.Email ?? user.Email;
                    user.KullaniciTipi = kullanici.KullaniciTipi ?? user.KullaniciTipi;
                    user.AktifMi = kullanici.AktifMi ?? user.AktifMi;
                    user.GuncellemeTarihi = DateTime.Now;

                    _db.SaveChanges();

                    users = _db.Kullanicis.ToList();
                    ViewBag.Users = JsonConvert.SerializeObject(users);
                    var userJson = HttpContext.Session.GetString("user");
                    var userdata = JsonConvert.DeserializeObject<Kullanici>(userJson);
                    var currentUser = _db.Kullanicis.Where(x => x.KullaniciAdi == userdata.KullaniciAdi).FirstOrDefault();
                    if (currentUser.Id == user.Id)
                    {
                        HttpContext.Session.Clear();
                        userJson = JsonConvert.SerializeObject(user);
                        HttpContext.Session.SetString("user", userJson);
                    }
                    return View(user);
                }
                catch
                {
                    ViewBag.Error = "Lütfen kullanılmayan bir mail veya kullanıcı adı giriniz.";
                    return View();
                }
            }
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
            if (ModelState.IsValid)
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
                var existingSalon = _db.Salons.Where(x => x.SinemaId == salon.SinemaId && x.SalonTipi == salon.SalonTipi && x.SalonNumarasi == salon.SalonNumarasi && x.Satir == salon.Satir && x.Sutun == salon.Sutun).FirstOrDefault();
                if (existingSalon == null)
                {
                    Salon yeniSalon = new Salon();
                    yeniSalon.Satir = salon.Satir;
                    yeniSalon.Sutun = salon.Sutun;
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
                        sa.Satir,
                        sa.Sutun,
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

        [HttpPost]
        [SendUserInfo]
        [AdminOnly]
        public IActionResult Salons(SalonEditModel salon)
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
                        sa.Satir,
                        sa.Sutun,
                        sa.SalonNumarasi,
                        sa.SalonTipi,
                        sa.OlusturulmaTarihi,
                        sa.SilinmeTarihi,
                        sa.GuncellemeTarihi,
                    }).ToList()
                })
                .ToList();
            ViewBag.Sinemalar = JsonConvert.SerializeObject(sinemalar);
            var sinema = _db.Sinemas
                    .Where(s => s.Salons.Any(sa => sa.Id == salon.SalonId)).FirstOrDefault(); // Sinema içerisinde verilen salon id'si var mı kontrol et
                    
            if (sinema is not null)
            {
                try
                {
                    sinema.Isim = salon.SinemaAdi ?? sinema.Isim;
                    sinema.Adres = salon.SinemaAdres ?? sinema.Adres;
                    var düzenlenenSalon = _db.Salons.Where(sa => sa.Id == salon.SalonId).FirstOrDefault();
                    düzenlenenSalon.SalonNumarasi = salon.SalonNumarasi ?? düzenlenenSalon.SalonNumarasi;
                    düzenlenenSalon.Satir = salon.Satir ?? düzenlenenSalon.Satir;
                    düzenlenenSalon.Sutun = salon.Sutun ?? düzenlenenSalon.Sutun;
                    var benzerSalon = _db.Salons
                        .Where(sa => sa.SalonNumarasi == düzenlenenSalon.SalonNumarasi
                                     && sa.Satir == düzenlenenSalon.Satir
                                     && sa.Sutun == düzenlenenSalon.Sutun
                                     && sa.Id != düzenlenenSalon.Id
                                     && sa.SilinmeTarihi == null)
                        .FirstOrDefault();

                    var benzerSinema = _db.Sinemas
                        .Where(s => s.Isim == sinema.Isim
                                    && s.Adres == sinema.Adres
                                    && s.Id != sinema.Id
                                    && s.SilinmeTarihi == null)
                        .FirstOrDefault();

                    if(benzerSalon is not null || benzerSinema is not null)
                    {
                        ViewBag.Error = "Bu salon zaten mevcut. Düzenlemeler kaydedilmedi.";
                    }
                    else
                    {
                        _db.SaveChanges();
                        sinemalar = _db.Sinemas
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
                                    sa.Satir,
                                    sa.Sutun,
                                    sa.SalonNumarasi,
                                    sa.SalonTipi,
                                    sa.OlusturulmaTarihi,
                                    sa.SilinmeTarihi,
                                    sa.GuncellemeTarihi,
                                }).ToList()
                            })
                            .ToList();
                        ViewBag.Sinemalar = JsonConvert.SerializeObject(sinemalar);
                    }



                    return View();
                }
                catch
                {
                    ViewBag.Error = "Bir hata oluştu.";
                    return View();
                }
            }
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

        [HttpGet("GetSinemaInfo/{id}")]
        public IActionResult GetSinemaInfo(int id)
        {
            var sinema = _db.Sinemas
                    .Where(s => s.Salons.Any(sa => sa.Id == id)) // Sinema içerisinde verilen salon id'si var mı kontrol et
                    .Select(s => new
                    {
                        s.Id,
                        s.Isim,
                        s.Adres,
                        s.OlusturulmaTarihi,
                        s.SilinmeTarihi,
                        s.GuncellemeTarihi,
                        // Yalnızca eşleşen salonu seç
                        Salons = s.Salons
                            .Where(sa => sa.Id == id)
                            .Select(sa => new
                            {
                                sa.Id,
                                sa.Satir,
                                sa.Sutun,
                                sa.SalonNumarasi,
                                sa.SalonTipi,
                                sa.OlusturulmaTarihi,
                                sa.SilinmeTarihi,
                                sa.GuncellemeTarihi,
                            })
                            .ToList()
                    })
                    .FirstOrDefault();
            if (sinema == null)
            {
                return NotFound();
            }
            return Json(sinema);
        }

        [AdminOnly]
        [SendUserInfo]
        [HttpPost]
        public IActionResult DeleteUser(AdminUserEditViewModel kullanici)
        {
            var user = _db.Kullanicis.Where(u => u.Id == kullanici.Id).FirstOrDefault();
            user.SilinmeTarihi = DateTime.Now;
            user.AktifMi = false;
            _db.SaveChanges();
            var userJson = HttpContext.Session.GetString("user");
            var userdata = JsonConvert.DeserializeObject<Kullanici>(userJson);
            var currentUser = _db.Kullanicis.Where(x => x.KullaniciAdi == userdata.KullaniciAdi).FirstOrDefault();
            if (currentUser.Id == user.Id)
            {
                HttpContext.Session.Clear();
                userJson = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("user", userJson);
            }

            return RedirectToAction("Settings", "Admin");
        }

        [AdminOnly]
        [SendUserInfo]
        [HttpPost]
        public IActionResult DeleteSalon(SalonEditModel salon)
        {
            var silinecekSalon = _db.Salons.Where(sa => sa.Id == salon.SalonId ).FirstOrDefault();
            silinecekSalon.SilinmeTarihi = DateTime.Now;
            _db.SaveChanges();

            return RedirectToAction("Salons", "Admin");
        }

        [SendUserInfo]
        [AdminOnly]
        public IActionResult AddFilm()
        {
            return View();
        }

        [SendUserInfo]
        [AdminOnly]
        [HttpPost]
        public IActionResult AddFilm(FilmAddingModel film)
        {
            if (ModelState.IsValid)
            {
                // Resim dosyası kontrolü
                if (film.Resim != null && film.Resim.Length > 0)
                {
                    // Dosya adını ve yolunu belirleyin
                    var fileName = $"{film.Isim.Replace(" ", "_")}_{Guid.NewGuid()}{Path.GetExtension(film.Resim.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/movies", fileName);

                    // Resim dosyasını kaydedin
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        film.Resim.CopyTo(stream);
                    }

                    // Yeni film nesnesi oluşturun
                    Film yeniFilm = new Film
                    {
                        Isim = film.Isim,
                        Sure = film.Sure,
                        Turler = film.Turler,
                        FilmDurumu = film.FilmDurumu,
                        VizyonTarihi = film.VizyonTarihi,
                        OlusturulmaTarihi = DateTime.Now,
                        ResimYolu = $"/img/movies/{fileName}" // Poster dosya yolu
                    };

                    // Veritabanına yeni filmi ekleyin
                    _db.Films.Add(yeniFilm);
                    _db.SaveChanges();

                    ViewBag.Success = "Film başarıyla eklendi.";
                    return View(film);
                }
                return View();
            }
            return View();
        }

        [SendUserInfo]
        [AdminOnly]
        public IActionResult AddGosterim()
        {
            var sinemalar = _db.Sinemas.ToList();
            ViewBag.Sinemalar = sinemalar;
            var filmler = _db.Films.Where(film => film.FilmDurumu == 1 ).ToList();
            ViewBag.Filmler = filmler;
            return View();
        }

        [SendUserInfo]
        [AdminOnly]
        [HttpPost]
        public IActionResult AddGosterim(GosterimAddingModel gosterim)
        {
            var sinemalar = _db.Sinemas.ToList();
            ViewBag.Sinemalar = sinemalar;
            var filmler = _db.Films.Where(film => film.FilmDurumu == 1).ToList();
            ViewBag.Filmler = filmler;
            if (ModelState.IsValid)
            {
                var benzerGosterim = _db.Gosterims.Where(g => g.FilmId == gosterim.FilmId && g.SalonId == gosterim.SalonId && g.SunumTarihi == gosterim.SunumTarihi && g.SunumSaati == gosterim.SunumSaati).FirstOrDefault();
                if(benzerGosterim is null)
                {
                    Gosterim yeniGosterim = new Gosterim
                    {
                        FilmId = gosterim.FilmId,
                        Ucret = gosterim.Ucret,
                        SunumTarihi = gosterim.SunumTarihi,
                        SunumSaati = gosterim.SunumSaati,
                        SalonId = gosterim.SalonId
                    };

                    // Veritabanına yeni filmi ekleyin
                    _db.Gosterims.Add(yeniGosterim);
                    _db.SaveChanges();

                    ViewBag.Success = "Gösterim başarıyla eklendi.";
                }
                else
                {
                    ModelState.AddModelError("", "Bu gösterim zaten eklenmiş");
                }
                    return View();
            }
            else
            {
                ModelState.AddModelError("", "Girilen bilgilerde bir hata var.");
            }
            return View();
        }

        [HttpGet("/GetSalonlar/{sinemaId}")]
        public IActionResult GetSalonlar(int sinemaId)
        {
            var salonlar = _db.Salons.Where(sa => sa.Sinema.Id == sinemaId && sa.SilinmeTarihi == null);
            if (salonlar == null)
            {
                return NotFound();
            }
            return Json(salonlar);
        }
    }
}