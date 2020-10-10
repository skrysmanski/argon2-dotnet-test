using System.Security.Cryptography;
using System.Text;

using JetBrains.Annotations;

namespace Argon2.Commons
{
    public static class SaltProvider
    {
        [PublicAPI, MustUseReturnValue]
        public static byte[] CreateRandomSalt(int saltLength)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            var salt = new byte[saltLength];
            rngCryptoServiceProvider.GetBytes(salt);

            return salt;
        }

        [PublicAPI, MustUseReturnValue]
        public static byte[] CreateNonRandomSalt(int saltLength)
        {
            return Encoding.ASCII.GetBytes("Stet clita kasd gubergren, no se")[0..saltLength];
        }
    }
}
