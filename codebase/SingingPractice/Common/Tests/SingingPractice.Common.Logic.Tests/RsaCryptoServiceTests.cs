using System;
using NUnit.Framework;
using SingingPractice.Common.Logic.Extensions;
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
            var dataBytes = data.GetBytes();
            using var cryptoServiceToSign = new RsaCryptoService();
            var signature = cryptoServiceToSign.Sign(dataBytes);
            var parameters = cryptoServiceToSign.GetEncryptionParameters();

            using var cryptoServiceToVerify = new RsaCryptoService(parameters.ParametersXml);
            var isValid = cryptoServiceToVerify.Verify(dataBytes, signature);

            Assert.IsTrue(isValid);
        }

        [Test]
        public void RsaCryptoKeysSigningWithRealValues()
        {
            // values given for user unsafe2@m.m
            var publicParameters = "<RSAKeyValue><Modulus>nJaOQn0E3bh1hYZf5pkgI28jsCn0AokOCoqUPzU0YjUj5P6adS2M946udewKlzBnSY6skqPHoDhPmDfNV589fSA5CQahl9sPAD5eGgZiawjFDRzQhEEP3eXcmmN4ZE6chm7OXNM8iu3+v8xrzxwpN9B0UNsnOG2D/l3mHFnYY+3NlBQaaiLieVg1YkyWAkd50BFoIpdEJLv81oXt10CsZ+0fRA0QGovlEuUqlZaIjht1NL3ciAShNuer3oRNJbhAHoLvodyCegfcRxReIUvoJjVJTUh05XJ25CwV64s8ChuTb3YcSnJMfhDuquclSfAdYDOf1zg5YbVuedETK79RfQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var privateParameters = "<RSAKeyValue><Modulus>nJaOQn0E3bh1hYZf5pkgI28jsCn0AokOCoqUPzU0YjUj5P6adS2M946udewKlzBnSY6skqPHoDhPmDfNV589fSA5CQahl9sPAD5eGgZiawjFDRzQhEEP3eXcmmN4ZE6chm7OXNM8iu3+v8xrzxwpN9B0UNsnOG2D/l3mHFnYY+3NlBQaaiLieVg1YkyWAkd50BFoIpdEJLv81oXt10CsZ+0fRA0QGovlEuUqlZaIjht1NL3ciAShNuer3oRNJbhAHoLvodyCegfcRxReIUvoJjVJTUh05XJ25CwV64s8ChuTb3YcSnJMfhDuquclSfAdYDOf1zg5YbVuedETK79RfQ==</Modulus><Exponent>AQAB</Exponent><P>wxTTvWl986BQQk637T8fmgxd2DU1MQmZM9d7zu9uyCqcn26SE4Lq4U+Ix1KV6eprfLAlxkhXHEACTcnf9hXEeNzywp8zDPD9KF+GmMPC7AlqXlP8HIc0ExRoXmOZizjRdtlvC1I3L8bmgbEl6GTy19qFqrUAEnru0rQ/0nYtYvM=</P><Q>zXyBaKjPC3FOuONrga08tNYrICWWltXnPzUZypzwRZX635awIe0RAcMqGyysXdHmGIPuDD8TeYhZOA6w3ihCVJmVCi8zyZC/HJDAFoOTDaeuH7TxUE6htF+z5BfefRIs7fX1jcKyzujWKakMq7pf0rIcdZf14JtEQXcBTC4wNc8=</Q><DP>OsMiSNLnAqTOqDqIAqnZ/hAtkHvuitfmUwxcmefbieX0Cb5HuCLeV4IapFfHGo/nUsbIiiKuQq8xQndFxB2ocfO0GFXWDdblmuyzYX7OT0VCyikoLvu2/uxNx+jejmZOCivS3CkmwHh8ZKKU1Zza3ZRQYSxmWiq3l7Z95wp7/2s=</DP><DQ>yOcA8x2QiER4ziVzd66zWq7GLUDy0XhDBjZZiBIWjEJNrIr8m77XUNzKbxUnVPciOzfJ5ulIlhsr97XNUPcVmvcpk3KA9IJzh28yjxCHFuOpR1C2Wmj2io7DJ6/6lFfP27wNH1OLVOaqdMLWI5QtmacQUhyHhdjoTAUZHK1P71s=</DQ><InverseQ>tw9UXkq8pMGmGJE89aoL6L7SdzNWcc+PKVd+IsZW+mk3YIwho0EsAZcPpbFnBHBrwRxDxUaJlWT8KfnImYcWwjwAgG1GHFcnlGt+pmryqz1lhu+waiCHnr9STZfUATc0K7COMLZNZbKA9KR9TgdAYCTQ7904HvTwZRW9FjRBx4o=</InverseQ><D>O+F/SYHRqJBvsL4wUljgZ2yK80U34PhUfEd+ZaWNALldnqaWnpTqwYi4wrOfYS3Lcd+zsuga4PRny8gbKJTmyMDXztHQXegRloBDMCSc4l2aLWeFfe37iVnrwMalNYayaN5a4DiPgHl+4A3mdG9Ke+Old343Q2buAQobg2AAVkXzFue7PRO4QOQekmhKYCFsayWvyXHRdhBMowLa2T/qkgc2noyojm0/tp+D3bYei5ttpBNie9Rs5ruFluUCqWe8duyc5syrFOqjVziU0xkvNW3rdZpyPLYoiOwkaUmOklkwiVnpGUGkr3CdxXEnymonjSyo31B03U9eBYkbc+I06Q==</D></RSAKeyValue>";
            var data = "Hello World!";

            var dataBytes = data.GetBytes();
            using var cryptoServiceToSign = new RsaCryptoService(privateParameters);
            var signature = cryptoServiceToSign.Sign(dataBytes);
            var signatureString = Convert.ToBase64String(signature);

            using var cryptoServiceToVerify = new RsaCryptoService(publicParameters);
            var isValid = cryptoServiceToVerify.Verify(dataBytes, signature);

            Assert.IsNotNull(signatureString);
            Assert.IsTrue(isValid);
        }
    }
}