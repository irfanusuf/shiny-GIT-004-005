using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using P0_ClassLibrary.Interfaces;

namespace P7_WebApi.Middlewares;

[AttributeUsage(AttributeTargets.All)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{

    public void OnAuthorization(AuthorizationFilterContext context)
    {


        var token = context.HttpContext.Request.Cookies["P7WebApi_Auth_Token"];

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
        
        
        if (userId == Guid.Empty)
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
