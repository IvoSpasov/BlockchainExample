namespace Node.Utilities
{
    using System.Security.Cryptography;
    using System.Text;

    public static class Crypto
    {
        public static string Sha256(string value)
        {
            var sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}
