using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cursos_WPF.Helpers
{
    public static class AccesosHelper
    {
        public static string GeneratePassword(
            int requiredLength = 8,
            int requiredUniqueChars = 4,
            bool requireDigit = true,
            bool requireLowercase = true,
            bool requireNonAlphanumeric = true,
            bool requireUppercase = true)
        {
            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
                };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (requireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (requireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (requireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (requireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < requiredLength
                || chars.Distinct().Count() < requiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public static byte[] GenerateLongRandomSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            return salt;
        }

        public static byte[] GenerateHashedPassword(string Password, byte[] Salt)
        {
            return Hash(Encoding.UTF8.GetBytes(Password), Salt);
        }

        public static byte[] Hash(byte[] Password, byte[] Salt)
        {
            // Salt is appended to the password to defend against dictionary attacks or brute force.
            byte[] SaltedPassword = Password.Concat(Salt).ToArray();

            return new SHA256Managed().ComputeHash(SaltedPassword);
        }

        public static bool ComparePasswords(string Password, byte[] Salt, byte[] StoredHashedPassword)
        {
            byte[] HashedPassword = GenerateHashedPassword(Password, Salt);

            return StoredHashedPassword.SequenceEqual(HashedPassword);
        }
    }
}
