using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly TPVAXDbContext _context = new TPVAXDbContext();

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Kiểm tra Khách hàng
            var kh = _context.KhachHangs
                .FirstOrDefault(k => k.Email == model.Identifier || k.SoDT == model.Identifier);

            if (kh != null)
            {
                var tk = _context.TaiKhoans
                    .FirstOrDefault(t => t.MaTK == kh.MaTK && t.MatKhau == model.MatKhau);

                if (tk != null)
                {
                    Session["User"] = tk.MaTK;
                    Session["KH"] = kh;
                    return RedirectToAction("Index", "Home");
                }
            }

            // Kiểm tra Nhân viên
            var nv = _context.NhanViens
                .FirstOrDefault(n => n.Email == model.Identifier || n.SoDT == model.Identifier);

            if (nv != null)
            {
                var tk = _context.TaiKhoans
                    .FirstOrDefault(t => t.MaTK == nv.MaTK && t.MatKhau == model.MatKhau);

                if (tk != null)
                {
                    Session["User"] = tk.MaTK;
                    Session["NV"] = nv;
                    return RedirectToAction("Index", "Admin");
                }
            }

            ModelState.AddModelError("", "Sai thông tin đăng nhập hoặc mật khẩu.");
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
                return View(model);

            var exists = _context.KhachHangs.Any(k => k.Email == model.Email || k.SoDT == model.SoDT);
            if (exists)
            {
                ModelState.AddModelError("", "Email hoặc số điện thoại đã được đăng ký.");
                return View(model);
            }

            if (string.IsNullOrEmpty(model.SoDT) || model.SoDT.Length < 4)
            {
                ModelState.AddModelError("", "Số điện thoại không hợp lệ.");
                return View(model);
            }

            var maTK = "TK" + model.SoDT.Substring(model.SoDT.Length - 4);
            var maKH = "KH" + maTK.Substring(2);

            var tk = new TaiKhoan
            {
                MaTK = maTK,
                MatKhau = model.MatKhau
            };

            var kh = new KhachHang
            {
                MaKH = maKH,
                HoTen = model.HoTen,
                CCCD = model.CCCD,
                NgaySinh = model.NgaySinh,
                GioiTinh = model.GioiTinh,
                DiaChi = model.DiaChi,
                SoDT = model.SoDT,
                Email = model.Email,
                MaTK = maTK
            };

            try
            {
                _context.TaiKhoans.Add(tk);
                _context.KhachHangs.Add(kh);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + ex.Message);
                return View(model);
            }

            ViewBag.SuccessMessage = "Đăng ký thành công! Vui lòng đăng nhập để sử dụng hệ thống.";
            return View();
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
