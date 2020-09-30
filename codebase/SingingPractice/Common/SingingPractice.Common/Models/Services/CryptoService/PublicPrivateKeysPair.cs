using System;

namespace SingingPractice.Common.Models.Services.CryptoService
{
    public class PublicPrivateKeysPair
    {
        public byte[] PublicKey { get; set; }

        public byte[] PrivateKey { get; set; }

        public string PublicKeyString => Convert.ToBase64String(PublicKey);

        public string PrivateKeyString => Convert.ToBase64String(PrivateKey);
    }
}
