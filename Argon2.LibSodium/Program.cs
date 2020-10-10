using System;
using System.Text;

using Argon2.Commons;

using Sodium;

namespace Argon2.LibSodium
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
            return PasswordHash.ArgonHashBinary(
                Encoding.UTF8.GetBytes(password),
                salt,
                opsLimit: 4,
                memLimit: 130_000_000,
                byteLength,
                PasswordHash.ArgonAlgorithm.Argon_2ID13
            );
        }
    }
}
