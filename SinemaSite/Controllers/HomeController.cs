using System.Configuration;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using NuGet.Protocol;
using SinemaSite.Models;


namespace SinemaSite.Controllers;

public class HomeController : Controller
{    
    private readonly ILogger<HomeController> _logger;
    private readonly CinemadbContext _db;

    public HomeController(ILogger<HomeController> logger, IConfiguration _configuration , CinemadbContext db)
    {
        _logger = logger;
        _db = db;
    }



    [SendUserInfo]
    public IActionResult Index()
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        var vizyonda = _db.Films.Where(x => x.VizyonTarihi < currentDate).ToList();
        ViewBag.Vizyonda = vizyonda;
        var gelecek = _db.Films.Where(x => x.VizyonTarihi >= currentDate).ToList();
        ViewBag.Gelecek = gelecek;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [SendUserInfo]
    public IActionResult Filmler()
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        var vizyonda = _db.Films.Where(x => x.VizyonTarihi < currentDate).ToList();
        ViewBag.Vizyonda = vizyonda;
        var gelecek = _db.Films.Where(x => x.VizyonTarihi >= currentDate).ToList();
        ViewBag.Gelecek = gelecek;

        return View();
    }

    [SendUserInfo]
    public IActionResult Sinemalar()
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
                        // Yalnızca eşleşen salonu seç
                        Salons = s.Salons
                            .Where(sa => sa.SinemaId == s.Id)
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
                    .ToList();
        ViewBag.Sinemalar = JsonConvert.SerializeObject(sinemalar);
        return View();
    }

    [SendUserInfo]
    public IActionResult Detaylar(int id)
    {
        var film = _db.Films.Where(x => x.Id == id)
                            .Select(s => new
                            {
                                s.Id,
                                s.Isim,
                                s.Sure,
                                s.FilmDurumu,
                                s.Turler,
                                s.ResimYolu,
                                Gosterims = s.Gosterims.Select(sa => new
                                {
                                    sa.Id,
                                    sa.SalonId,
                                    sa.FilmId,
                                    sa.Ucret,
                                    sa.SunumTarihi,
                                    sa.SunumSaati,
                                }).ToList()
                            })
                            .ToList();
        var salonIds = film
            .SelectMany(f => f.Gosterims)
            .Select(g => g.SalonId)
            .Distinct()
            .ToList();

        var salonlar = _db.Salons
            .Where(s => salonIds.Contains(s.Id))
            .Select(s => new
            {
                s.Id,
                s.SinemaId
            })
            .ToList();

        var sinemaIds = salonlar
            .Select(s => s.SinemaId)
            .Distinct()
            .ToList();

        var sinemalar = _db.Sinemas
            .Where(s => sinemaIds.Contains(s.Id))
            .ToList();

        if (film == null)
        {
            return NotFound();
        }
        var firstFilm = film.FirstOrDefault();
        ViewBag.Film = firstFilm;
        ViewBag.Sinemalar = sinemalar;
        return View();
    }

    [SendUserInfo]
    public IActionResult SinemaDetaylar(int id)
    {
        var sinema = _db.Sinemas.Where(x => x.Id == id)
                            .Select(s => new
                            {
                                s.Id,
                                s.Isim,
                                s.Adres,
                                Salons = s.Salons.Select(sa => new
                                {
                                    sa.Id,
                                    sa.Satir,
                                    sa.Sutun,
                                    sa.SalonTipi,
                                    sa.SalonNumarasi,
                                    Gosterims = sa.Gosterims
                                    .OrderBy(g => g.SunumSaati)
                                    .Select(g => new
                                    {
                                        g.Id,
                                        g.FilmId,
                                        g.SunumSaati,
                                        g.SunumTarihi,
                                        g.Ucret
                                    }).ToList()
                                }).ToList()
                            })
                            .ToList();

        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly oneMonthAgo = currentDate.AddDays(-30);
        var filmler = _db.Films.Where(x => x.VizyonTarihi >= oneMonthAgo).ToList();

        ViewBag.Filmler = filmler;
        ViewBag.Sinemalar = JsonConvert.SerializeObject(sinema);
        return View();
    }


    [HttpGet("GetGosterimInfo/{id}")]
    public IActionResult GetGosterimInfo(int id)
    {

        var gosterim = _db.Gosterims
       .Include(g => g.Film)
       .Include(g => g.Salon)
       .ThenInclude(s => s.Sinema)
       .Where(x => x.Id == id)
       .Select(g => new
       {
           g.Id,
           g.FilmId,
           g.SunumSaati,
           g.SunumTarihi,
           g.Ucret,
           Film = new
           {
               g.Film.Id,
               g.Film.Isim,
               g.Film.Turler,
               g.Film.ResimYolu,
               g.Film.VizyonTarihi,
           },
           Salon = new
           {
               g.Salon.Id,
               g.Salon.SalonNumarasi,
               g.Salon.SalonTipi,
               g.Salon.Satir,
               g.Salon.Sutun,
               Sinema = new
               {
                   g.Salon.Sinema.Id,
                   g.Salon.Sinema.Isim,
                   g.Salon.Sinema.Adres
               }
           },
           Tickets = g.Tickets.Select(t => new
           {
               t.Id,
               t.Satir,
               t.Sutun,
               t.MusteriId
           }).ToList()
       }).FirstOrDefault();

        if (gosterim == null)
        {
            return NotFound();
        }

        return Json(gosterim);
    }
    [HttpPost("submitSeats")]
    public IActionResult SubmitSeats([FromBody] SubmitSeatsRequest request)
    {
        if (request == null || !ModelState.IsValid)
        {
            return BadRequest(new { message = "Geçersiz Veri." });
        }

        var userJson = HttpContext.Session.GetString("user");
        var userdata = JsonConvert.DeserializeObject<Kullanici>(userJson);
        var currentUser = _db.Kullanicis.Where(x => x.KullaniciAdi == userdata.KullaniciAdi).FirstOrDefault();

        if (currentUser == null)
        {
            return BadRequest(new { message = "Kullanıcı Bulunamadı" });
        }
        else if (currentUser.Bakiye < request.ToplamFiyat)
        {
            return BadRequest(new { message = "Bakiye Yetersiz." });
        }

        foreach (var seat in request.Seats)
        {
            Ticket bilet = new Ticket()
            {
                GosterimId = request.GosterimId,
                Satir = seat.Row,
                Sutun = seat.Col,
                MusteriId = currentUser.Id,
                SatinAlimTarihi = DateOnly.FromDateTime(DateTime.Now)
            };
            _db.Tickets.Add(bilet);
            _db.SaveChanges();
        }
        currentUser.Bakiye -= request.ToplamFiyat;
        _db.SaveChanges();

        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        HttpContext.Session.Clear();
        userJson = JsonConvert.SerializeObject(currentUser);
        HttpContext.Session.SetString("user", userJson);

        // Satın alma işlemi başarılıysa
        return Json(new { success = true });
    }

    public class SubmitSeatsRequest
    {
        public List<Seat> Seats { get; set; }
        public int ToplamFiyat { get; set; }
        public int GosterimId { get; set; }
    }

    public class Seat
    {
        public int Col { get; set; }
        public int Row { get; set; }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}