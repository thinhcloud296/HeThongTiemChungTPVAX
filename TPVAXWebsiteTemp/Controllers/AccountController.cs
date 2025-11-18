using System.Web.Mvc;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý đăng ký, đăng nhập, quản lý tài khoản khách hàng
    /// </summary>
    public class AccountController : Controller
    {
        // TODO: Inject IUnitOfWork hoặc services

        // TODO: Implement đăng nhập
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            // Hiển thị form đăng nhập
            return View();
        }

        // TODO: Xử lý đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            // 1. Validate model
            // 2. Tìm khách hàng theo số điện thoại
            // 3. Verify password (sử dụng BCrypt hoặc SHA256)
            // 4. Tạo session/cookie
            // 5. Redirect về trang trước hoặc trang chủ
            return View(model);
        }

        // TODO: Implement đăng ký tài khoản mới
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // TODO: Xử lý đăng ký
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            // 1. Validate model
            // 2. Kiểm tra CCCD/SĐT đã tồn tại chưa
            // 3. Hash password
            // 4. Tạo TaiKhoan mới
            // 5. Tạo KhachHang mới
            // 6. Tạo HoSoTiemChung cho bản thân
            // 7. Tạo LienKetHoSo
            // 8. Redirect đến trang login
            return View(model);
        }

        // TODO: Implement đăng xuất
        public ActionResult Logout()
        {
            // Clear session và cookie
            return RedirectToAction("Index", "Home");
        }

        // TODO: Implement trang profile
        public ActionResult Profile()
        {
            // Hiển thị thông tin cá nhân
            return View();
        }

        // TODO: Implement cập nhật profile
        [HttpPost]
        public ActionResult UpdateProfile()
        {
            // Cập nhật thông tin cá nhân
            return RedirectToAction("Profile");
        }

        // TODO: Implement đổi mật khẩu
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}
