using System;
using System.Security.Cryptography;
using System.Text;

using JetBrains.Annotations;

using Sodium;

namespace Argon2.LibSodium
{
    internal class Program
    {
        private static void Main()
        {
            var salt = CreateSalt(16);
            var hashedPassword = HashPassword("Hello, World!", salt, byteLength: 40);

            Console.WriteLine(BitConverter.ToString(hashedPassword));
        }

        private static byte[] HashPassword(string password, byte[] salt, int byteLength)
        {
            return PasswordHash.ArgonHashBinary(
                Encoding.UTF8.GetBytes(password),
                salt,
                opsLimit: 4,
                memLimit: 130_000_000,
                byteLength,
                PasswordHash.ArgonAlgorithm.Argon_2ID13
            );
        }

        [MustUseReturnValue]
        private static byte[] CreateSalt(int saltLength)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            var salt = new byte[saltLength];
            rngCryptoServiceProvider.GetBytes(salt);

            return salt;
        }
    }
}
