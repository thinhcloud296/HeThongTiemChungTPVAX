using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý lịch tiêm chủng
    /// </summary>
    public class LichTiemController : Controller
    {
        // TODO: Inject services
        // TODO: Require authentication

        // TODO: Hiển thị lịch sử tiêm chủng của tất cả hồ sơ
        public ActionResult LichSuTiemChung()
        {
            // Load tất cả hồ sơ tiêm chủng liên kết với khách hàng
            // Load lịch tiêm của từng hồ sơ
            // Hiển thị theo timeline
            return View();
        }

        // TODO: Form đặt lịch tiêm mới
        [HttpGet]
        public ActionResult DatLich(string maVC)
        {
            // Load danh sách hồ sơ để chọn (bản thân, con, người thân...)
            // Load thông tin vaccine nếu có maVC
            // Hiển thị form chọn ngày tiêm
            return View();
        }

        // TODO: Xử lý đặt lịch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatLich()
        {
            // Validate thông tin
            // Tạo lịch tiêm mới
            // Gửi email/SMS xác nhận (optional)
            return RedirectToAction("LichSuTiemChung");
        }

        // TODO: Hủy lịch tiêm
        [HttpPost]
        public JsonResult HuyLich(string maLT)
        {
            // Kiểm tra quyền (chỉ hủy được lịch của mình)
            // Kiểm tra thời gian (có thể quy định thời gian tối thiểu trước khi hủy)
            // Cập nhật trạng thái hoặc xóa
            return Json(new { success = true });
        }

        // TODO: Quản lý hồ sơ tiêm chủng
        public ActionResult QuanLyHoSo()
        {
            // Hiển thị danh sách hồ sơ của khách hàng
            // Cho phép thêm hồ sơ cho người thân
            return View();
        }

        // TODO: Thêm hồ sơ người thân
        [HttpGet]
        public ActionResult ThemHoSo()
        {
            return View();
        }

        // TODO: Xử lý thêm hồ sơ
        [HttpPost]
        public ActionResult ThemHoSo()
        {
            // Tạo HoSoTiemChung mới
            // Tạo LienKetHoSo với vai trò (con, bố mẹ, vợ/chồng...)
            return RedirectToAction("QuanLyHoSo");
        }
    }
}
