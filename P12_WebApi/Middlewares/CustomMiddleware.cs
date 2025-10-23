using System;

namespace P12_WebApi.Middlewares;

public class CustomMiddleware
{

    private readonly RequestDelegate next;


    public CustomMiddleware(RequestDelegate request)
    {
        next = request;

    }
    
    public void Invoke(HttpContext context)
    {
        Console.WriteLine("something is happening !!! before processing ..... processsing ...........");

        // jwt verify 

        // if (verify)
        // {
        next(context);
        // }


    


    }


}
