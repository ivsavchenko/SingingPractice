using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace SingingPractice.RegistrationService.Web.Api.Middlewares
{
    public static class ExceptionHandlingMiddleware
    {
        public static async Task Handle(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature.Error;

            var result = JsonSerializer.Serialize(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }
}
