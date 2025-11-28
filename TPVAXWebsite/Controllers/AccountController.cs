using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;
using BCrypt.Net;
using System.Collections.Generic;

namespace TPVAXWebsite.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();
        private readonly TPVAXDbContext _context = new TPVAXDbContext();

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                if (Session["KH"] != null)
                    return RedirectToAction("Dashboard", "Account");
                if (Session["NV"] != null)
                    return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var kh = _context.KhachHangs
                    .FirstOrDefault(k => k.SoDT == model.TenDangNhap || k.Email == model.TenDangNhap);

                if (kh != null && !string.IsNullOrEmpty(kh.MaTK))
                {
                    var tk = _context.TaiKhoans.FirstOrDefault(t => t.MaTK == kh.MaTK);
                    
                    if (tk != null && BCrypt.Net.BCrypt.Verify(model.MatKhau, tk.MatKhau))
                    {
                        Session["User"] = tk.MaTK;
                        Session["KH"] = kh;
                        Session["UserRole"] = "Customer";
                        
                        TempData["SuccessMessage"] = $"Chào mừng {kh.HoTen}!";
                        return RedirectToAction("Dashboard", "Account");
                    }
                }

                var nv = _context.NhanViens
                    .FirstOrDefault(n => n.SoDT == model.TenDangNhap || n.Email == model.TenDangNhap);

                if (nv != null && !string.IsNullOrEmpty(nv.MaTK))
                {
                    var tk = _context.TaiKhoans.FirstOrDefault(t => t.MaTK == nv.MaTK);
                    
                    if (tk != null && BCrypt.Net.BCrypt.Verify(model.MatKhau, tk.MatKhau))
                    {
                        Session["User"] = tk.MaTK;
                        Session["NV"] = nv;
                        Session["UserRole"] = "Staff";
                        
                        TempData["SuccessMessage"] = $"Chào mừng {nv.HoTen}!";
                        return RedirectToAction("Index", "Admin");
                    }
                }

                ModelState.AddModelError("", "Thông tin đăng nhập hoặc mật khẩu không chính xác.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi đăng nhập: " + ex.Message);
            }

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

            try
            {
                // B2: Dò CCCD trong bảng KhachHang - kiểm tra tài khoản đã tồn tại chưa
                if (_context.KhachHangs.Any(k => k.CCCD == model.CCCD))
                {
                    ModelState.AddModelError("CCCD", "CCCD này đã được đăng ký tài khoản. Vui lòng đăng nhập.");
                    return View(model);
                }

                // Kiểm tra SĐT và Email trùng
                if (_context.KhachHangs.Any(k => k.SoDT == model.SoDT))
                {
                    ModelState.AddModelError("SoDT", "Số điện thoại này đã được đăng ký.");
                    return View(model);
                }

                if (!string.IsNullOrEmpty(model.Email) && 
                    _context.KhachHangs.Any(k => k.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email này đã được đăng ký.");
                    return View(model);
                }

                // B2: Dò CCCD trong bảng HoSoTiemChung - kiểm tra hồ sơ đã có sẵn chưa
                var hoSoCu = _context.HoSoTiemChungs.FirstOrDefault(h => h.CCCD == model.CCCD);
                
                _uow.BeginTransaction();

                string maTK;
                do
                {
                    maTK = TPVAXWebsite.Common.KeyGenerator.GenMaTK();
                } while (_uow.TaiKhoans.Any(k => k.MaTK == maTK));

                var taiKhoan = new TaiKhoan
                {
                    MaTK = maTK,
                    MatKhau = BCrypt.Net.BCrypt.HashPassword(model.MatKhau)
                };
                _uow.TaiKhoans.Add(taiKhoan);
                _uow.SaveChanges(); // Insert TaiKhoan TRƯỚC để tránh FK constraint

                string maKH;
                do
                {
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
                    GioiTinh = model.GioiTinh,
                    DiaChi = model.DiaChi,
                    MaTK = taiKhoan.MaTK
                };
                _uow.KhachHangs.Add(khachHang);
                _uow.SaveChanges(); // Save KhachHang

                // Xử lý Hồ sơ tiêm chủng và Liên kết
                string maHSTC_CanLienKet;
                string messageDetail;

                if (hoSoCu != null)
                {
                    // TRƯỜNG HỢP ĐẶC BIỆT: Hồ sơ đã tồn tại, tự động liên kết
                    maHSTC_CanLienKet = hoSoCu.MaHSTC;
                    messageDetail = $"Hệ thống đã tìm thấy và tự động liên kết với hồ sơ tiêm chủng có sẵn (Họ tên: {hoSoCu.HoTen}).";
                }
                else
                {
                    // TRƯỜNG HỢP 1: Tạo hồ sơ tiêm chủng mới
                    maHSTC_CanLienKet = TPVAXWebsite.Common.KeyGenerator.GenMaHSTC(model.CCCD);
                    var hoSoMoi = new HoSoTiemChung
                    {
                        MaHSTC = maHSTC_CanLienKet,
                        HoTen = model.HoTen,
                        CCCD = model.CCCD,
                        NgaySinh = model.NgaySinh,
                        GioiTinh = model.GioiTinh,
                        TrangThai = true
                    };
                    _uow.HoSoTiemChungs.Add(hoSoMoi);
                    _uow.SaveChanges(); // Save HoSoTiemChung
                    messageDetail = "Hệ thống đã tạo hồ sơ tiêm chủng mới cho bạn.";
                }

                // Tạo liên kết giữa KhachHang và HoSoTiemChung
                string maLK;
                do
                {
                    maLK = TPVAXWebsite.Common.KeyGenerator.GenMaLK(model.CCCD);
                } while (_uow.LienKetHoSos.Any(lk => lk.MaLK == maLK));

                var lienKet = new LienKetHoSo
                {
                    MaLK = maLK,
                    MaKH = maKH,
                    MaHSTC = maHSTC_CanLienKet,
                    VaiTro = "Bản thân", // Khi đăng ký, mặc định là "Bản thân"
                    NgayLienKet = DateTime.Now
                };
                _uow.LienKetHoSos.Add(lienKet);
                _uow.SaveChanges(); // Save LienKetHoSo

                _uow.Commit();

                TempData["SuccessMessage"] = $"Đăng ký thành công! {messageDetail}";

                return RedirectToAction("Login");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                _uow.Rollback();
                var errorMessages = new System.Text.StringBuilder();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessages.AppendLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                ModelState.AddModelError("", "Lỗi validation: " + errorMessages.ToString());
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbUpdateEx)
            {
                _uow.Rollback();
                var innerMessage = dbUpdateEx.InnerException?.InnerException?.Message ?? dbUpdateEx.InnerException?.Message ?? dbUpdateEx.Message;
                ModelState.AddModelError("", "Lỗi cập nhật database: " + innerMessage);
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                var innerMessage = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError("", "Lỗi đăng ký: " + innerMessage);
            }

            return View(model);
        }

        // GET: Account/Dashboard
        [HttpGet]
        public ActionResult Dashboard()
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null) 
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để truy cập trang này.";
                return RedirectToAction("Login");
            }

            // Xóa các thông báo cũ khi vào Dashboard
            TempData.Remove("SuccessMessage");
            TempData.Remove("ErrorMessage");

            var maKH = kh.MaKH;
            var lienKetList = _context.LienKetHoSos.Where(lk => lk.MaKH == maKH).ToList();
            var maHSTCs = lienKetList.Select(lk => lk.MaHSTC).ToList();
            var hoSos = _context.HoSoTiemChungs.Where(h => maHSTCs.Contains(h.MaHSTC)).ToList();
            var allLichTiems = _context.LichTiems.Where(l => maHSTCs.Contains(l.MaHSTC)).ToList();

            foreach (var lich in allLichTiems)
            {
                if (!string.IsNullOrEmpty(lich.MaVC))
                {
                    lich.Vaccine = _context.Vaccines.Find(lich.MaVC);
                }
                lich.HoSoTiemChung = _context.HoSoTiemChungs.Find(lich.MaHSTC);
            }

            var lichDaTiem = allLichTiems
                .Where(l => l.NgayTiemThucTe != null && l.TrangThai == "Đã tiêm")
                .OrderByDescending(l => l.NgayTiemThucTe)
                .ToList();

            var lichSapToi = allLichTiems
                .Where(l => l.TrangThai == "Chưa tiêm" && l.NgayHenTiem >= DateTime.Now)
                .OrderBy(l => l.NgayHenTiem)
                .ToList();

            var lichDaHuy = allLichTiems
                .Where(l => l.TrangThai == "Đã hủy")
                .OrderByDescending(l => l.NgayHenTiem)
                .ToList();

            ViewBag.VaiTroChinh = lienKetList.FirstOrDefault()?.VaiTro ?? "Bản thân";

            // Load hóa đơn
            var hoaDons = _context.HoaDons
                .Where(hd => hd.MaKH == maKH)
                .OrderByDescending(hd => hd.NgayLap)
                .Take(10)
                .ToList();

            // Load khuyến mãi đang hoạt động
            var khuyenMais = _context.KhuyenMais
                .Where(km => km.NgayBatDau <= DateTime.Now && km.NgayKetThuc >= DateTime.Now)
                .OrderByDescending(km => km.NgayBatDau)
                .Take(5)
                .ToList();

            var model = new DashboardViewModel
            {
                KhachHang = kh,
                HoSoTiemChungs = hoSos,
                SoMuiHoanThanh = lichDaTiem.Count,
                LichTiems = lichDaTiem,
                LichHenSapToi = lichSapToi,
                LichDaHuy = lichDaHuy,
                HoaDons = hoaDons,
                KhuyenMais = khuyenMais
            };

            return View(model);
        }

        // POST: Account/CapNhatThongTin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatThongTin(KhachHang model)
        {
            var khSession = Session["KH"] as KhachHang;
            if (khSession == null) 
            {
                return Json(new { success = false, message = "Phiên đăng nhập hết hạn." });
            }

            try
            {
                var kh = _context.KhachHangs.Find(khSession.MaKH);
                if (kh == null) 
                {
                    return Json(new { success = false, message = "Không tìm thấy thông tin khách hàng." });
                }

                if (!string.IsNullOrEmpty(model.Email) && model.Email != kh.Email)
                {
                    if (_context.KhachHangs.Any(k => k.Email == model.Email && k.MaKH != kh.MaKH))
                    {
                        return Json(new { success = false, message = "Email này đã được sử dụng." });
                    }
                }

                kh.HoTen = model.HoTen;
                kh.Email = model.Email;
                kh.NgaySinh = model.NgaySinh;
                kh.GioiTinh = model.GioiTinh;
                kh.DiaChi = model.DiaChi;

                _context.SaveChanges();
                Session["KH"] = kh;

                return Json(new { success = true, message = "Cập nhật thông tin thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // POST: Account/DoiMatKhau
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau(string MatKhauCu, string MatKhauMoi, string XacNhanMatKhauMoi)
        {
            var khSession = Session["KH"] as KhachHang;
            if (khSession == null) 
            {
                return Json(new { success = false, message = "Phiên đăng nhập hết hạn." });
            }

            try
            {
                var tk = _context.TaiKhoans.Find(khSession.MaTK);
                if (tk == null) 
                {
                    return Json(new { success = false, message = "Không tìm thấy tài khoản." });
                }

                if (!BCrypt.Net.BCrypt.Verify(MatKhauCu, tk.MatKhau))
                {
                    return Json(new { success = false, message = "Mật khẩu hiện tại không đúng." });
                }

                if (MatKhauMoi != XacNhanMatKhauMoi)
                {
                    return Json(new { success = false, message = "Xác nhận mật khẩu mới không khớp." });
                }

                if (MatKhauMoi.Length < 6)
                {
                    return Json(new { success = false, message = "Mật khẩu mới phải có ít nhất 6 ký tự." });
                }

                tk.MatKhau = BCrypt.Net.BCrypt.HashPassword(MatKhauMoi);
                _context.SaveChanges();

                return Json(new { success = true, message = "Đổi mật khẩu thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            TempData["SuccessMessage"] = "Đăng xuất thành công!";
            return RedirectToAction("Login", "Account");
        }

        // GET: Account/Profile
        [HttpGet]
        public new ActionResult Profile()
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null) 
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để truy cập trang này.";
                return RedirectToAction("Login");
            }

            var khFromDb = _context.KhachHangs.Find(kh.MaKH);
            Session["KH"] = khFromDb;

            // Xóa TempData["SuccessMessage"] nếu có để tránh hiển thị lại thông báo cũ
            TempData.Remove("SuccessMessage");

            return View(khFromDb);
        }

        // GET: Account/HoaDon
        [HttpGet]
        public ActionResult HoaDon()
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để truy cập trang này.";
                return RedirectToAction("Login");
            }

            var hoaDons = _context.HoaDons
                .Where(hd => hd.MaKH == kh.MaKH)
                .OrderByDescending(hd => hd.NgayLap)
                .ToList();

            foreach (var hd in hoaDons)
            {
                hd.ChiTietHoaDon = _context.ChiTietHoaDons
                    .Where(ct => ct.MaHD == hd.MaHD)
                    .ToList();
            }

            return View(hoaDons);
        }

        // GET: Account/ChiTietHoaDon
        [HttpGet]
        public ActionResult ChiTietHoaDon(string maHD)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để truy cập trang này.";
                return RedirectToAction("Login");
            }

            var hoaDon = _context.HoaDons
                .FirstOrDefault(hd => hd.MaHD == maHD && hd.MaKH == kh.MaKH);

            if (hoaDon == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy hóa đơn.";
                return RedirectToAction("Dashboard");
            }

            hoaDon.ChiTietHoaDon = _context.ChiTietHoaDons
                .Where(ct => ct.MaHD == maHD)
                .ToList();

            return View(hoaDon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow?.Dispose();
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
