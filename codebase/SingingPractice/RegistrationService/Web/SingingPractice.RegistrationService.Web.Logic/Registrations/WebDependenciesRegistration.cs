using Microsoft.Extensions.DependencyInjection;
using SingingPractice.Common.Constants;
using SingingPractice.Database;
using SingingPractice.Database.Registrations;
using SingingPractice.RegistrationService.Web.Common.Contracts.Managers;
using SingingPractice.RegistrationService.Web.Logic.Managers;

namespace SingingPractice.RegistrationService.Web.Logic.Registrations
{
    public static class WebDependenciesRegistration
    {
        public static void Configure(IServiceCollection services)
        {
            DatabaseRegistration.RegisterDatabase(EnvironmentConstants.ConnectionString);
            services.AddScoped(s => new SingingPracticeDb());
            services.AddScoped<ILicenseManager, LicenseManager>();
        }
    }
}
