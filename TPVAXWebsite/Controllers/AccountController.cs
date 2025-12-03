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
        private readonly TPVAXDbContext _context = new TPVAXDbContext();

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                // Phân quyền: Khách hàng -> Dashboard, Nhân viên -> Admin
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
                        Session["ChucVu"] = nv.ChucVu; // Lưu chức vụ để phân quyền chi tiết
                        
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

        // GET: Account/RegisterExisting - Trang đăng ký cho khách hàng đã có thông tin trong hệ thống
        [HttpGet]
        public ActionResult RegisterExisting()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        // POST: Account/RegisterExisting - Tìm kiếm khách hàng theo CCCD và SĐT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterExisting(RegisterExistingCustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // Tìm khách hàng theo CCCD và SĐT
                var khachHang = _context.KhachHangs
                    .FirstOrDefault(k => k.CCCD == model.CCCD && k.SoDT == model.SoDT);

                if (khachHang == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thông tin khách hàng với CCCD và Số điện thoại này. Vui lòng kiểm tra lại hoặc đăng ký mới.";
                    return View(model);
                }

                // Kiểm tra xem khách hàng đã có tài khoản chưa
                if (!string.IsNullOrEmpty(khachHang.MaTK))
                {
                    TempData["ErrorMessage"] = "Khách hàng này đã có tài khoản. Vui lòng đăng nhập bằng số điện thoại hoặc email.";
                    return View(model);
                }

                // Chuyển sang trang xác nhận và tạo mật khẩu
                var confirmModel = new ConfirmAndCreateAccountViewModel
                {
                    MaKH = khachHang.MaKH,
                    HoTen = khachHang.HoTen,
                    CCCD = khachHang.CCCD,
                    SoDT = khachHang.SoDT,
                    NgaySinh = khachHang.NgaySinh,
                    GioiTinh = khachHang.GioiTinh,
                    Email = khachHang.Email,
                    DiaChi = khachHang.DiaChi
                };

                return View("ConfirmCreateAccount", confirmModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi hệ thống: " + ex.Message;
                return View(model);
            }
        }

        // POST: Account/CreateAccountForExisting - Tạo tài khoản cho khách hàng đã tồn tại
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountForExisting(ConfirmAndCreateAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ConfirmCreateAccount", model);
            }

            try
            {
                // Tìm lại khách hàng để đảm bảo dữ liệu chính xác
                var khachHang = _context.KhachHangs.Find(model.MaKH);
                
                if (khachHang == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thông tin khách hàng. Vui lòng thử lại.";
                    return RedirectToAction("RegisterExisting");
                }

                // Kiểm tra lại xem đã có tài khoản chưa (tránh race condition)
                if (!string.IsNullOrEmpty(khachHang.MaTK))
                {
                    TempData["ErrorMessage"] = "Khách hàng này đã có tài khoản. Vui lòng đăng nhập.";
                    return RedirectToAction("Login");
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Tạo mã tài khoản mới
                        string maTK;
                        do
                        {
                            maTK = TPVAXWebsite.Common.KeyGenerator.GenMaTK();
                        } while (_context.TaiKhoans.Any(t => t.MaTK == maTK));

                        // Tạo tài khoản mới
                        var taiKhoan = new TaiKhoan
                        {
                            MaTK = maTK,
                            MatKhau = BCrypt.Net.BCrypt.HashPassword(model.MatKhau)
                        };
                        _context.TaiKhoans.Add(taiKhoan);
                        _context.SaveChanges();

                        // Cập nhật MaTK vào bảng KhachHang
                        khachHang.MaTK = maTK;
                        _context.SaveChanges();

                        transaction.Commit();

                        TempData["SuccessMessage"] = $"Tạo tài khoản thành công! Chào mừng {khachHang.HoTen}. Bạn có thể đăng nhập bằng số điện thoại hoặc email.";
                        return RedirectToAction("Login");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi tạo tài khoản: " + ex.Message;
                return View("ConfirmCreateAccount", model);
            }
        }

        // GET: Account/DangKyTuHoSo - Trang đăng ký từ hồ sơ tiêm chủng có sẵn (chỉ cần CCCD)
        [HttpGet]
        public ActionResult DangKyTuHoSo()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View(new TimHoSoTiemChungViewModel());
        }

        // POST: Account/DangKyTuHoSo - Tìm hồ sơ tiêm chủng theo CCCD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKyTuHoSo(TimHoSoTiemChungViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // Kiểm tra xem CCCD đã có tài khoản khách hàng chưa
                var khachHangDaCo = _context.KhachHangs.FirstOrDefault(k => k.CCCD == model.CCCD);
                if (khachHangDaCo != null)
                {
                    if (!string.IsNullOrEmpty(khachHangDaCo.MaTK))
                    {
                        TempData["ErrorMessage"] = "CCCD này đã có tài khoản. Vui lòng đăng nhập.";
                        return View(model);
                    }
                    else
                    {
                        // Khách hàng đã có nhưng chưa có tài khoản -> chuyển sang ConfirmCreateAccount
                        var confirmModel = new ConfirmAndCreateAccountViewModel
                        {
                            MaKH = khachHangDaCo.MaKH,
                            HoTen = khachHangDaCo.HoTen,
                            CCCD = khachHangDaCo.CCCD,
                            SoDT = khachHangDaCo.SoDT,
                            NgaySinh = khachHangDaCo.NgaySinh,
                            GioiTinh = khachHangDaCo.GioiTinh,
                            Email = khachHangDaCo.Email,
                            DiaChi = khachHangDaCo.DiaChi
                        };
                        return View("ConfirmCreateAccount", confirmModel);
                    }
                }

                // Tìm hồ sơ tiêm chủng theo CCCD
                var hoSo = _context.HoSoTiemChungs.FirstOrDefault(h => h.CCCD == model.CCCD && h.TrangThai == true);
                
                if (hoSo == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy hồ sơ tiêm chủng với CCCD này. Vui lòng kiểm tra lại hoặc đăng ký mới.";
                    return View(model);
                }

                // Chuyển sang trang xác nhận hồ sơ tiêm chủng
                var xacNhanModel = new XacNhanHoSoTiemChungViewModel
                {
                    MaHSTC = hoSo.MaHSTC,
                    HoTen = hoSo.HoTen,
                    CCCD = hoSo.CCCD,
                    NgaySinh = hoSo.NgaySinh,
                    GioiTinh = hoSo.GioiTinh
                };

                return View("XacNhanHoSoTiemChung", xacNhanModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi hệ thống: " + ex.Message;
                return View(model);
            }
        }

        // GET: Account/XacNhanHoSoTiemChung
        [HttpGet]
        public ActionResult XacNhanHoSoTiemChung()
        {
            return RedirectToAction("DangKyTuHoSo");
        }

        // POST: Account/TaoTaiKhoanTuHoSo - Tạo tài khoản từ hồ sơ tiêm chủng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaoTaiKhoanTuHoSo(XacNhanHoSoTiemChungViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("XacNhanHoSoTiemChung", model);
            }

            try
            {
                // Kiểm tra hồ sơ tiêm chủng
                var hoSo = _context.HoSoTiemChungs.Find(model.MaHSTC);
                if (hoSo == null)
                {
                    TempData["ErrorMessage"] = "Hồ sơ tiêm chủng không tồn tại.";
                    return RedirectToAction("DangKyTuHoSo");
                }

                // Kiểm tra CCCD đã có khách hàng chưa
                if (_context.KhachHangs.Any(k => k.CCCD == hoSo.CCCD))
                {
                    TempData["ErrorMessage"] = "CCCD này đã được đăng ký tài khoản.";
                    return RedirectToAction("DangKyTuHoSo");
                }

                // Kiểm tra SĐT trùng
                if (_context.KhachHangs.Any(k => k.SoDT == model.SoDT))
                {
                    ModelState.AddModelError("SoDT", "Số điện thoại này đã được đăng ký.");
                    return View("XacNhanHoSoTiemChung", model);
                }

                // Kiểm tra Email trùng (nếu có nhập)
                if (!string.IsNullOrEmpty(model.Email) && _context.KhachHangs.Any(k => k.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email này đã được đăng ký.");
                    return View("XacNhanHoSoTiemChung", model);
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Tạo tài khoản
                        string maTK;
                        do
                        {
                            maTK = TPVAXWebsite.Common.KeyGenerator.GenMaTK();
                        } while (_context.TaiKhoans.Any(t => t.MaTK == maTK));

                        var taiKhoan = new TaiKhoan
                        {
                            MaTK = maTK,
                            MatKhau = BCrypt.Net.BCrypt.HashPassword(model.MatKhau)
                        };
                        _context.TaiKhoans.Add(taiKhoan);
                        _context.SaveChanges();

                        // Tạo khách hàng từ thông tin hồ sơ tiêm chủng + SĐT/Email từ form
                        string maKH;
                        do
                        {
                            maKH = TPVAXWebsite.Common.KeyGenerator.GenMaKH(hoSo.CCCD);
                        } while (_context.KhachHangs.Any(k => k.MaKH == maKH));

                        var khachHang = new KhachHang
                        {
                            MaKH = maKH,
                            HoTen = hoSo.HoTen,
                            CCCD = hoSo.CCCD,
                            NgaySinh = hoSo.NgaySinh,
                            GioiTinh = hoSo.GioiTinh,
                            SoDT = model.SoDT,
                            Email = model.Email,
                            MaTK = maTK
                        };
                        _context.KhachHangs.Add(khachHang);
                        _context.SaveChanges();

                        // Tạo liên kết hồ sơ
                        string maLK;
                        do
                        {
                            maLK = TPVAXWebsite.Common.KeyGenerator.GenMaLK(hoSo.CCCD);
                        } while (_context.LienKetHoSos.Any(lk => lk.MaLK == maLK));

                        var lienKet = new LienKetHoSo
                        {
                            MaLK = maLK,
                            MaKH = maKH,
                            MaHSTC = hoSo.MaHSTC,
                            VaiTro = "Bản thân",
                            NgayLienKet = DateTime.Now
                        };
                        _context.LienKetHoSos.Add(lienKet);
                        _context.SaveChanges();

                        transaction.Commit();

                        TempData["SuccessMessage"] = $"Đăng ký thành công! Hồ sơ tiêm chủng đã được liên kết với tài khoản của bạn.";
                        return RedirectToAction("Login");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi tạo tài khoản: " + ex.Message;
                return View("XacNhanHoSoTiemChung", model);
            }
        }

        // GET: Account/XacNhanLienKetHoSo - Hiển thị trang xác nhận liên kết hồ sơ có sẵn
        [HttpGet]
        public ActionResult XacNhanLienKetHoSo()
        {
            // Kiểm tra có thông tin pending không
            if (TempData["PendingRegister"] == null || TempData["HoSoCuMaHSTC"] == null)
            {
                return RedirectToAction("Register");
            }

            // Giữ lại TempData để dùng trong POST
            TempData.Keep("PendingRegister");
            TempData.Keep("HoSoCuMaHSTC");
            
            ViewBag.HoTen = TempData["HoSoCuHoTen"];
            ViewBag.NgaySinh = TempData["HoSoCuNgaySinh"];
            TempData.Keep("HoSoCuHoTen");
            TempData.Keep("HoSoCuNgaySinh");
            
            return View();
        }

        // POST: Account/HoanTatDangKyVoiLienKet - Hoàn tất đăng ký và liên kết hồ sơ có sẵn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HoanTatDangKyVoiLienKet(string SoDT, string Email)
        {
            try
            {
                // Lấy thông tin từ TempData
                var pendingJson = TempData["PendingRegister"] as string;
                var maHSTC = TempData["HoSoCuMaHSTC"] as string;

                if (string.IsNullOrEmpty(pendingJson) || string.IsNullOrEmpty(maHSTC))
                {
                    TempData["ErrorMessage"] = "Phiên đăng ký đã hết hạn. Vui lòng thử lại.";
                    return RedirectToAction("Register");
                }

                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterViewModel>(pendingJson);

                // Validate SĐT từ modal
                if (string.IsNullOrEmpty(SoDT) || !System.Text.RegularExpressions.Regex.IsMatch(SoDT, @"^0\d{9}$"))
                {
                    TempData["ErrorMessage"] = "Số điện thoại không hợp lệ. Vui lòng thử lại.";
                    TempData["PendingRegister"] = pendingJson;
                    TempData["HoSoCuMaHSTC"] = maHSTC;
                    TempData["HoSoCuHoTen"] = model.HoTen;
                    TempData["HoSoCuNgaySinh"] = model.NgaySinh.ToString("dd/MM/yyyy");
                    return RedirectToAction("XacNhanLienKetHoSo");
                }

                // Kiểm tra lại các điều kiện
                if (_context.KhachHangs.Any(k => k.CCCD == model.CCCD))
                {
                    TempData["ErrorMessage"] = "CCCD này đã được đăng ký tài khoản.";
                    return RedirectToAction("Register");
                }

                // Kiểm tra SĐT từ modal (thay vì từ model)
                if (_context.KhachHangs.Any(k => k.SoDT == SoDT))
                {
                    TempData["ErrorMessage"] = "Số điện thoại này đã được đăng ký.";
                    TempData["PendingRegister"] = pendingJson;
                    TempData["HoSoCuMaHSTC"] = maHSTC;
                    TempData["HoSoCuHoTen"] = model.HoTen;
                    TempData["HoSoCuNgaySinh"] = model.NgaySinh.ToString("dd/MM/yyyy");
                    return RedirectToAction("XacNhanLienKetHoSo");
                }

                // Kiểm tra Email từ modal (nếu có nhập)
                if (!string.IsNullOrEmpty(Email) && _context.KhachHangs.Any(k => k.Email == Email))
                {
                    TempData["ErrorMessage"] = "Email này đã được đăng ký.";
                    TempData["PendingRegister"] = pendingJson;
                    TempData["HoSoCuMaHSTC"] = maHSTC;
                    TempData["HoSoCuHoTen"] = model.HoTen;
                    TempData["HoSoCuNgaySinh"] = model.NgaySinh.ToString("dd/MM/yyyy");
                    return RedirectToAction("XacNhanLienKetHoSo");
                }

                var hoSoCu = _context.HoSoTiemChungs.Find(maHSTC);
                if (hoSoCu == null)
                {
                    TempData["ErrorMessage"] = "Hồ sơ tiêm chủng không còn tồn tại.";
                    return RedirectToAction("Register");
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Tạo tài khoản
                        string maTK;
                        do
                        {
                            maTK = TPVAXWebsite.Common.KeyGenerator.GenMaTK();
                        } while (_context.TaiKhoans.Any(k => k.MaTK == maTK));

                        var taiKhoan = new TaiKhoan
                        {
                            MaTK = maTK,
                            MatKhau = BCrypt.Net.BCrypt.HashPassword(model.MatKhau)
                        };
                        _context.TaiKhoans.Add(taiKhoan);
                        _context.SaveChanges();

                        // Tạo khách hàng - lấy thông tin từ hồ sơ tiêm chủng + SĐT/Email từ modal
                        string maKH;
                        do
                        {
                            maKH = TPVAXWebsite.Common.KeyGenerator.GenMaKH(hoSoCu.CCCD);
                        } while (_context.KhachHangs.Any(k => k.MaKH == maKH));

                        var khachHang = new KhachHang
                        {
                            MaKH = maKH,
                            HoTen = hoSoCu.HoTen,           // Lấy từ hồ sơ tiêm chủng
                            CCCD = hoSoCu.CCCD,             // Lấy từ hồ sơ tiêm chủng
                            NgaySinh = hoSoCu.NgaySinh,     // Lấy từ hồ sơ tiêm chủng
                            GioiTinh = hoSoCu.GioiTinh,     // Lấy từ hồ sơ tiêm chủng
                            SoDT = SoDT,                    // Lấy từ modal
                            Email = Email,                  // Lấy từ modal
                            DiaChi = model.DiaChi,          // Lấy từ form đăng ký ban đầu
                            MaTK = taiKhoan.MaTK
                        };
                        _context.KhachHangs.Add(khachHang);
                        _context.SaveChanges();

                        // Liên kết với hồ sơ có sẵn
                        string maLK;
                        do
                        {
                            maLK = TPVAXWebsite.Common.KeyGenerator.GenMaLK(hoSoCu.CCCD);
                        } while (_context.LienKetHoSos.Any(lk => lk.MaLK == maLK));

                        var lienKet = new LienKetHoSo
                        {
                            MaLK = maLK,
                            MaKH = maKH,
                            MaHSTC = maHSTC,
                            VaiTro = "Bản thân",
                            NgayLienKet = DateTime.Now
                        };
                        _context.LienKetHoSos.Add(lienKet);
                        _context.SaveChanges();

                        transaction.Commit();

                        TempData["SuccessMessage"] = $"Đăng ký thành công! Hồ sơ tiêm chủng của bạn (do người thân tạo trước đó) đã được liên kết với tài khoản.";
                        return RedirectToAction("Login");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi đăng ký: " + ex.Message;
                return RedirectToAction("Register");
            }
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
                var hoSoCu = _context.HoSoTiemChungs.FirstOrDefault(h => h.CCCD == model.CCCD && h.TrangThai == true);
                
                // Nếu hồ sơ đã tồn tại (do người thân tạo trước), yêu cầu xác nhận liên kết
                if (hoSoCu != null)
                {
                    // Lưu thông tin đăng ký vào TempData để xử lý sau khi xác nhận
                    TempData["PendingRegister"] = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    TempData["HoSoCuMaHSTC"] = hoSoCu.MaHSTC;
                    TempData["HoSoCuHoTen"] = hoSoCu.HoTen;
                    TempData["HoSoCuNgaySinh"] = hoSoCu.NgaySinh.ToString("dd/MM/yyyy");
                    
                    return RedirectToAction("XacNhanLienKetHoSo");
                }
                
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        string maTK;
                        do
                        {
                            maTK = TPVAXWebsite.Common.KeyGenerator.GenMaTK();
                        } while (_context.TaiKhoans.Any(k => k.MaTK == maTK));

                        var taiKhoan = new TaiKhoan
                        {
                            MaTK = maTK,
                            MatKhau = BCrypt.Net.BCrypt.HashPassword(model.MatKhau)
                        };
                        _context.TaiKhoans.Add(taiKhoan);
                        _context.SaveChanges();

                        string maKH;
                        do
                        {
                            maKH = TPVAXWebsite.Common.KeyGenerator.GenMaKH(model.CCCD);
                        } while (_context.KhachHangs.Any(k => k.MaKH == maKH));

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
                        _context.KhachHangs.Add(khachHang);
                        _context.SaveChanges();

                        // Tạo hồ sơ tiêm chủng mới (vì nếu có hồ sơ cũ đã redirect ở trên)
                        string maHSTC = TPVAXWebsite.Common.KeyGenerator.GenMaHSTC(model.CCCD);
                        var hoSoMoi = new HoSoTiemChung
                        {
                            MaHSTC = maHSTC,
                            HoTen = model.HoTen,
                            CCCD = model.CCCD,
                            NgaySinh = model.NgaySinh,
                            GioiTinh = model.GioiTinh,
                            TrangThai = true
                        };
                        _context.HoSoTiemChungs.Add(hoSoMoi);
                        _context.SaveChanges();

                        // Tạo liên kết
                        string maLK;
                        do
                        {
                            maLK = TPVAXWebsite.Common.KeyGenerator.GenMaLK(model.CCCD);
                        } while (_context.LienKetHoSos.Any(lk => lk.MaLK == maLK));

                        var lienKet = new LienKetHoSo
                        {
                            MaLK = maLK,
                            MaKH = maKH,
                            MaHSTC = maHSTC,
                            VaiTro = "Bản thân",
                            NgayLienKet = DateTime.Now
                        };
                        _context.LienKetHoSos.Add(lienKet);
                        _context.SaveChanges();

                        transaction.Commit();

                        TempData["SuccessMessage"] = "Đăng ký thành công! Hệ thống đã tạo hồ sơ tiêm chủng mới cho bạn.";
                        return RedirectToAction("Login");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
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
                var innerMessage = dbUpdateEx.InnerException?.InnerException?.Message ?? dbUpdateEx.InnerException?.Message ?? dbUpdateEx.Message;
                ModelState.AddModelError("", "Lỗi cập nhật database: " + innerMessage);
            }
            catch (Exception ex)
            {
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

            var chiTietList = _context.ChiTietHoaDons
                .Where(ct => ct.MaHD == maHD)
                .ToList();

            // Lấy thông tin khuyến mãi nếu có
            string tenKM = null;
            if (!string.IsNullOrEmpty(hoaDon.MaKM))
            {
                var km = _context.KhuyenMais.Find(hoaDon.MaKM);
                tenKM = km?.TenKM;
            }

            // Tạo ViewModel
            var viewModel = new Models.ViewModels.HoaDonViewModel
            {
                MaHD = hoaDon.MaHD,
                NgayLap = hoaDon.NgayLap,
                TongTien = hoaDon.TongTien,
                TrangThai = hoaDon.TrangThai == true ? "Đã thanh toán" : "Chưa thanh toán",
                MaKH = hoaDon.MaKH,
                TenKH = kh.HoTen,
                MaKM = hoaDon.MaKM,
                TenKM = tenKM,
                ChiTietHoaDon = chiTietList.Select(ct => {
                    // Lấy thông tin sản phẩm
                    string tenSP = "";
                    string hinhAnh = "";
                    
                    if (ct.LoaiSanPham == "VACCINE")
                    {
                        var vaccine = _context.Vaccines.Find(ct.MaSanPham);
                        tenSP = vaccine?.TenVC ?? "Không rõ";
                        hinhAnh = vaccine?.HinhAnh;
                    }
                    else
                    {
                        var goi = _context.GoiVaccines.Find(ct.MaSanPham);
                        tenSP = goi?.TenGoi ?? "Không rõ";
                        hinhAnh = goi?.HinhAnh;
                    }

                    return new Models.ViewModels.ChiTietHoaDonViewModel
                    {
                        MaCTHD = ct.MaCTHD,
                        MaSanPham = ct.MaSanPham,
                        TenSanPham = tenSP,
                        LoaiSanPham = ct.LoaiSanPham,
                        SoLuong = ct.SoLuong,
                        DonGia = ct.DonGia,
                        ThanhTien = ct.DonGia * ct.SoLuong,
                        MaHD = ct.MaHD,
                        HinhAnh = hinhAnh
                    };
                }).ToList()
            };

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
