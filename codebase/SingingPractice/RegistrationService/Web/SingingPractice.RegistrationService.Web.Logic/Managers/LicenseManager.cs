using System;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.Azure.ServiceBus;
using SingingPractice.Common.Constants;
using SingingPractice.Common.Extensions;
using SingingPractice.Database;
using SingingPractice.RegistrationService.Web.Common.Contracts.Managers;
using SingingPractice.RegistrationService.Web.Common.Enums;
using SingingPractice.RegistrationService.Web.Common.Models.Licenses;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SingingPractice.RegistrationService.Web.Logic.Managers
{
    public class LicenseManager : ILicenseManager
    {
        private readonly SingingPracticeDb singingPracticeDb;

        public LicenseManager(SingingPracticeDb singingPracticeDb)
        {
            this.singingPracticeDb = singingPracticeDb;
        }

        public async Task ActivateAsync(ActivateLicenseDto dto)
        {
            var serialized = JsonSerializer.Serialize(dto);

            var messageBody = System.Text.Encoding.Unicode.GetBytes(serialized);
            var builder = new ServiceBusConnectionStringBuilder(EnvironmentConstants.ServiceBusWriterConnection)
            {
                EntityPath = EnvironmentConstants.ServiceBusEntityPath
            };

            var client = new QueueClient(builder);
            await client.SendAsync(new Message(messageBody));
        }

        public async Task<LicenseStatus> ValidateAsync(string key)
        {
            // TODO handle all exceptions and hide them from the end user 
            var deserialized = key.FromBase64();
            var issuedLicense = JsonSerializer.Deserialize<IssuedLicenseDto>(deserialized);
            var license = await singingPracticeDb.Licenses.FirstOrDefaultAsync(l => l.Id == issuedLicense.Id);

            if (license == null || string.IsNullOrWhiteSpace(license.KeyHash))
            {
                return LicenseStatus.Invalid;
            }

            var hash = BCrypt.Net.BCrypt.HashPassword(issuedLicense.Key.ToString(), license.Salt);

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
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hash = BCrypt.Net.BCrypt.HashPassword(licenseKey.ToString(), salt);
            var license = new License {Id = id, KeyHash = hash, Salt = salt, CreationDate = DateTime.UtcNow};

            await singingPracticeDb.InsertAsync(license);
            var issuedLicense = new IssuedLicenseDto {Id = id, Key = licenseKey};
            var serialized  = JsonSerializer.Serialize(issuedLicense);
            var result = serialized.ToBase64();
            
            return result;
        }
    }
}
