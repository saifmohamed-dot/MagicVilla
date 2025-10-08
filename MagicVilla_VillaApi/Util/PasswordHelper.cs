using Microsoft.AspNetCore.Identity;

namespace MagicVilla_VillaApi.Util
{
    public static class PasswordHelper
    {
        readonly static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        public static string ComputeHashForPassword(string password)
        {
            return passwordHasher.HashPassword(null, password);
        }
        public static bool VerifyPassword(string storedHash , string entredPassword)
        {
            var res = passwordHasher.VerifyHashedPassword(null, storedHash, entredPassword);
            return res == PasswordVerificationResult.Success;
        }
    }
}
