//using System.Web.Mvc;
//using TPVAXWebsite.DAL;
//using TPVAXWebsite.Models.Domain;
//using TPVAXWebsite.Models.ViewModels;

//namespace TPVAXWebsite.Controllers
//{
//    public class AccountController : Controller
//    {
//        private UnitOfWork _uow = new UnitOfWork();
//        private readonly TPVAXDbContext _context = new TPVAXDbContext();

//        // GET: Account/Login
//        [HttpGet]
//        public ActionResult Login()
//        {
//            return View();
//        }

//        // POST: Account/Login
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Login(LoginViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            // Kiểm tra Khách hàng
//            var kh = _context.KhachHangs
//                .FirstOrDefault(k => k.Email == model.Identifier || k.SoDT == model.Identifier);

//            if (kh != null)
//            {
//                var tk = _context.TaiKhoans
//                    .FirstOrDefault(t => t.MaTK == kh.MaTK && t.MatKhau == model.MatKhau);

//                if (tk != null)
//                {
//                    Session["User"] = tk.MaTK;
//                    Session["KH"] = kh;
//                    return RedirectToAction("Index", "Home");
//                }
//            }

//            // Kiểm tra Nhân viên
//            var nv = _context.NhanViens
//                .FirstOrDefault(n => n.Email == model.Identifier || n.SoDT == model.Identifier);

//            if (nv != null)
//            {
//                var tk = _context.TaiKhoans
//                    .FirstOrDefault(t => t.MaTK == nv.MaTK && t.MatKhau == model.MatKhau);

//                if (tk != null)
//                {
//                    Session["User"] = tk.MaTK;
//                    Session["NV"] = nv;
//                    return RedirectToAction("Index", "Admin");
//                }
//            }

//            ModelState.AddModelError("", "Sai thông tin đăng nhập hoặc mật khẩu.");
//            return View(model);
//        }

//        // GET: Account/Register
//        [HttpGet]
//        public ActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // 1. Kiểm tra tài khoản đã tồn tại chưa (Check SĐT)
//                if (_uow.TaiKhoans.Any(t => t.MaTK == model.SoDT))
//                {
//                    ModelState.AddModelError("", "Số điện thoại này đã được đăng ký.");
//                    return View(model);
//                }

//                _uow.BeginTransaction();
//                try
//                {
//                    // --- BƯỚC 1: TẠO TÀI KHOẢN ---
//                    string maTK;
//                    do
//                    {
//                        // Gọi hàm từ thư viện Common của Web
//                        maTK = TPVAXWebsite.Common.KeyGenerator.GenMaTK();
//                    } while (_uow.TaiKhoans.Any(k => k.MaTK == maTK));

//                    var taiKhoan = new TaiKhoan
//                    {
//                        MaTK = maTK,
//                        MatKhau = model.MatKhau // Nên mã hóa MD5 ở đây
//                    };
//                    _uow.TaiKhoans.Add(taiKhoan);

//                    // --- BƯỚC 2: TẠO KHÁCH HÀNG ---
//                    string maKH;
//                    do
//                    {
//                        // Gọi hàm từ thư viện Common của Web
//                        maKH = TPVAXWebsite.Common.KeyGenerator.GenMaKH(model.CCCD);
//                    } while (_uow.KhachHangs.Any(k => k.MaKH == maKH));

//                    var khachHang = new KhachHang
//                    {
//                        MaKH = maKH,
//                        HoTen = model.HoTen,
//                        CCCD = model.CCCD,
//                        NgaySinh = model.NgaySinh,
//                        SoDT = model.SoDT,
//                        Email = model.Email,
//                        MaTK = taiKhoan.MaTK
//                    };
//                    _uow.KhachHangs.Add(khachHang);

//                    // --- BƯỚC 3: XỬ LÝ LIÊN KẾT HỒ SƠ (LOGIC THÔNG MINH) ---
//                    var hoSoCu = _uow.HoSoTiemChungs
//                        .FirstOrDefault(h => h.CCCD == model.CCCD);

//                    string maHSTC_CanLienKet;
//                    if (hoSoCu != null)
//                    {
//                        // CASE A: ĐÃ CÓ HỒ SƠ -> LIÊN KẾT LẠI
//                        maHSTC_CanLienKet = hoSoCu.MaHSTC;
//                    }
//                    else
//                    {
//                        // CASE B: CHƯA CÓ HỒ SƠ -> TẠO MỚI
//                        maHSTC_CanLienKet = "HS" + DateTime.Now.ToString("yyMMddHHmm");
//                        var hoSoMoi = new HoSoTiemChung
//                        {
//                            MaHSTC = maHSTC_CanLienKet,
//                            HoTen = model.HoTen,
//                            CCCD = model.CCCD,
//                            NgaySinh = model.NgaySinh,
//                            GioiTinh = "Chưa rõ",
//                            TrangThai = true
//                        };
//                        _uow.HoSoTiemChungs.Add(hoSoMoi);
//                    }

//                    // --- BƯỚC 4: TẠO LIÊN KẾT (BẢNG LienKetHoSo) ---
//                    var lienKet = new LienKetHoSo
//                    {
//                        MaLK = "LK" + DateTime.Now.Ticks.ToString().Substring(12),
//                        MaKH = maKH,
//                        MaHSTC = maHSTC_CanLienKet,
//                        VaiTro = "Bản thân",
//                        NgayLienKet = DateTime.Now
//                    };
//                    _uow.LienKetHoSos.Add(lienKet);

//                    // Lưu tất cả
//                    _uow.Commit();

//                    TempData["SuccessMessage"] = "Đăng ký thành công! " +
//                        (hoSoCu != null ? "Hệ thống đã tìm thấy và liên kết hồ sơ tiêm chủng cũ của bạn." : "");

//                    return RedirectToAction("Login");
//                }
//                catch (Exception ex)
//                {
//                    _uow.Rollback();
//                    ModelState.AddModelError("", "Lỗi đăng ký: " + ex.Message);
//                }
//            }
//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult CapNhatThongTin(KhachHang model)
//        {
//            var khSession = Session["KH"] as KhachHang;
//            if (khSession == null) return RedirectToAction("Login");

//            var kh = _context.KhachHangs.Find(khSession.MaKH);
//            if (kh == null) return HttpNotFound();

//            kh.HoTen = model.HoTen;
//            kh.Email = model.Email;
//            kh.SoDT = model.SoDT;
//            kh.NgaySinh = model.NgaySinh;
//            kh.DiaChi = model.DiaChi;

//            _context.SaveChanges();
//            Session["KH"] = kh;

//            TempData["SuccessMessage"] = "Thông tin đã được cập nhật.";
//            return RedirectToAction("Dashboard");
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult DoiMatKhau(string MatKhauCu, string MatKhauMoi, string XacNhanMatKhauMoi)
//        {
//            var khSession = Session["KH"] as KhachHang;
//            if (khSession == null) return RedirectToAction("Login");

//            var tk = _context.TaiKhoans.Find(khSession.MaTK);
//            if (tk == null) return HttpNotFound();

//            if (tk.MatKhau != MatKhauCu)
//            {
//                TempData["ErrorMessage"] = "Mật khẩu hiện tại không đúng.";
//                return RedirectToAction("Dashboard");
//            }

//            if (MatKhauMoi != XacNhanMatKhauMoi)
//            {
//                TempData["ErrorMessage"] = "Xác nhận mật khẩu mới không khớp.";
//                return RedirectToAction("Dashboard");
//            }

//            tk.MatKhau = MatKhauMoi;
//            _context.SaveChanges();

//            TempData["SuccessMessage"] = "Mật khẩu đã được thay đổi.";
//            return RedirectToAction("Dashboard");
//        }

//        // GET: Account/Logout
//        public ActionResult Logout()
//        {
//            Session.Clear();
//            return RedirectToAction("Login", "Account");
//        }

//        [HttpGet]
//        public ActionResult Dashboard()
//        {
//            var kh = Session["KH"] as KhachHang;
//            if (kh == null) return RedirectToAction("Login");

//            var maKH = kh.MaKH;

//            // Lấy danh sách hồ sơ liên kết với khách hàng
//            var maHSTCs = _context.LienKetHoSos
//                .Where(lk => lk.MaKH == maKH)
//                .Select(lk => lk.MaHSTC)
//                .ToList();

//            var hoSos = _context.HoSoTiemChungs
//                .Where(h => maHSTCs.Contains(h.MaHSTC))
//                .ToList();

//            // Lấy toàn bộ lịch tiêm liên quan
//            var allLichTiems = _context.LichTiems
//                .Where(l => maHSTCs.Contains(l.MaHSTC))
//                .Include(l => l.Vaccine)
//                .Include(l => l.HoSoTiemChung)
//                .ToList();

//            // Phân loại lịch tiêm
//            var lichDaTiem = allLichTiems
//                .Where(l => l.NgayTiemThucTe != null && l.TrangThai == "Đã tiêm")
//                .OrderByDescending(l => l.NgayTiemThucTe)
//                .ToList();

//            var lichSapToi = allLichTiems
//                .Where(l => l.NgayTiemThucTe == null && l.TrangThai == "Chưa tiêm" && l.NgayHenTiem >= DateTime.Now)
//                .OrderBy(l => l.NgayHenTiem)
//                .ToList();

//            var lichDaHuy = allLichTiems
//                .Where(l => l.TrangThai == "Đã hủy")
//                .OrderByDescending(l => l.NgayHenTiem)
//                .ToList();

//            var soMuiHoanThanh = lichDaTiem.Count;

//            var vaiTroChinh = _context.LienKetHoSos
//                .Where(lk => lk.MaKH == maKH && maHSTCs.Contains(lk.MaHSTC))
//                .Select(lk => lk.VaiTro)
//                .FirstOrDefault();

//            ViewBag.VaiTroChinh = vaiTroChinh;

//            var model = new DashboardViewModel
//            {
//                KhachHang = kh,
//                HoSoTiemChungs = hoSos,
//                SoMuiHoanThanh = soMuiHoanThanh,
//                LichTiems = lichDaTiem,
//                LichHenSapToi = lichSapToi,
//                LichDaHuy = lichDaHuy
//            };

//            return View(model);
//        }
//    }
//}
