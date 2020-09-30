using SingingPractice.Common.Contracts.Services;

namespace SingingPractice.Common.Logic.Services
{
    public class BCryptHashingService : IHashingService
    {
        public string CreateSalt()
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            return salt;
        }

        public string CreateHash(string valueToHash, string salt)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(valueToHash, salt);
            return hash;
        }
    }
}
