namespace SingingPractice.Common.Contracts.Services
{
    public interface IHashingService
    {
        string CreateSalt();

        string CreateHash(string valueToHash, string salt);
    }
}