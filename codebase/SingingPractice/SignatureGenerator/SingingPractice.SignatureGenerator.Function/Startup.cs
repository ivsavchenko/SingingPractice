using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SingingPractice.Common.Constants;
using SingingPractice.Database.Registrations;
using SingingPractice.SignatureGenerator.Common.Contracts.Managers;
using SingingPractice.SignatureGenerator.Common.Contracts.Services;
using SingingPractice.SignatureGenerator.Logic.Managers;
using SingingPractice.SignatureGenerator.Logic.Services;

[assembly: FunctionsStartup(typeof(SingingPractice.SignatureGenerator.Function.Startup))]
namespace SingingPractice.SignatureGenerator.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            DatabaseRegistration.RegisterDatabase(EnvironmentConstants.ConnectionString);
            builder.Services.AddSingleton<INotificationSender, FakeEmailNotificationSender>();
            builder.Services.AddSingleton<IRegistrationManager, RegistrationManager>();
        }
    }
}
