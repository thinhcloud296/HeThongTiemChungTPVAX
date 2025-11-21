using System;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý đăng ký, đăng nhập, quản lý tài khoản khách hàng
    /// </summary>
    public class AccountController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();
        // GET: Account/Login
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            // TODO: Implement login logic với DAL + Services
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPost()
        {
            // TODO: Implement login logic với DAL + Services
            return View("Login");
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Kiểm tra tài khoản đã tồn tại chưa (Check SĐT)
                if (_uow.TaiKhoans.Any(t => t.MaTK == model.SoDT))
                {
                    ModelState.AddModelError("", "Số điện thoại này đã được đăng ký.");
                    return View(model);
                }

                _uow.BeginTransaction();
                try
                {

                    // --- BƯỚC 1: TẠO TÀI KHOẢN ---
                    string maTK;
                    do
                    {
                        // Gọi hàm từ thư viện Common của Web
                        maTK = TPVAXWebsite.Common.KeyGenerator.GenMaTK();
                    } while (_uow.TaiKhoans.Any(k => k.MaTK == maTK));
                    var taiKhoan = new TaiKhoan
                    {
                        MaTK = maTK, // Dùng SĐT làm username
                        MatKhau = model.MatKhau // Nên mã hóa MD5 ở đây
                    };
                    _uow.TaiKhoans.Add(taiKhoan);

                    // --- BƯỚC 2: TẠO KHÁCH HÀNG ---
                    string maKH;
                    do
                    {
                        // Gọi hàm từ thư viện Common của Web
                        maKH = TPVAXWebsite.Common.KeyGenerator.GenMaKH(model.CCCD);
                    } while (_uow.KhachHangs.Any(k => k.MaKH == maKH));
                    var khachHang = new KhachHang
                    {
                        MaKH = maKH,
                        HoTen = model.HoTen,
                        CCCD = model.CCCD,
                        NgaySinh = model.NgaySinh,
                        SoDT = model.SoDT,
                        Email = model.Email,
                        MaTK = taiKhoan.MaTK
                    };
                    _uow.KhachHangs.Add(khachHang);

                    // --- BƯỚC 3: XỬ LÝ LIÊN KẾT HỒ SƠ (LOGIC THÔNG MINH) ---

                    // Kiểm tra xem CCCD này đã từng tiêm chủng chưa?
                    var hoSoCu = _uow.HoSoTiemChungs
                        .FirstOrDefault(h => h.CCCD == model.CCCD);

                    string maHSTC_CanLienKet;

                    if (hoSoCu != null)
                    {
                        // CASE A: ĐÃ CÓ HỒ SƠ -> LIÊN KẾT LẠI
                        // Đây là logic bạn yêu cầu: Khách đã có hồ sơ thì link vào, không tạo mới
                        maHSTC_CanLienKet = hoSoCu.MaHSTC;
                    }
                    else
                    {
                        // CASE B: CHƯA CÓ HỒ SƠ -> TẠO MỚI
                        maHSTC_CanLienKet = "HS" + DateTime.Now.ToString("yyMMddHHmm");
                        var hoSoMoi = new HoSoTiemChung
                        {
                            MaHSTC = maHSTC_CanLienKet,
                            HoTen = model.HoTen,
                            CCCD = model.CCCD,
                            NgaySinh = model.NgaySinh,
                            GioiTinh = "Chưa rõ", // Hoặc thêm dropdown chọn giới tính ở View
                            TrangThai = true
                        };
                        _uow.HoSoTiemChungs.Add(hoSoMoi);
                    }

                    // --- BƯỚC 4: TẠO LIÊN KẾT (BẢNG LienKetHoSo) ---
                    var lienKet = new LienKetHoSo
                    {
                        MaLK = "LK" + DateTime.Now.Ticks.ToString().Substring(12),
                        MaKH = maKH,
                        MaHSTC = maHSTC_CanLienKet,
                        VaiTro = "Bản thân", // Đánh dấu đây là hồ sơ chính chủ
                        NgayLienKet = DateTime.Now
                    };
                    _uow.LienKetHoSos.Add(lienKet);

                    // Lưu tất cả
                    _uow.Commit();

                    TempData["SuccessMessage"] = "Đăng ký thành công! " +
                        (hoSoCu != null ? "Hệ thống đã tìm thấy và liên kết hồ sơ tiêm chủng cũ của bạn." : "");

                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    _uow.Rollback();
                    ModelState.AddModelError("", "Lỗi đăng ký: " + ex.Message);
                }
            }
            return View(model);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            // TODO: Clear session với Services
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Dashboard
        public ActionResult Dashboard()
        {
            // TODO: Check login and load data với DAL + Services
            return View();
        }
    }
}
