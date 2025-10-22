
using System.Net;
using System.Text.Json;


namespace P12_WebApi.Middlewares
{
    public class ErrorHandler    {
        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
         
            var statusCode = HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = "An error occurred while processing your request.",
                Detailed = exception.Message 
            };

            var payload = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}
