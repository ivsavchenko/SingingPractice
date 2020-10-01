using SingingPractice.Common.Models.Services.CryptoService;

namespace SingingPractice.Common.Contracts.Services
{
    public interface ICryptoService
    {
        PublicPrivateKeysPair GetEncryptionParameters();

        void Initialize(string xml);

        byte[] Sign(byte[] data);

        bool Verify(byte[] data, byte[] signature);
    }
}
