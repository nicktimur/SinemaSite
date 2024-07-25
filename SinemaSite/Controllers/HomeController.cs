using System.Configuration;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
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
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [SendUserInfo]
    public IActionResult Filmler()
    {
        var vizyonda = _db.Films.Where(x => x.FilmDurumu == 1 ).ToList();
        ViewBag.Vizyonda = vizyonda;
        var gelecek = _db.Films.Where(x => x.FilmDurumu == 2).ToList();
        ViewBag.Gelecek = gelecek;

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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}