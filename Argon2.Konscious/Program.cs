using System;
using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;

using JetBrains.Annotations;

using Konscious.Security.Cryptography;

namespace Argon2.Konscious
{
    internal class Program
    {
        private static void Main()
        {
            var salt = CreateSalt(32);
            var hashedPassword = HashPassword("Hello, World!", salt, byteLength: 40);

            Console.WriteLine(BitConverter.ToString(hashedPassword));
        }

        private static byte[] HashPassword(string password, byte[] salt, int byteLength)
        {
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 2;
            argon2.Iterations = 4;
            argon2.MemorySize = 130; // MB

            return argon2.GetBytes(byteLength);
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
