using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý hóa đơn và thanh toán
    /// </summary>
    public class HoaDonController : Controller
    {
        // TODO: Inject services
        // TODO: Require authentication

        // TODO: Danh sách hóa đơn của khách hàng
        public ActionResult DanhSach()
        {
            // Load danh sách hóa đơn theo thời gian giảm dần
            // Hiển thị trạng thái: Chưa thanh toán, Đã thanh toán
            return View();
        }

        // TODO: Chi tiết hóa đơn
        public ActionResult ChiTiet(string id)
        {
            // Load thông tin hóa đơn
            // Load chi tiết các sản phẩm
            // Hiển thị khuyến mãi (nếu có)
            // Tính tổng tiền
            return View();
        }

        // TODO: Trang thanh toán (checkout)
        [HttpGet]
        public ActionResult ThanhToan()
        {
            // Load giỏ hàng
            // Cho phép nhập mã khuyến mãi
            // Hiển thị tổng tiền và các phương thức thanh toán
            return View();
        }

        // TODO: Xử lý thanh toán
        [HttpPost]
        public ActionResult ThanhToan(string phuongThucThanhToan, string maKhuyenMai)
        {
            // Tạo hóa đơn từ giỏ hàng
            // Tạo chi tiết hóa đơn
            // Xóa giỏ hàng
            // Xử lý thanh toán theo phương thức:
            //   - Tiền mặt tại trung tâm
            //   - Chuyển khoản
            //   - Ví điện tử (MoMo, ZaloPay...)
            // Gửi email xác nhận
            return RedirectToAction("ChiTiet", new { id = "maHD" });
        }

        // TODO: In hóa đơn (PDF)
        public ActionResult InHoaDon(string id)
        {
            // Generate PDF hóa đơn
            // Trả về file PDF
            return null;
        }

        // TODO: Hủy hóa đơn (nếu chưa thanh toán)
        [HttpPost]
        public JsonResult HuyHoaDon(string maHD)
        {
            // Kiểm tra trạng thái hóa đơn
            // Chỉ hủy được hóa đơn chưa thanh toán
            // Trả lại vaccine vào kho nếu cần
            return Json(new { success = true });
        }
    }
}
