using System.Security.Cryptography;
using CheckmateAcademy.Application.Interfaces.Services;

namespace CheckmateAcademy.Application.Services
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private const int Iterations = 600_000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public string Hash(string password)
        {
            ArgumentException.ThrowIfNullOrEmpty(password);

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            return $"pbkdf2-sha256${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        public bool Verify(string password, string passwordHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(passwordHash))
                return false;

            string[] parts = passwordHash.Split('$');

            if (parts.Length != 4 || parts[0] != "pbkdf2-sha256")
                return false;

            if (!int.TryParse(parts[1], out int iterations) ||
                iterations < 100_000 ||
                iterations > 2_000_000)
            {
                return false;
            }

            try
            {
                byte[] salt = Convert.FromBase64String(parts[2]);
                byte[] expectedHash = Convert.FromBase64String(parts[3]);

                if (salt.Length != SaltSize || expectedHash.Length != HashSize)
                    return false;

                byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    iterations,
                    HashAlgorithmName.SHA256,
                    HashSize);

                return CryptographicOperations.FixedTimeEquals(
                    actualHash,
                    expectedHash);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
