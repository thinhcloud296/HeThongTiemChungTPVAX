using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPVAXWebsite.Common;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;


namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// PaymentController - Xử lý thanh toán qua cổng VNPAY
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
    /// 
    /// VNPAY Sandbox Credentials:
    /// - vnp_TmnCode: 4Z9J8W1F
    /// - vnp_HashSecret: T9GIA2J4ACXZ76RYHWR6QME84JZEDD1C
    /// - vnp_Url: https://sandbox.vnpayment.vn/paymentv2/vpcpay.html
    /// </summary>
    public class PaymentController : Controller
    {
        private readonly TPVAXDbContext _context = new TPVAXDbContext();

        // ============================================================
        // VNPAY Configuration - Sandbox Environment
        // ============================================================
        private const string vnp_TmnCode = "4Z9J8W1F";
        private const string vnp_HashSecret = "T9GIA2J4ACXZ76RYHWR6QME84JZEDD1C";
        private const string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        // vnp_ReturnUrl sẽ được tạo động dựa trên domain hiện tại

        /// <summary>
        /// GET: Payment/VnPayPayment
        /// Tạo URL thanh toán và redirect người dùng đến VNPAY
        /// Demo với đơn hàng mẫu: 100,000 VND
        /// </summary>
        public ActionResult VnPayPayment()
        {
            // ============================================================
            // TẠO ĐƠN HÀNG MẪU (DUMMY ORDER)
            // Trong thực tế, lấy thông tin từ giỏ hàng hoặc session
            // ============================================================
            decimal amount = 100000; // 100,000 VND
            string orderInfo = "Thanh toan vaccine";
            string orderId = DateTime.Now.Ticks.ToString(); // Mã đơn hàng unique

            // ============================================================
            // TẠO DYNAMIC RETURN URL
            // Auto-detect domain hiện tại (localhost hoặc production)
            // ============================================================
            // Request.Url.Scheme: http hoặc https
            // Request.Url.Authority: localhost:port hoặc tpvax.site
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
            string vnp_ReturnUrl = baseUrl + Url.Action("PaymentCallback", "Payment");

            // ============================================================
            // KHỞI TẠO VNPAY LIBRARY VÀ THÊM CÁC THAM SỐ
            // ============================================================
            VnPayLibrary vnpay = new VnPayLibrary();

            // Mã website của merchant trên hệ thống VNPAY
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);

            // ============================================================
            // QUAN TRỌNG: vnp_Amount phải nhân 100
            // VNPAY yêu cầu số tiền phải được nhân 100 (không có dấu phẩy)
            // Ví dụ: 100,000 VND => gửi 10000000
            // Lý do: VNPAY lưu trữ số tiền dưới dạng số nguyên với 2 chữ số thập phân
            // ============================================================
            long amountInVnpayFormat = (long)(amount * 100);
            vnpay.AddRequestData("vnp_Amount", amountInVnpayFormat.ToString());

            // Thời gian tạo giao dịch (định dạng: yyyyMMddHHmmss)
            // FIX: Sử dụng múi giờ Việt Nam (UTC+7) thay vì DateTime.Now (UTC trên Azure)
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, 
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            vnpay.AddRequestData("vnp_CreateDate", vietnamTime.ToString("yyyyMMddHHmmss"));

            // Đơn vị tiền tệ (VND)
            vnpay.AddRequestData("vnp_CurrCode", "VND");

            // Địa chỉ IP của khách hàng
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(HttpContext));

            // Ngôn ngữ giao diện thanh toán (vn: Tiếng Việt, en: English)
            vnpay.AddRequestData("vnp_Locale", "vn");

            // Thông tin mô tả đơn hàng
            vnpay.AddRequestData("vnp_OrderInfo", orderInfo);

            // Loại đơn hàng (other: khác, billpayment: thanh toán hóa đơn, ...)
            vnpay.AddRequestData("vnp_OrderType", "other");

            // URL để VNPAY redirect về sau khi thanh toán
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);

            // Mã tham chiếu giao dịch (unique cho mỗi giao dịch)
            vnpay.AddRequestData("vnp_TxnRef", orderId);

            // Thời gian hết hạn thanh toán (15 phút từ thời điểm tạo)
            // FIX: Sử dụng múi giờ Việt Nam (UTC+7)
            vnpay.AddRequestData("vnp_ExpireDate", vietnamTime.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            // ============================================================
            // TẠO URL THANH TOÁN VÀ REDIRECT
            // ============================================================
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return Redirect(paymentUrl);
        }

        /// <summary>
        /// GET: Payment/VnPayPaymentForOrder
        /// Thanh toán cho hóa đơn thực tế từ hệ thống
        /// </summary>
        /// <param name="maHD">Mã hóa đơn cần thanh toán</param>
        public ActionResult VnPayPaymentForOrder(string maHD)
        {
            if (string.IsNullOrEmpty(maHD))
            {
                TempData["ErrorMessage"] = "Mã hóa đơn không hợp lệ.";
                return RedirectToAction("Index", "HoaDon");
            }

            // Lấy thông tin hóa đơn từ database
            var hoaDon = _context.HoaDons.Find(maHD);
            if (hoaDon == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy hóa đơn.";
                return RedirectToAction("Index", "HoaDon");
            }

            // Kiểm tra hóa đơn đã thanh toán chưa
            if (hoaDon.TrangThai == true)
            {
                TempData["ErrorMessage"] = "Hóa đơn này đã được thanh toán.";
                return RedirectToAction("ChiTiet", "HoaDon", new { id = maHD });
            }

            // Lấy số tiền từ hóa đơn
            decimal amount = hoaDon.TongTien;
            string orderInfo = $"Thanh toan hoa don {maHD} - He thong tiem chung TPVAX";

            // Tạo dynamic return URL
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
            string vnp_ReturnUrl = baseUrl + Url.Action("PaymentCallback", "Payment");

            // Khởi tạo VNPAY Library
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);

            // vnp_Amount phải nhân 100 (VNPAY requirement)
            long amountInVnpayFormat = (long)(amount * 100);
            vnpay.AddRequestData("vnp_Amount", amountInVnpayFormat.ToString());

            // FIX: Sử dụng múi giờ Việt Nam (UTC+7) thay vì DateTime.Now (UTC trên Azure)
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, 
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            vnpay.AddRequestData("vnp_CreateDate", vietnamTime.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(HttpContext));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", orderInfo);
            vnpay.AddRequestData("vnp_OrderType", "billpayment");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);

            // Sử dụng mã hóa đơn làm TxnRef để có thể cập nhật trạng thái sau
            vnpay.AddRequestData("vnp_TxnRef", maHD);

            // FIX: Sử dụng múi giờ Việt Nam (UTC+7)
            vnpay.AddRequestData("vnp_ExpireDate", vietnamTime.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return Redirect(paymentUrl);
        }

        /// <summary>
        /// GET: Payment/PaymentCallback
        /// Nhận kết quả thanh toán từ VNPAY và xử lý
        /// </summary>
        public ActionResult PaymentCallback()
        {
            // ============================================================
            // NHẬN VÀ XỬ LÝ RESPONSE TỪ VNPAY
            // ============================================================
            VnPayLibrary vnpay = new VnPayLibrary();

            // Lấy tất cả query parameters từ URL callback
            foreach (string key in Request.QueryString.AllKeys)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, Request.QueryString[key]);
                }
            }

            // Lấy các thông tin quan trọng từ response
            string vnp_TxnRef = vnpay.GetResponseData("vnp_TxnRef");           // Mã đơn hàng
            string vnp_TransactionNo = vnpay.GetResponseData("vnp_TransactionNo"); // Mã giao dịch VNPAY
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");   // Mã phản hồi
            string vnp_SecureHash = Request.QueryString["vnp_SecureHash"];         // Chữ ký bảo mật
            string vnp_Amount = vnpay.GetResponseData("vnp_Amount");               // Số tiền
            string vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");         // Thông tin đơn hàng
            string vnp_BankCode = vnpay.GetResponseData("vnp_BankCode");           // Mã ngân hàng
            string vnp_PayDate = vnpay.GetResponseData("vnp_PayDate");             // Thời gian thanh toán

            // ============================================================
            // XÁC THỰC CHỮ KÝ (SIGNATURE VALIDATION)
            // Đảm bảo response không bị giả mạo
            // ============================================================
            bool isValidSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

            if (isValidSignature)
            {
                // ============================================================
                // KIỂM TRA MÃ PHẢN HỒI (RESPONSE CODE)
                // 00: Giao dịch thành công
                // Các mã khác: Giao dịch thất bại
                // ============================================================
                if (vnp_ResponseCode == "00")
                {
                    // GIAO DỊCH THÀNH CÔNG
                    // Cập nhật trạng thái đơn hàng trong database (nếu cần)
                    UpdateOrderStatus(vnp_TxnRef, true, vnp_TransactionNo);

                    // Chuyển đổi số tiền (chia 100 vì VNPAY nhân 100)
                    decimal amount = 0;
                    if (long.TryParse(vnp_Amount, out long amountLong))
                    {
                        amount = amountLong / 100m;
                    }

                    ViewBag.IsSuccess = true;
                    ViewBag.Message = "Thanh toán thành công!";
                    ViewBag.TransactionId = vnp_TransactionNo;
                    ViewBag.OrderId = vnp_TxnRef;
                    ViewBag.Amount = amount.ToString("N0") + " VND";
                    ViewBag.OrderInfo = vnp_OrderInfo;
                    ViewBag.BankCode = vnp_BankCode;
                    ViewBag.PayDate = vnp_PayDate;
                }
                else
                {
                    // GIAO DỊCH THẤT BẠI
                    // Cập nhật trạng thái đơn hàng (nếu cần)
                    UpdateOrderStatus(vnp_TxnRef, false, vnp_TransactionNo);

                    ViewBag.IsSuccess = false;
                    ViewBag.Message = "Thanh toán thất bại!";
                    ViewBag.ErrorCode = vnp_ResponseCode;
                    ViewBag.ErrorMessage = GetResponseMessage(vnp_ResponseCode);
                    ViewBag.OrderId = vnp_TxnRef;
                }
            }
            else
            {
                // CHỮ KÝ KHÔNG HỢP LỆ - Có thể bị tấn công
                ViewBag.IsSuccess = false;
                ViewBag.Message = "Chữ ký không hợp lệ!";
                ViewBag.ErrorMessage = "Giao dịch có thể đã bị giả mạo. Vui lòng liên hệ hỗ trợ.";
            }

            return View();
        }

        /// <summary>
        /// Cập nhật trạng thái đơn hàng sau khi thanh toán
        /// </summary>
        private void UpdateOrderStatus(string orderId, bool isSuccess, string transactionNo)
        {
            try
            {
                // Kiểm tra xem orderId có phải là mã hóa đơn trong hệ thống không
                var hoaDon = _context.HoaDons.Find(orderId);
                if (hoaDon != null && isSuccess)
                {
                    hoaDon.TrangThai = true; // Đã thanh toán
                    // Có thể lưu thêm transactionNo vào một trường khác nếu cần
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                System.Diagnostics.Debug.WriteLine($"Error updating order status: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy thông báo lỗi dựa trên mã phản hồi từ VNPAY
        /// </summary>
        private string GetResponseMessage(string responseCode)
        {
            switch (responseCode)
            {
                case "00": return "Giao dịch thành công";
                case "07": return "Trừ tiền thành công. Giao dịch bị nghi ngờ (liên quan tới lừa đảo, giao dịch bất thường)";
                case "09": return "Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng chưa đăng ký dịch vụ InternetBanking tại ngân hàng";
                case "10": return "Giao dịch không thành công do: Khách hàng xác thực thông tin thẻ/tài khoản không đúng quá 3 lần";
                case "11": return "Giao dịch không thành công do: Đã hết hạn chờ thanh toán. Xin quý khách vui lòng thực hiện lại giao dịch";
                case "12": return "Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng bị khóa";
                case "13": return "Giao dịch không thành công do: Quý khách nhập sai mật khẩu xác thực giao dịch (OTP)";
                case "24": return "Giao dịch không thành công do: Khách hàng hủy giao dịch";
                case "51": return "Giao dịch không thành công do: Tài khoản của quý khách không đủ số dư để thực hiện giao dịch";
                case "65": return "Giao dịch không thành công do: Tài khoản của Quý khách đã vượt quá hạn mức giao dịch trong ngày";
                case "75": return "Ngân hàng thanh toán đang bảo trì";
                case "79": return "Giao dịch không thành công do: KH nhập sai mật khẩu thanh toán quá số lần quy định";
                case "99": return "Các lỗi khác (lỗi còn lại, không có trong danh sách mã lỗi đã liệt kê)";
                default: return $"Lỗi không xác định (Mã: {responseCode})";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
