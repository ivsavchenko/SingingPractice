using System;
using System.Threading.Tasks;
using SingingPractice.RegistrationService.Web.Common.Contracts.Managers;
using SingingPractice.RegistrationService.Web.Common.Enums;
using SingingPractice.RegistrationService.Web.Common.Models.Licenses;

namespace SingingPractice.RegistrationService.Web.Logic.Managers
{
    public class LicenseManager : ILicenseManager
    {
        public LicenseManager()
        {
        }

        public Task ActivateAsync(ActivateLicenseDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<LicenseStatus> ValidateAsync(Guid key)
        {
            // TODO DB logic goes here
            await Task.Yield();

            var hash = "getHashFromDb";
            var isValid = BCrypt.Net.BCrypt.Verify(key.ToString(), hash);

#warning fix incorrect logic
            var licenseStatus = isValid ? LicenseStatus.Inactive : LicenseStatus.Invalid;

            return licenseStatus;
        }

        public async Task<Guid> IssueAsync()
        {
            var licenseKey = Guid.NewGuid();
            var hash = BCrypt.Net.BCrypt.HashPassword(licenseKey.ToString());

            // TODO insert to DB here
            await Task.Yield();

            return licenseKey;
        }
    }
}
