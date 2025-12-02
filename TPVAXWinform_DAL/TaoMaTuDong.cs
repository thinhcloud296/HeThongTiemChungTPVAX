using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class TaoMaTuDong
    {
        private static long _counter = 0;
        private static readonly object _lock = new object();

        /// <summary>
        /// Tạo mã unique dựa trên timestamp và counter (thread-safe)
        /// Format: PREFIX + YYMMDD + HHMMSS + 4 số counter
        /// </summary>
        private static string GenerateUniqueCode(string prefix, int maxLength = 10)
        {
            lock (_lock)
            {
                // Increment counter (reset nếu quá lớn)
                _counter = (_counter + 1) % 10000;

                // Tạo mã: PREFIX + timestamp ngắn + counter
                string timestamp = DateTime.Now.ToString("MMddHHmmss"); // 10 ký tự
                string counterStr = _counter.ToString("D4"); // 4 ký tự

                // Tính toán độ dài còn lại cho timestamp
                int prefixLen = prefix.Length;
                int remainingLen = maxLength - prefixLen;

                if (remainingLen >= 8)
                {
                    // Đủ chỗ: lấy 4 số cuối timestamp + 4 số counter
                    return prefix + timestamp.Substring(6) + counterStr;
                }
                else
                {
                    // Không đủ chỗ: dùng Guid ngắn
                    string guid = Guid.NewGuid().ToString("N").Substring(0, remainingLen);
                    return prefix + guid;
                }
            }
        }

        /// <summary>
        /// Tạo mã khách hàng - Format: KH + 8 ký tự unique
        /// </summary>
        public string GenMaKH(string CCCD = null)
        {
            return GenerateUniqueCode("KH", 10);
        }

        /// <summary>
        /// Tạo mã liên kết hồ sơ - Format: LKHS + 6 ký tự unique
        /// </summary>
        public string GenMaLK(string CCCD = null)
        {
            return GenerateUniqueCode("LKHS", 10);
        }

        /// <summary>
        /// Tạo mã hồ sơ tiêm chủng - Format: HSTC + 6 ký tự unique
        /// </summary>
        public string GenMaHSTC(string CCCD = null)
        {
            return GenerateUniqueCode("HSTC", 10);
        }

        /// <summary>
        /// Tạo mã tài khoản - Format: TK + 8 ký tự unique
        /// </summary>
        public string GenMaTK()
        {
            return GenerateUniqueCode("TK", 10);
        }

        /// <summary>
        /// Tạo mã hóa đơn - Format: HD + 8 ký tự unique
        /// </summary>
        public  string GenMaHD()
        {
            return GenerateUniqueCode("HD", 10);
        }

        /// <summary>
        /// Tạo mã chi tiết hóa đơn - Format: CTHD + 6 ký tự unique
        /// </summary>
        public  string GenMaCTHD()
        {
            return GenerateUniqueCode("CTHD", 10);
        }

        /// <summary>
        /// Tạo mã lịch tiêm - Format: LT + 8 ký tự unique
        /// </summary>
        public  string GenMaLT()
        {
            return GenerateUniqueCode("LT", 10);
        }
    }
}
