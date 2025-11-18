using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý giỏ hàng
    /// </summary>
    public class GioHangController : Controller
    {
        // TODO: Inject services
        // TODO: Require authentication

        // TODO: Hiển thị giỏ hàng
        public ActionResult Index()
        {
            // Load giỏ hàng của khách hàng đang đăng nhập
            // Tính tổng tiền
            // Kiểm tra khuyến mãi (nếu có)
            return View();
        }

        // TODO: Thêm sản phẩm vào giỏ (AJAX)
        [HttpPost]
        public JsonResult ThemVaoGio(string maSanPham, string loaiSanPham, int soLuong = 1)
        {
            // Validate đăng nhập
            // Kiểm tra sản phẩm đã có trong giỏ chưa
            // Nếu có: tăng số lượng
            // Nếu chưa: thêm mới
            return Json(new { success = true });
        }

        // TODO: Cập nhật số lượng (AJAX)
        [HttpPost]
        public JsonResult CapNhatSoLuong(int maGH, int soLuong)
        {
            // Validate số lượng > 0
            // Cập nhật số lượng
            // Trả về tổng tiền mới
            return Json(new { success = true });
        }

        // TODO: Xóa sản phẩm khỏi giỏ (AJAX)
        [HttpPost]
        public JsonResult Xoa(int maGH)
        {
            // Xóa item khỏi giỏ hàng
            return Json(new { success = true });
        }

        // TODO: Áp dụng mã khuyến mãi (AJAX)
        [HttpPost]
        public JsonResult ApDungKhuyenMai(string maKM)
        {
            // Kiểm tra mã khuyến mãi hợp lệ
            // Tính giảm giá
            // Trả về số tiền giảm và tổng tiền sau giảm
            return Json(new { success = true });
        }
    }
}
