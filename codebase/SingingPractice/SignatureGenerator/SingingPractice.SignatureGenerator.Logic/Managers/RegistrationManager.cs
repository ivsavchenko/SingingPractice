using System;
using System.Text.Json;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.Extensions.Logging;
using SingingPractice.Common.Contracts.Services;
using SingingPractice.Common.Logic.Extensions;
using SingingPractice.Common.Models.Licenses;
using SingingPractice.Database;
using SingingPractice.SignatureGenerator.Common.Contracts.Managers;
using SingingPractice.SignatureGenerator.Common.Contracts.Services;
using SingingPractice.SignatureGenerator.Common.Models.Notifications;

namespace SingingPractice.SignatureGenerator.Logic.Managers
{
    public class RegistrationManager : IRegistrationManager
    {
        private readonly INotificationSender notificationSender;
        private readonly SingingPracticeDb singingPracticeDb;
        private readonly ICryptoService cryptoService;
        private readonly IHashingService hashingService;
        private readonly ILogger<RegistrationManager> logger;

        public RegistrationManager(SingingPracticeDb singingPracticeDb, INotificationSender notificationSender, 
            ICryptoService cryptoService, IHashingService hashingService, ILogger<RegistrationManager> logger)
        {
            this.singingPracticeDb = singingPracticeDb;
            this.notificationSender = notificationSender;
            this.cryptoService = cryptoService;
            this.hashingService = hashingService;
            this.logger = logger;
        }

        public async Task RegisterAsync(string json)
        {
            var licenseToActivate = JsonSerializer.Deserialize<ActivateLicenseDto>(json);
            var issuedLicense = licenseToActivate.Key.FromJsonBase64<IssuedLicenseDto>();
            var license = await singingPracticeDb.Licenses.FirstOrDefaultAsync(l => l.Id == issuedLicense.Id);
            var hash = hashingService.CreateHash(issuedLicense.Key.ToString(), license?.Salt);
            
            if (license == null || license.ActivationDate != null || license.KeyHash != hash)
            {
                logger.LogWarning($"Can't activate license {issuedLicense.Id}");
                return;
            }

            var signingKey = await ActivateCustomersLicenseAsync(licenseToActivate, license);
            await SendNotificationAsync(licenseToActivate, signingKey);
        }

        private async Task SendNotificationAsync(ActivateLicenseDto licenseToActivate, string signingKey)
        {
            var notification = new EmailNotificationDbo
            {
                Email = licenseToActivate.User.Email,
                Name = licenseToActivate.User.Name,
                SigningKey = signingKey
            };

            await notificationSender.SendAsync(notification);
        }

        private async Task<string> ActivateCustomersLicenseAsync(ActivateLicenseDto licenseToActivate, License license)
        {
            var existingCustomer = await singingPracticeDb.Customers
                .FirstOrDefaultAsync(l => l.Email.ToLower() == licenseToActivate.User.Email.ToLower());

            var parameters = cryptoService.GetEncryptionParameters();

            var newCustomer = new Customer
            {
                Address = licenseToActivate.User.Address,
                Email = licenseToActivate.User.Email,
                Name = licenseToActivate.User.Name,
                PublicParameters = parameters.ParametersXml
            };

            var customerLicense = new CustomerLicense
            {
                LicenseId = license.Id
            };

            await using var transaction = await singingPracticeDb.BeginTransactionAsync();
            try
            {
                customerLicense.CustomerId = existingCustomer == null ? await singingPracticeDb.InsertWithInt32IdentityAsync(newCustomer) : existingCustomer.Id;
                await singingPracticeDb.InsertWithInt32IdentityAsync(customerLicense);
                license.ActivationDate = DateTime.UtcNow;
                await singingPracticeDb.UpdateAsync(license);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            var signingKey = existingCustomer == null ?
                parameters.PrivateParametersXml // we use it only for demonstration needs. In real life we have to return only private key
                : "Use the key received with the very first license";

            return signingKey;
        }
    }
}
