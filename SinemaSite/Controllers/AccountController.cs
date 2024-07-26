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
            var userJson = HttpContext.Session.GetString("user");
            var userdata = JsonConvert.DeserializeObject<Kullanici>(userJson);
            var user = _context.Kullanicis.Where(x => x.KullaniciAdi == userdata.KullaniciAdi).FirstOrDefault();
            user.SonAktifTarih = DateTime.Now;
            _context.SaveChanges();
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
                yeniKullanici.AktifMi = true;
                try
                {
                    _context.Kullanicis.Add(yeniKullanici);
                    _context.SaveChanges();

                    var user = _context.Kullanicis.Where(x => x.KullaniciAdi == kullanici.KullaniciAdi && x.Sifre == kullanici.Sifre).FirstOrDefault();

                    if (user != null)
                    {
                        var userJson = JsonConvert.SerializeObject(user);
                        HttpContext.Session.SetString("user", userJson);
                        user.SonAktifTarih = DateTime.Now;
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
                var user = _context.Kullanicis.Where(x => x.KullaniciAdi == kullanici.KullaniciAdi).FirstOrDefault();
                if (user != null && user.AktifMi)
                {
                    if(user.Sifre == kullanici.Sifre)
                    {
                        user.SonAktifTarih = DateTime.Now;
                        _context.SaveChanges();
                        var userJson = JsonConvert.SerializeObject(user);
                        HttpContext.Session.SetString("user", userJson);
                        if (user.KullaniciTipi != 2)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                    }
                }
                else if (!user.AktifMi)
                {
                    ModelState.AddModelError("", "Bu kullanıcı silinmiş");
                }
                return View();
            }
            return View();
        }

        [SessionAuthorize(true)]
        [SendUserInfo]
        public IActionResult Profile()
        {
            var userJson = HttpContext.Session.GetString("user");
            var userdata = JsonConvert.DeserializeObject<Kullanici>(userJson);
            var user = _context.Kullanicis.Where(x => x.KullaniciAdi == userdata.KullaniciAdi).FirstOrDefault();

            var tickets = _context.Tickets.Where(x => x.MusteriId == user.Id)
                .Include(x => x.Gosterim)
                .Include(x => x.Gosterim.Film)
                .Include(x => x.Gosterim.Salon)
                .Include(x => x.Gosterim.Salon.Sinema)
                .OrderByDescending(x => x.OlusturulmaTarihi)
                .ToList();

            var groupedTickets = tickets
                .GroupBy(t => t.SatinAlimTarihi)
                .OrderByDescending(g => g.Key)
                .ToList();

            ViewBag.GroupedTickets = groupedTickets;
            return View();
        }

        [SendUserInfo]
        [HttpPost]
        public IActionResult Profile(UserEditViewModel kullanici)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userJson = HttpContext.Session.GetString("user");
            var userdata = JsonConvert.DeserializeObject<Kullanici>(userJson);
            var user = _context.Kullanicis.Where(x => x.KullaniciAdi == userdata.KullaniciAdi).FirstOrDefault();


            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View();
            }
            try
            {
                user.KullaniciAdi = kullanici.KullaniciAdi ?? user.KullaniciAdi;
                user.Isim = kullanici.Isim ?? user.Isim;
                user.Soyisim = kullanici.Soyisim ?? user.Soyisim;
                user.Email = kullanici.Email ?? user.Email;
                user.GuncellemeTarihi = DateTime.Now;

                _context.SaveChanges();
                HttpContext.Session.Clear();
                userJson = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("user", userJson);

                return View();
            }
            catch
            {
                ViewBag.Error = "Lütfen kullanılmayan bir mail veya kullanıcı adı giriniz.";
                return View();
            }
        }

        [SendUserInfo]
        [SessionAuthorize(true)]
        public IActionResult BakiyeYukle()
        {
            return View();
        }

        [SendUserInfo]
        [HttpPost]
        public IActionResult BakiyeYukle(BalanceLoadModel bakiye)
        {
            var userJson = HttpContext.Session.GetString("user");
            var userdata = JsonConvert.DeserializeObject<Kullanici>(userJson);
            var currentUser = _context.Kullanicis.Where(x => x.KullaniciAdi == userdata.KullaniciAdi).FirstOrDefault();

            currentUser.Bakiye += bakiye.YuklenecekMiktar;
            _context.SaveChanges();

            HttpContext.Session.Clear();
            userJson = JsonConvert.SerializeObject(currentUser);
            HttpContext.Session.SetString("user", userJson);

            return View();
        }

        [HttpPost]
        public IActionResult DeleteUser()
        {
            var userJson = HttpContext.Session.GetString("user");
            var userdata = JsonConvert.DeserializeObject<Kullanici>(userJson);
            var user = _context.Kullanicis.Where(x => x.KullaniciAdi == userdata.KullaniciAdi).FirstOrDefault();
            user.SilinmeTarihi = DateTime.Now;
            user.AktifMi = false;
            _context.SaveChanges();
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

    }
}
