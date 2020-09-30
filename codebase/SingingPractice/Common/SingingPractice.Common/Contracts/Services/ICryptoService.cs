using SingingPractice.Common.Models.Services.CryptoService;

namespace SingingPractice.Common.Contracts.Services
{
    public interface ICryptoService
    {
        PublicPrivateKeysPair GetEncryptionParameters();

        byte[] Sign(byte[] data);

        bool Verify(byte[] data, byte[] signature);
    }
}
