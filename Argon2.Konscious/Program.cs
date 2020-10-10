using System;
using System.Text;

using Argon2.Commons;

using Konscious.Security.Cryptography;

namespace Argon2.Konscious
{
    internal class Program
    {
        private static void Main()
        {
            var salt = SaltProvider.CreateNonRandomSalt(16);
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
    }
}
