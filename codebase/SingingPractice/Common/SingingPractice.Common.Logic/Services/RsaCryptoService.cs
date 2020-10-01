using System;
using System.Security.Cryptography;
using SingingPractice.Common.Contracts.Services;
using SingingPractice.Common.Models.Services.CryptoService;

namespace SingingPractice.Common.Logic.Services
{
    public class RsaCryptoService : ICryptoService, IDisposable
    {
        private readonly RSA rsa;
        private bool disposed = false;

        public RsaCryptoService(string parametersXml = null)
        {
            rsa = RSA.Create();

            if (parametersXml != null)
            {
                rsa.FromXmlString(parametersXml);
            }
        }

        public PublicPrivateKeysPair GetEncryptionParameters()
        {
            var keys = new PublicPrivateKeysPair
            {
                PrivateKey = rsa.ExportRSAPrivateKey(),
                PublicKey = rsa.ExportRSAPublicKey(),
                ParametersXml = rsa.ToXmlString(false)
            };
            
            return keys;
        }

        public byte[] Sign(byte[] data)
        {
            var signedData = rsa.SignData(data, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
            return signedData;
        }

        public bool Verify(byte[] data, byte[] signature)
        {
            var isValid = rsa.VerifyData(data, signature, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
            return isValid;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                rsa?.Dispose();
            }

            disposed = true;
        }
    }
}
