
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.MiddlewareExceptions
{
    public class Middleware1(IHostEnvironment host,ILogger<Middleware1> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        public async Task HandleException(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode =HttpStatusCode.InternalServerError;
            logger.LogError(ex.Message);
            httpContext.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new ProblemDetails()
            {
                Title = ex.Message,
                Detail = host.IsDevelopment() ? ex.StackTrace?.ToString() : ex.Message,
                Status =(int) statusCode,
            };
            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json= JsonSerializer.Serialize(response, option);
            await httpContext.Response.WriteAsync(json);
            await httpContext.Response.WriteAsync(json);

        }
    }
}
