using System;
using System.Threading.Tasks;
using LinqToDB;
using SingingPractice.Common.Contracts.Services;
using SingingPractice.Common.Logic.Extensions;
using SingingPractice.Database;
using SingingPractice.RegistrationService.Web.Common.Contracts.Managers;
using SingingPractice.RegistrationService.Web.Common.Enums;
using SingingPractice.RegistrationService.Web.Common.Models.Licenses;

namespace SingingPractice.RegistrationService.Web.Logic.Managers
{
    public class LicenseManager : ILicenseManager
    {
        private readonly SingingPracticeDb singingPracticeDb;
        private readonly IHashingService hashingService;
        private readonly IMessageSenderService messageSenderService;

        public LicenseManager(SingingPracticeDb singingPracticeDb, IHashingService hashingService, IMessageSenderService messageSenderService)
        {
            this.singingPracticeDb = singingPracticeDb;
            this.hashingService = hashingService;
            this.messageSenderService = messageSenderService;
        }

        public async Task ActivateAsync(ActivateLicenseDto dto)
        {
            await messageSenderService.SendAsync(dto);
        }

        public async Task<LicenseStatus> ValidateAsync(string key)
        {
            var issuedLicense = key.FromJsonBase64<IssuedLicenseDto>();
            var license = await singingPracticeDb.Licenses.FirstOrDefaultAsync(l => l.Id == issuedLicense.Id);

            if (license == null || string.IsNullOrWhiteSpace(license.KeyHash))
            {
                return LicenseStatus.Invalid;
            }

            var hash = hashingService.CreateHash(issuedLicense.Key.ToString(), license.Salt);

            if (license.KeyHash != hash)
            {
                return LicenseStatus.Invalid;
            }

            return license.ActivationDate.HasValue ? LicenseStatus.Active : LicenseStatus.Inactive;
        }

        public async Task<string> IssueAsync()
        {
            var id = Guid.NewGuid();
            var licenseKey = Guid.NewGuid();
            var salt = hashingService.CreateSalt();
            var hash = hashingService.CreateHash(licenseKey.ToString(), salt);
            var license = new License {Id = id, KeyHash = hash, Salt = salt, CreationDate = DateTime.UtcNow};

            await singingPracticeDb.InsertAsync(license);
            var issuedLicense = new IssuedLicenseDto {Id = id, Key = licenseKey};
            var result = issuedLicense.ToJsonBase64();
            
            return result;
        }
    }
}
