using System.Security.Cryptography;
using System.Text;

namespace TPVAXWebsite.Helpers
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Hash mật khẩu sử dụng SHA256
        /// </summary>
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return ConvertToHexString(hash);
            }
        }

        /// <summary>
        /// Kiểm tra mật khẩu có khớp với hash không
        /// </summary>
        public static bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput.Equals(hash);
        }

        private static string ConvertToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
