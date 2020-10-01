using System;
using System.Threading.Tasks;
using LinqToDB;
using SingingPractice.Common.Contracts.Services;
using SingingPractice.Common.Logic.Extensions;
using SingingPractice.Database;
using SingingPractice.RegistrationService.Web.Common.Contracts.Managers;
using SingingPractice.RegistrationService.Web.Common.Models.Songs;

namespace SingingPractice.RegistrationService.Web.Logic.Managers
{
    public class SongManager : ISongManager
    {
        private readonly SingingPracticeDb singingPracticeDb;
        private readonly ICryptoService cryptoService;

        public SongManager(SingingPracticeDb singingPracticeDb, ICryptoService cryptoService)
        {
            this.singingPracticeDb = singingPracticeDb;
            this.cryptoService = cryptoService;
        }

        public async Task<string> SingAsync(SongDto dto)
        {
            var customer = await singingPracticeDb.Customers.FirstAsync(c => c.Email.ToLower() == dto.Email.ToLower());
            cryptoService.Initialize(customer.PublicParameters);

            var textBytes = dto.Text.GetBytes();
            var signatureBytes = Convert.FromBase64String(dto.Signature);

            var isTrusted = cryptoService.Verify(textBytes, signatureBytes);

            return isTrusted ? $"Singing your song: {dto.Text}" : "No singing for strangers";
        }
    }
}
