using System;
using System.Text;

namespace TPVAXWinform_BLL.Common
{
    /// <summary>
    /// Lớp tiện ích tạo mật khẩu ngẫu nhiên
    /// </summary>
    public static class PasswordGenerator
    {
        private static readonly Random _random = new Random();
        private const string AlphanumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// Tạo mật khẩu ngẫu nhiên với độ dài chỉ định
        /// </summary>
        /// <param name="length">Độ dài mật khẩu (mặc định 6)</param>
        /// <returns>Chuỗi mật khẩu ngẫu nhiên</returns>
        public static string Generate(int length = 6)
        {
            if (length <= 0)
                throw new ArgumentException("Độ dài mật khẩu phải lớn hơn 0");

            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = _random.Next(AlphanumericChars.Length);
                sb.Append(AlphanumericChars[index]);
            }
            return sb.ToString();
        }
    }
}
