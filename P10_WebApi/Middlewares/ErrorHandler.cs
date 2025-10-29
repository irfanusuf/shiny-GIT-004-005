

using Serilog;
using System.Net;



namespace P10_WebApi.Middlewares
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);   // delagate    middleware A  just transfers the request to B 
            }
            catch (Exception ex)
            {
    
                          //{response from the controllers}   middleware C to B to A ...... Error handler   
                Log.Error(ex, "Unhandled exception for request {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                await HandleExceptionAsync(context, ex);
            }
        }



        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;   // 500 

                // generic  response to client whenever inner exception in controllers happen .................... 

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = "An error occurred while processing your request."
            };

            // var payload = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}





