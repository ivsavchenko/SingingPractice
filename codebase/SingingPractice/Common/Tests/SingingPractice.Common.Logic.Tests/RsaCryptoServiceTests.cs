using System;
using NUnit.Framework;
using SingingPractice.Common.Logic.Services;

namespace SingingPractice.Common.Logic.Tests
{
    public class RsaCryptoServiceTests
    {
        [Test]
        public void RsaCryptoKeysShouldBeDifferent()
        {
            var cryptoService = new RsaCryptoService();
            var keysPair1 = cryptoService.CreateKeys();
            var keysPair2 = cryptoService.CreateKeys();

            Assert.AreNotEqual(keysPair1.PrivateKeyString, keysPair2.PrivateKeyString);
            Assert.AreNotEqual(keysPair1.PublicKeyString, keysPair2.PublicKeyString);
        }

        [Test]
        public void RsaCryptoKeysShouldBeDecodable()
        {
            var cryptoService = new RsaCryptoService();
            var keysPair = cryptoService.CreateKeys();

            var privateKey = Convert.FromBase64String(keysPair.PrivateKeyString);
            var publicKey = Convert.FromBase64String(keysPair.PublicKeyString);

            Assert.AreEqual(keysPair.PrivateKey, privateKey);
            Assert.AreEqual(keysPair.PublicKey, publicKey);
        }
    }
}