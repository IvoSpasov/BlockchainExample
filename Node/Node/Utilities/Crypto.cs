namespace Node.Utilities
{
    using System.Text;
    using Node.Models;
    using Org.BouncyCastle.Asn1.Sec;
    using Org.BouncyCastle.Asn1.X9;
    using Org.BouncyCastle.Crypto.Digests;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Crypto.Signers;
    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.Math.EC;

    public static class Crypto
    {
        static readonly X9ECParameters curve = SecNamedCurves.GetByName("secp256k1");

        public static byte[] CalculateSHA256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            Sha256Digest digest = new Sha256Digest();
            digest.BlockUpdate(bytes, 0, bytes.Length);
            byte[] result = new byte[digest.GetDigestSize()];
            digest.DoFinal(result, 0);
            return result;
        }

        public static bool IsSignatureValid(Transaction transaction)
        {
            ECPoint publicKeyPoint = GetECPublicKeyPoint(transaction.SenderPublicKey);
            ECDomainParameters ecSpec = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
            ECPublicKeyParameters publicKeyParameters = new ECPublicKeyParameters(publicKeyPoint, ecSpec);

            IDsaKCalculator kCalculator = new HMacDsaKCalculator(new Sha256Digest());
            ECDsaSigner signer = new ECDsaSigner(kCalculator);
            signer.Init(false, publicKeyParameters);
            
            byte[] tranHashWithoutSignature = CalculateSHA256(transaction.AsJsonString(false));
            //string hashString = ConvertHash(tranHashWithoutSignature);
            BigInteger signatureR = new BigInteger(transaction.SenderSignature[0], 16);
            BigInteger signatureS = new BigInteger(transaction.SenderSignature[1], 16);

            bool isValid = signer.VerifySignature(tranHashWithoutSignature, signatureR, signatureS);
            return isValid;
        }

        private static ECPoint GetECPublicKeyPoint(string encodedPublicKeyInHex)
        {
            var bigInt = new BigInteger(encodedPublicKeyInHex, 16);
            byte[] key = bigInt.ToByteArray();
            var point = curve.Curve.DecodePoint(key);
            return point;
        }

        private static string ConvertHash(byte[] hash)
        {
            var sb = new StringBuilder();
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
