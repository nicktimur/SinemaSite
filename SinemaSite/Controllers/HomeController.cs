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
        DateOnly twoWeeksAgo = currentDate.AddDays(-14);
        var filmler = _db.Films.Where(x => x.VizyonTarihi >= twoWeeksAgo).ToList();

        ViewBag.Filmler = filmler;
        ViewBag.Sinemalar = JsonConvert.SerializeObject(sinema);
        return View();
    }

    [SendUserInfo]
    [SessionAuthorize(true)]
    public IActionResult SatinAl(int id)
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

        var jsonGosterim = JsonConvert.SerializeObject(gosterim);

        ViewBag.Gosterim = jsonGosterim;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}