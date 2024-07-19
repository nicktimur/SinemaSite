using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class SessionAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly bool _canGoLoggedIn;

    public SessionAuthorizeAttribute(bool canGoLoggedIn)
    {
        _canGoLoggedIn = canGoLoggedIn;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!_canGoLoggedIn && context.HttpContext.Session.GetString("user") != null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
        else if (_canGoLoggedIn && context.HttpContext.Session.GetString("user") == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}
