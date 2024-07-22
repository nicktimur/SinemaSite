using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SinemaSite.Models;
using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class AdminOnly : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.Session.GetString("user") != null)
        {
            var userJson = context.HttpContext.Session.GetString("user");
            var userData = JsonConvert.DeserializeObject<Kullanici>(userJson);
            if(userData.KullaniciTipi != 2)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
        else
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}