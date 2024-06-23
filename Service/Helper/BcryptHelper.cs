using System.Security.Cryptography;
using System.Text;

namespace Service.Helper
{
    public static class BcryptHelper
    {
        private const int workFactor = 12;
        public static String HashPassword(String value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value, workFactor);
        }
        public static bool CheckPassword(String hashPassword, String passwordValue)
        {
            var checker = BCrypt.Net.BCrypt.Verify(passwordValue, hashPassword);
            return checker;
        }
        public static string GeneratePassword()
        {
            int length = 8;
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+";
            StringBuilder result = new StringBuilder(length);
            byte[] randomBytes = new byte[length];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);

                foreach (byte b in randomBytes)
                {
                    result.Append(validChars[b % validChars.Length]);
                }
            }

            return result.ToString();
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
