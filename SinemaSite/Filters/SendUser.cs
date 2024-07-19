using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SinemaSite.Models;
using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class SendUserInfo : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userJson = context.HttpContext.Session.GetString("user");
        if (!string.IsNullOrEmpty(userJson))
        {
            var user = JsonConvert.DeserializeObject<Kullanici>(userJson);
            context.HttpContext.Items["CurrentUser"] = user;
        }

        base.OnActionExecuting(context);
    }
}

