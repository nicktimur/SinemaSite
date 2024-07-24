using System.Configuration;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}