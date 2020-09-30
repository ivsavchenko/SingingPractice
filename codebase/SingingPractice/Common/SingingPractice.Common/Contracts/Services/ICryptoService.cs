using SingingPractice.Common.Models.Services.CryptoService;

namespace SingingPractice.Common.Contracts.Services
{
    public interface ICryptoService
    {
        PublicPrivateKeysPair CreateKeys();
    }
}
