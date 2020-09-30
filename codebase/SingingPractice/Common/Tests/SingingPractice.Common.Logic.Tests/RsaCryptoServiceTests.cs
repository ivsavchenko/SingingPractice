using System;
using System.Text;
using NUnit.Framework;
using SingingPractice.Common.Logic.Services;

namespace SingingPractice.Common.Logic.Tests
{
    public class RsaCryptoServiceTests
    {
        [Test]
        public void RsaCryptoKeysShouldBeDifferent()
        {
            using var cryptoService1 = new RsaCryptoService();
            using var cryptoService2 = new RsaCryptoService();
            var keysPair1 = cryptoService1.GetEncryptionParameters();
            var keysPair2 = cryptoService2.GetEncryptionParameters();

            Assert.AreNotEqual(keysPair1.PrivateKeyString, keysPair2.PrivateKeyString);
            Assert.AreNotEqual(keysPair1.PublicKeyString, keysPair2.PublicKeyString);
        }

        [Test]
        public void RsaCryptoKeysShouldBeSame()
        {
            using var cryptoService = new RsaCryptoService();
            var keysPair1 = cryptoService.GetEncryptionParameters();
            var keysPair2 = cryptoService.GetEncryptionParameters();

            Assert.AreEqual(keysPair1.PrivateKeyString, keysPair2.PrivateKeyString);
            Assert.AreEqual(keysPair1.PublicKeyString, keysPair2.PublicKeyString);
        }

        [Test]
        public void RsaCryptoKeysShouldBeDecodable()
        {
            using var cryptoService = new RsaCryptoService();
            var keysPair = cryptoService.GetEncryptionParameters();

            var privateKey = Convert.FromBase64String(keysPair.PrivateKeyString);
            var publicKey = Convert.FromBase64String(keysPair.PublicKeyString);

            Assert.AreEqual(keysPair.PrivateKey, privateKey);
            Assert.AreEqual(keysPair.PublicKey, publicKey);
        }

        [Test]
        public void RsaCryptoKeysSigning()
        {
            var data = "Hello World!";
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using var cryptoServiceToSign = new RsaCryptoService();
            var signature = cryptoServiceToSign.Sign(dataBytes);
            var parameters = cryptoServiceToSign.GetEncryptionParameters();

            using var cryptoServiceToVerify = new RsaCryptoService(parameters.ParametersXml);
            var isValid = cryptoServiceToVerify.Verify(dataBytes, signature);

            Assert.IsTrue(isValid);
        }
    }
}