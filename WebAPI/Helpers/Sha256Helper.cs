using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Helpers
{
    public static class Sha256Helper
    {
        public static string Sha256(this string input)
        {
            if (input is null) return string.Empty;

            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }
    }
}
