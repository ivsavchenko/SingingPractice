using System.Security.Cryptography;
using SingingPractice.Common.Contracts.Services;
using SingingPractice.Common.Models.Services.CryptoService;

namespace SingingPractice.Common.Logic.Services
{
    public class RsaCryptoService : ICryptoService
    {
        public PublicPrivateKeysPair CreateKeys()
        {
            using var rsa = RSA.Create();

            var keys = new PublicPrivateKeysPair
            {
                PrivateKey = rsa.ExportRSAPrivateKey(),
                PublicKey = rsa.ExportRSAPublicKey()
            };

            return keys;
        }
    }
}
