using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using SinemaSite.Models;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace SinemaSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly CinemadbContext _context;

        public AccountController(CinemadbContext context)
        {
            _context = context;
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [SessionAuthorize(false)]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Kullanici kullanici)
        {
            if (kullanici is not null)
            {
                Kullanici yeniKullanici = new Kullanici();
                yeniKullanici.Id = kullanici.Id;
                yeniKullanici.Isim = kullanici.Isim;
                yeniKullanici.Soyisim = kullanici.Soyisim;
                yeniKullanici.KullaniciAdi = kullanici.KullaniciAdi;
                yeniKullanici.Email = kullanici.Email;
                yeniKullanici.Sifre = kullanici.Sifre;
                try
                {
                    _context.Kullanicis.Add(yeniKullanici);
                    _context.SaveChanges();

                    var user = _context.Kullanicis.Where(x => x.KullaniciAdi == kullanici.KullaniciAdi && x.Sifre == kullanici.Sifre).FirstOrDefault();

                    if (user != null)
                    {
                        var userJson = JsonConvert.SerializeObject(user);
                        HttpContext.Session.SetString("user", userJson);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Kullanıcı bilgileri ile giriş yapılamazsa hata mesajı
                        ViewBag.Error = "Giriş işlemi lütfen manuel olarak deneyiniz.";
                        return View(kullanici);
                    }
                    
                }
                catch
                {
                    ViewBag.Error = "Lütfen kullanılmayan bir mail veya kullanıcı adı giriniz.";
                    return View(kullanici);
                }

                return View();
            }
            else
                return View(kullanici);
        }

        [SessionAuthorize(false)]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Kullanici kullanici)
        {
            if (kullanici is not null)
            {
                var user = _context.Kullanicis.Where(x => x.KullaniciAdi == kullanici.KullaniciAdi && x.Sifre == kullanici.Sifre).FirstOrDefault();
                if (user != null) 
                {
                    var userJson = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("user", userJson);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                }
                return View();
            }
            return View();
        }

        [SessionAuthorize(true)]
        [SendUserInfo]
        public IActionResult Profile()
        {
            return View();
        }

    }
}
