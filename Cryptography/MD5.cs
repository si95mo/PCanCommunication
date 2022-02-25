using System.Text;
using Crypto = System.Security.Cryptography.MD5;

namespace Cryptography
{
    /// <summary>
    /// Implement an MD5 hash calculus
    /// </summary>
    public static class MD5
    {
        /// <summary>
        /// Create a new MD5 cryptography
        /// </summary>
        /// <param name="input">The input to encrypt</param>
        /// <returns>The encrypted string</returns>
        public static string CreateNew(string input)
        {
            string output = "";

            // Use input string to calculate MD5 hash
            using (Crypto md5 = Crypto.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    sb.Append(hashBytes[i].ToString("X2"));

                output = sb.ToString();
            }

            return output;
        }
    }
}