using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TPVAXWebsite.Common
{
    /// <summary>
    /// VnPayLibrary - Helper class để tích hợp cổng thanh toán VNPAY
    /// 
    /// ============================================================
    /// THÔNG TIN THẺ TEST (VNPAY Sandbox)
    /// ============================================================
    /// Ngân hàng: NCB
    /// Số thẻ: 9704198526191432198
    /// Tên chủ thẻ: NGUYEN VAN A
    /// Ngày phát hành: 07/15
    /// Mật khẩu OTP: 123456
    /// ============================================================
    /// </summary>
    public class VnPayLibrary
    {
        // SortedList để lưu trữ các tham số request theo thứ tự alphabet
        // VNPAY yêu cầu các tham số phải được sắp xếp theo thứ tự alphabet khi tạo chữ ký
        private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
        private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());

        #region Request Methods

        /// <summary>
        /// Thêm tham số vào request data
        /// </summary>
        /// <param name="key">Tên tham số</param>
        /// <param name="value">Giá trị tham số</param>
        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _requestData.Add(key, value);
            }
        }

        /// <summary>
        /// Tạo URL redirect đến VNPAY với query string và chữ ký bảo mật
        /// </summary>
        /// <param name="baseUrl">URL cổng thanh toán VNPAY</param>
        /// <param name="vnp_HashSecret">Secret key để tạo chữ ký HMACSHA512</param>
        /// <returns>URL hoàn chỉnh để redirect người dùng</returns>
        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            StringBuilder data = new StringBuilder();

            // Duyệt qua tất cả tham số đã được sắp xếp theo alphabet
            foreach (KeyValuePair<string, string> kv in _requestData)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    // URL encode giá trị để đảm bảo an toàn khi truyền qua URL
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }

            // Tạo query string (bỏ ký tự & cuối cùng)
            string queryString = data.ToString();
            if (queryString.Length > 0)
            {
                queryString = queryString.Remove(queryString.Length - 1, 1);
            }

            // Tạo URL cơ bản
            string signData = queryString;
            string vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, signData);

            // Thêm chữ ký vào URL
            string paymentUrl = baseUrl + "?" + queryString + "&vnp_SecureHash=" + vnp_SecureHash;

            return paymentUrl;
        }

        #endregion

        #region Response Methods

        /// <summary>
        /// Thêm tham số từ response của VNPAY
        /// </summary>
        /// <param name="key">Tên tham số</param>
        /// <param name="value">Giá trị tham số</param>
        public void AddResponseData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _responseData.Add(key, value);
            }
        }

        /// <summary>
        /// Lấy giá trị tham số từ response
        /// </summary>
        /// <param name="key">Tên tham số</param>
        /// <returns>Giá trị tham số hoặc chuỗi rỗng nếu không tồn tại</returns>
        public string GetResponseData(string key)
        {
            return _responseData.TryGetValue(key, out string value) ? value : string.Empty;
        }

        /// <summary>
        /// Xác thực chữ ký từ response của VNPAY
        /// Sử dụng thuật toán HMACSHA512 để verify
        /// </summary>
        /// <param name="inputHash">Chữ ký nhận được từ VNPAY (vnp_SecureHash)</param>
        /// <param name="secretKey">Secret key của merchant</param>
        /// <returns>True nếu chữ ký hợp lệ, False nếu không hợp lệ</returns>
        public bool ValidateSignature(string inputHash, string secretKey)
        {
            // Tạo lại chuỗi dữ liệu từ response (không bao gồm vnp_SecureHash và vnp_SecureHashType)
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in _responseData)
            {
                if (!string.IsNullOrEmpty(kv.Value) 
                    && kv.Key != "vnp_SecureHash" 
                    && kv.Key != "vnp_SecureHashType")
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }

            // Bỏ ký tự & cuối cùng
            string signData = data.ToString();
            if (signData.Length > 0)
            {
                signData = signData.Remove(signData.Length - 1, 1);
            }

            // Tạo chữ ký từ dữ liệu response
            string myChecksum = Utils.HmacSHA512(secretKey, signData);

            // So sánh chữ ký (không phân biệt hoa thường)
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }

    /// <summary>
    /// Comparer để sắp xếp các tham số theo thứ tự alphabet
    /// VNPAY yêu cầu các tham số phải được sắp xếp theo thứ tự này
    /// </summary>
    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }

    /// <summary>
    /// Utils class chứa các hàm tiện ích cho VNPAY
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Tạo chữ ký HMACSHA512
        /// VNPAY sử dụng thuật toán HMACSHA512 để bảo mật giao dịch
        /// </summary>
        /// <param name="key">Secret key</param>
        /// <param name="inputData">Dữ liệu cần ký</param>
        /// <returns>Chuỗi hash dạng hex</returns>
        public static string HmacSHA512(string key, string inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);

            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        /// <summary>
        /// Lấy địa chỉ IP của client
        /// </summary>
        /// <param name="context">HttpContext hiện tại</param>
        /// <returns>Địa chỉ IP</returns>
        public static string GetIpAddress(HttpContextBase context)
        {
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0].Trim();
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"] ?? context.Request.UserHostAddress ?? "127.0.0.1";
        }
    }
}
