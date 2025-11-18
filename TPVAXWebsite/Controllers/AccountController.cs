using System.Web;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.ViewModels;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý đăng ký, đăng nhập, quản lý tài khoản khách hàng
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController()
        {
            var context = new TPVAXDbContext();
            var unitOfWork = new UnitOfWork(context);
            _accountService = new AccountService(unitOfWork);
        }

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var khachHang = _accountService.Login(model.SoDienThoai, model.MatKhau);

            if (khachHang != null)
            {
                // Tạo session
                Session["MaKH"] = khachHang.MaKH;
                Session["HoTen"] = khachHang.HoTen;
                Session["Email"] = khachHang.Email;

                // Ghi nhớ đăng nhập nếu có
                if (model.RememberMe)
                {
                    Response.Cookies.Add(new HttpCookie("MaKH", khachHang.MaKH)
                    {
                        Expires = System.DateTime.Now.AddDays(30)
                    });
                }

                // Redirect về trang trước hoặc trang chủ
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Số điện thoại hoặc mật khẩu không đúng");
            return View(model);
        }

        // GET: Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string errorMessage;
            var success = _accountService.Register(model, out errorMessage);

            if (success)
            {
                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", errorMessage);
            return View(model);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            // Clear session
            Session.Clear();
            Session.Abandon();

            // Clear cookies
            if (Request.Cookies["MaKH"] != null)
            {
                Response.Cookies["MaKH"].Expires = System.DateTime.Now.AddDays(-1);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Profile
        [Filters.CustomAuthorize]
        public ActionResult Profile()
        {
            string maKH = Session["MaKH"]?.ToString();
            if (string.IsNullOrEmpty(maKH))
            {
                return RedirectToAction("Login");
            }

            var khachHang = _accountService.GetKhachHangByMaKH(maKH);
            return View(khachHang);
        }

        // POST: Account/UpdateProfile
        [HttpPost]
        [Filters.CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile()
        {
            // TODO: Implement cập nhật profile
            return RedirectToAction("Profile");
        }

        // GET: Account/ChangePassword
        [Filters.CustomAuthorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: Account/ChangePassword
        [HttpPost]
        [Filters.CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu xác nhận không khớp");
                return View();
            }

            string maKH = Session["MaKH"]?.ToString();
            string errorMessage;
            var success = _accountService.ChangePassword(maKH, oldPassword, newPassword, out errorMessage);

            if (success)
            {
                TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                return RedirectToAction("Profile");
            }

            ModelState.AddModelError("", errorMessage);
            return View();
        }
    }
}
