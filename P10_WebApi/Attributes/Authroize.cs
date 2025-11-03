using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using P0_ClassLibrary.Interfaces;

namespace P10_WebApi.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class AuthroizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Cookies["P10WebApi_AuthToken"];

        if (token == null)
        {
            context.Result = new JsonResult(new { message = "Session Expired ! Kindly Login Again !" })
            {
                StatusCode = 401
            };
            return;
        }
        var tokenService = context.HttpContext.RequestServices.GetService(typeof(ITokenService)) as ITokenService;



        var userId = tokenService?.VerifyTokenAndGetId(token);
        
        
        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new JsonResult(new { message = "Forbidden ! Can't access the Resources !" })
            {
                StatusCode = 403
            };
            return;
        }
        else
        {
            context.HttpContext.Items["userId"] = userId;    // session 
        }
    }
}
