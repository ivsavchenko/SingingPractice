using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using SingingPractice.Common.Constants;

namespace SingingPractice.RegistrationService.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddApplicationInsights(EnvironmentConstants.AppInsightsInstrumenationKey);
                    builder.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information);
                });
    }
}
