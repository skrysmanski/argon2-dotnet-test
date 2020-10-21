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
            var salt = SaltProvider.CreateRandomSalt(16);
            var hashedPassword = HashPassword("Hello, World!", salt, byteLength: 40);
            Console.WriteLine(BitConverter.ToString(hashedPassword));

            var testTime = TimeSpan.FromSeconds(10);

            Console.WriteLine($"Calculating hashes per second for the next {testTime.TotalSeconds:0.} seconds...");

            var startTime = DateTime.UtcNow;

            int hashCount = 0;
            while (DateTime.UtcNow - startTime < testTime)
            {
                HashPassword("Hello, World!", salt, byteLength: 40);
                hashCount++;
            }

            var timeSpent = DateTime.UtcNow - startTime;
            Console.WriteLine($"Hashes per second: {hashCount / timeSpent.TotalSeconds:0.0}");
        }

        private static byte[] HashPassword(string password, byte[] salt, int byteLength)
        {
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // 8 = max CPU usage on CPU with 4 cores and hyper threading
            argon2.MemorySize = 130; // MB

            // This gives about 0.6 hashes per second on a Raspberry Pi 4 and about
            // 9 hashes per second on a medium desktop CPU.
            argon2.Iterations = 1000;

            return argon2.GetBytes(byteLength);
        }
    }
}
