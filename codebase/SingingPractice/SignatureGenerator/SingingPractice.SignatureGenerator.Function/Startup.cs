using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SingingPractice.SignatureGenerator.Common.Contracts.Services;
using SingingPractice.SignatureGenerator.Logic.Services;

[assembly: FunctionsStartup(typeof(SingingPractice.SignatureGenerator.Function.Startup))]
namespace SingingPractice.SignatureGenerator.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<INotificationSender, FakeEmailNotificationSender>();
        }
    }
}
