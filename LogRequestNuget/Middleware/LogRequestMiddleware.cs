
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LogRequestNuget.Middleware
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public LogRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using var buffer = new MemoryStream();
            var request = httpContext.Request;
            var response = httpContext.Response;
            var stream = response.Body;
            response.Body = buffer;
            await _next(httpContext);
            Debug.WriteLine($"Request Content Type : { httpContext.Request.Headers["Accept"]} {Environment.NewLine}" +
                            $"Request Path : {request.Path} { Environment.NewLine} " +
                            $"Response Type : {response.ContentType} {Environment.NewLine}" +
                            $"Response Length : {response.ContentLength ?? buffer.Length}");
            buffer.Position = 0;
            await buffer.CopyToAsync(stream);
        }
    }

    public static class LogRequestMiddlewareExtensions  
    {
        public static IApplicationBuilder UseLogRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogRequestMiddleware>();
        }
    }
}
