using System;
using System.Threading.Tasks;
using SingingPractice.RegistrationService.Web.Common.Contracts.Managers;

namespace SingingPractice.RegistrationService.Web.Logic.Managers
{
    public class LicenseManager : ILicenseManager
    {
        public LicenseManager()
        {
        }

        public Task ActivateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> IssueAsync()
        {
            var licenseKey = Guid.NewGuid();

            var hash = BCrypt.Net.BCrypt.HashPassword(licenseKey.ToString());

            // TODO for testing needs
            var isValid = BCrypt.Net.BCrypt.Verify(licenseKey.ToString(), hash);

            if (!isValid)
            {
                throw new Exception("Not validated");
            }

            // TODO insert to DB here
            await Task.Yield();

            return licenseKey;
        }
    }
}
