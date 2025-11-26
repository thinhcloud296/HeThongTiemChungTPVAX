using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý hóa đơn và thanh toán
    /// </summary>
    public class HoaDonController : Controller
    {
        private readonly TPVAXDbContext _context = new TPVAXDbContext();

        // GET: HoaDon/Index - Danh sách hóa đơn của khách hàng
        public ActionResult Index()
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để xem hóa đơn.";
                return RedirectToAction("Login", "Account");
            }

            var hoaDons = _context.HoaDons
                .Include(hd => hd.ChiTietHoaDon)
                .Include(hd => hd.KhachHang)
                .Include(hd => hd.NhanVien)
                .Include(hd => hd.KhuyenMai)
                .Where(hd => hd.MaKH == kh.MaKH)
                .OrderByDescending(hd => hd.NgayLap)
                .ToList();

            var hoaDonVMs = hoaDons.Select(hd => new HoaDonViewModel
            {
                MaHD = hd.MaHD,
                NgayLap = hd.NgayLap,
                TongTien = hd.TongTien,
                TrangThai = hd.TrangThai.HasValue && hd.TrangThai.Value ? "Đã thanh toán" : "Chưa thanh toán",
                MaKH = hd.MaKH,
                MaNV = hd.MaNV,
                MaKM = hd.MaKM,
                TenKH = hd.KhachHang?.HoTen,
                TenNV = hd.NhanVien?.HoTen,
                TenKM = hd.KhuyenMai?.TenKM,
                ChiTietHoaDon = hd.ChiTietHoaDon != null 
                    ? hd.ChiTietHoaDon.Select(ct => new ChiTietHoaDonViewModel
                    {
                        MaCTHD = ct.MaCTHD,
                        SoLuong = ct.SoLuong,
                        DonGia = ct.DonGia,
                        MaSanPham = ct.MaSanPham,
                        LoaiSanPham = ct.LoaiSanPham,
                        MaHD = ct.MaHD
                    }).ToList()
                    : new List<ChiTietHoaDonViewModel>()
            }).ToList();

            return View(hoaDonVMs);
        }

        // GET: HoaDon/Checkout - Trang thanh toán
        public ActionResult Checkout()
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để thanh toán.";
                return RedirectToAction("Login", "Account");
            }

            // Load giỏ hàng
            var gioHangItems = LoadGioHangItems(kh.MaKH);

            if (gioHangItems == null || !gioHangItems.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng trống. Vui lòng thêm sản phẩm trước khi thanh toán.";
                return RedirectToAction("Index", "GioHang");
            }

            // Load khuyến mãi có thể áp dụng
            var khuyenMais = _context.KhuyenMais
                .Where(km => km.NgayBatDau <= DateTime.Now
                          && km.NgayKetThuc >= DateTime.Now
                          && km.TrangThai == true)
                .ToList();

            decimal tongTien = gioHangItems.Sum(item => item.ThanhTien);

            var model = new CheckoutViewModel
            {
                KhachHang = kh,
                GioHang = gioHangItems,
                TongTienTruocGiam = tongTien,
                TienGiam = 0,
                TongTienSauGiam = tongTien,
                KhuyenMais = khuyenMais,
                DiaChiGiaoHang = kh.DiaChi
            };

            return View(model);
        }

        // POST: HoaDon/ApDungKhuyenMai
        [HttpPost]
        public JsonResult ApDungKhuyenMai(string MaKM)
        {
            try
            {
                var kh = Session["KH"] as KhachHang;
                if (kh == null)
                {
                    return Json(new { success = false, message = "Phiên đăng nhập hết hạn." });
                }

                // Kiểm tra mã khuyến mãi
                var khuyenMai = _context.KhuyenMais
                    .FirstOrDefault(km => km.MaKM == MaKM
                                       && km.NgayBatDau <= DateTime.Now
                                       && km.NgayKetThuc >= DateTime.Now
                                       && km.TrangThai == true);

                if (khuyenMai == null)
                {
                    return Json(new { success = false, message = "Mã khuyến mãi không hợp lệ hoặc đã hết hạn." });
                }

                // Load giỏ hàng
                var gioHangItems = LoadGioHangItems(kh.MaKH);
                decimal tongTien = gioHangItems.Sum(item => item.ThanhTien);

                // Tính tiền giảm
                decimal tienGiam = 0;

                // Lấy danh sách sản phẩm áp dụng khuyến mãi
                var sanPhamApDung = _context.ChiTietKhuyenMais
                    .Where(ct => ct.MaKM == MaKM)
                    .Select(ct => new { ct.MaSanPham, ct.LoaiSanPham })
                    .ToList();

                if (sanPhamApDung.Any())
                {
                    // Khuyến mãi áp dụng cho sản phẩm cụ thể
                    var tongTienApDung = gioHangItems
                        .Where(item => sanPhamApDung.Any(sp => sp.MaSanPham == item.MaSanPham
                                                             && sp.LoaiSanPham == item.LoaiSanPham))
                        .Sum(item => item.ThanhTien);

                    if (tongTienApDung > 0)
                    {
                        if (khuyenMai.KieuGiam == "PhanTram")
                        {
                            tienGiam = tongTienApDung * khuyenMai.GiaTriGiam / 100;
                        }
                        else // SoTien
                        {
                            tienGiam = khuyenMai.GiaTriGiam;
                        }
                    }
                }
                else
                {
                    // Khuyến mãi áp dụng toàn bộ đơn hàng
                    if (khuyenMai.KieuGiam == "PhanTram")
                    {
                        tienGiam = tongTien * khuyenMai.GiaTriGiam / 100;
                    }
                    else // SoTien
                    {
                        tienGiam = khuyenMai.GiaTriGiam;
                    }
                }

                // Không giảm quá tổng tiền
                if (tienGiam > tongTien)
                {
                    tienGiam = tongTien;
                }

                decimal tongTienSauGiam = tongTien - tienGiam;

                return Json(new
                {
                    success = true,
                    message = "Áp dụng khuyến mãi thành công!",
                    maKM = MaKM,
                    tenKM = khuyenMai.TenKM,
                    tienGiam = tienGiam,
                    tongTienSauGiam = tongTienSauGiam
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // POST: HoaDon/XacNhanThanhToan
        [HttpPost]
        public JsonResult XacNhanThanhToan(string MaKM, string NgayHenTiem, string GioHenTiem)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var kh = Session["KH"] as KhachHang;
                    if (kh == null)
                    {
                        return Json(new { success = false, message = "Phiên đăng nhập hết hạn." });
                    }

                    // Load giỏ hàng
                    var gioHangItems = _context.GioHangs
                        .Where(g => g.MaKH == kh.MaKH)
                        .ToList();

                    if (!gioHangItems.Any())
                    {
                        return Json(new { success = false, message = "Giỏ hàng trống." });
                    }

                    // Tính tổng tiền
                    decimal tongTien = 0;
                    var chiTietList = new List<ChiTietHoaDon>();

                    foreach (var item in gioHangItems)
                    {
                        decimal donGia = 0;
                        string tenSanPham = "";

                        if (item.LoaiSanPham == "VACCINE")
                        {
                            var vaccine = _context.Vaccines.Find(item.MaSanPham);
                            if (vaccine == null)
                            {
                                throw new Exception($"Vaccine {item.MaSanPham} không tồn tại.");
                            }
                            if (vaccine.SoLuong < item.SoLuong)
                            {
                                throw new Exception($"Vaccine {vaccine.TenVC} không đủ số lượng.");
                            }
                            donGia = vaccine.GiaBan;
                            tenSanPham = vaccine.TenVC;

                            // Trừ tồn kho
                            vaccine.SoLuong -= item.SoLuong;
                        }
                        else if (item.LoaiSanPham == "GOIVACCINE")
                        {
                            var goi = _context.GoiVaccines.Find(item.MaSanPham);
                            if (goi == null)
                            {
                                throw new Exception($"Gói vaccine {item.MaSanPham} không tồn tại.");
                            }
                            donGia = goi.GiaGoi;
                            tenSanPham = goi.TenGoi;
                        }

                        decimal thanhTien = donGia * item.SoLuong;
                        tongTien += thanhTien;

                        chiTietList.Add(new ChiTietHoaDon
                        {
                            SoLuong = item.SoLuong,
                            DonGia = donGia,
                            MaSanPham = item.MaSanPham,
                            LoaiSanPham = item.LoaiSanPham
                        });
                    }

                    // Áp dụng khuyến mãi nếu có
                    decimal tienGiam = 0;
                    if (!string.IsNullOrEmpty(MaKM))
                    {
                        var khuyenMai = _context.KhuyenMais.Find(MaKM);
                        if (khuyenMai != null
                            && khuyenMai.NgayBatDau <= DateTime.Now
                            && khuyenMai.NgayKetThuc >= DateTime.Now
                            && khuyenMai.TrangThai == true)
                        {
                            var sanPhamApDung = _context.ChiTietKhuyenMais
                                .Where(ct => ct.MaKM == MaKM)
                                .Select(ct => new { ct.MaSanPham, ct.LoaiSanPham })
                                .ToList();

                            if (sanPhamApDung.Any())
                            {
                                var tongTienApDung = chiTietList
                                    .Where(ct => sanPhamApDung.Any(sp => sp.MaSanPham == ct.MaSanPham
                                                                       && sp.LoaiSanPham == ct.LoaiSanPham))
                                    .Sum(ct => ct.DonGia * ct.SoLuong);

                                if (khuyenMai.KieuGiam == "PhanTram")
                                {
                                    tienGiam = tongTienApDung * khuyenMai.GiaTriGiam / 100;
                                }
                                else
                                {
                                    tienGiam = khuyenMai.GiaTriGiam;
                                }
                            }
                            else
                            {
                                if (khuyenMai.KieuGiam == "PhanTram")
                                {
                                    tienGiam = tongTien * khuyenMai.GiaTriGiam / 100;
                                }
                                else
                                {
                                    tienGiam = khuyenMai.GiaTriGiam;
                                }
                            }

                            if (tienGiam > tongTien)
                            {
                                tienGiam = tongTien;
                            }
                        }
                    }

                    decimal tongTienSauGiam = tongTien - tienGiam;

                    // Tạo mã hóa đơn
                    string maHD = TaoMaHoaDon();

                    // Validate và chuẩn hóa MaKM
                    string maKMValid = null;
                    if (!string.IsNullOrWhiteSpace(MaKM))
                    {
                        MaKM = MaKM.Trim();
                        if (MaKM.Length <= 10)
                        {
                            maKMValid = MaKM;
                        }
                    }

                    // Tạo hóa đơn
                    var hoaDon = new HoaDon
                    {
                        MaHD = maHD,
                        NgayLap = DateTime.Now,
                        TongTien = tongTienSauGiam,
                        TrangThai = true,
                        MaKH = kh.MaKH,
                        MaNV = null, // Website không có nhân viên
                        MaKM = maKMValid,
                        KhachHang = null, // Không set navigation property
                        NhanVien = null,
                        KhuyenMai = null
                    };

                    _context.HoaDons.Add(hoaDon);
                    _context.SaveChanges();

                    // Thêm chi tiết hóa đơn
                    // Lấy số thứ tự tiếp theo cho MaCTHD
                    var lastCTHD = _context.ChiTietHoaDons
                        .OrderByDescending(ct => ct.MaCTHD)
                        .FirstOrDefault();
                    
                    int nextNumber = 1;
                    if (lastCTHD != null)
                    {
                        string numberPart = lastCTHD.MaCTHD.Substring(4);
                        nextNumber = int.Parse(numberPart) + 1;
                    }

                    foreach (var chiTiet in chiTietList)
                    {
                        chiTiet.MaCTHD = "CTHD" + nextNumber.ToString("D6");
                        chiTiet.MaHD = maHD;
                        chiTiet.HoaDon = null; // Không set navigation property
                        _context.ChiTietHoaDons.Add(chiTiet);
                        nextNumber++;
                    }
                    _context.SaveChanges();

                    // Tạo lịch tiêm cho các vaccine (không tạo cho gói vaccine)
                    // Tìm hồ sơ tiêm chủng của khách hàng thông qua bảng LienKetHoSo
                    var lienKet = _context.LienKetHoSos
                        .FirstOrDefault(lk => lk.MaKH == kh.MaKH);
                    
                    var hoSoTiemChung = lienKet != null 
                        ? _context.HoSoTiemChungs.Find(lienKet.MaHSTC) 
                        : null;

                    // Nếu chưa có hồ sơ tiêm chủng, tự động tạo
                    if (hoSoTiemChung == null)
                    {
                        var lastHSTC = _context.HoSoTiemChungs
                            .OrderByDescending(hs => hs.MaHSTC)
                            .FirstOrDefault();

                        int hstcNumber = 1;
                        if (lastHSTC != null)
                        {
                            string numberPart = lastHSTC.MaHSTC.Substring(4);
                            hstcNumber = int.Parse(numberPart) + 1;
                        }

                        hoSoTiemChung = new HoSoTiemChung
                        {
                            MaHSTC = "HSTC" + hstcNumber.ToString("D6"),
                            HoTen = kh.HoTen,
                            GioiTinh = kh.GioiTinh,
                            NgaySinh = kh.NgaySinh ?? DateTime.Now,
                            CCCD = kh.CCCD,
                            GhiChu = "Tự động tạo khi đặt lịch online",
                            TrangThai = true,
                            LienKetHoSo = null,
                            LichTiem = null
                        };
                        _context.HoSoTiemChungs.Add(hoSoTiemChung);
                        _context.SaveChanges();

                        // Tạo liên kết
                        var lastLienKet = _context.LienKetHoSos
                            .OrderByDescending(lk => lk.MaLK)
                            .FirstOrDefault();

                        int lienKetNumber = 1;
                        if (lastLienKet != null)
                        {
                            string numberPart = lastLienKet.MaLK.Substring(2);
                            lienKetNumber = int.Parse(numberPart) + 1;
                        }

                        var newLienKet = new LienKetHoSo
                        {
                            MaLK = "LK" + lienKetNumber.ToString("D8"),
                            VaiTro = "Chính mình",
                            NgayLienKet = DateTime.Now,
                            MaKH = kh.MaKH,
                            MaHSTC = hoSoTiemChung.MaHSTC,
                            KhachHang = null,
                            HoSoTiemChung = null
                        };
                        _context.LienKetHoSos.Add(newLienKet);
                        _context.SaveChanges();
                    }

                    // Tạo lịch tiêm với ngày giờ đã chọn
                    DateTime ngayHen = DateTime.Now.AddDays(7); // Mặc định
                    if (!string.IsNullOrEmpty(NgayHenTiem))
                    {
                        DateTime.TryParse(NgayHenTiem, out ngayHen);
                        // Thêm giờ nếu có
                        if (!string.IsNullOrEmpty(GioHenTiem))
                        {
                            var gioParts = GioHenTiem.Split(':');
                            if (gioParts.Length == 2)
                            {
                                int gio = int.Parse(gioParts[0]);
                                int phut = int.Parse(gioParts[1]);
                                ngayHen = new DateTime(ngayHen.Year, ngayHen.Month, ngayHen.Day, gio, phut, 0);
                            }
                        }
                    }

                    var lastLichTiem = _context.LichTiems
                        .OrderByDescending(lt => lt.MaLT)
                        .FirstOrDefault();

                    int lichTiemNumber = 1;
                    if (lastLichTiem != null)
                    {
                        string numberPart = lastLichTiem.MaLT.Substring(2);
                        lichTiemNumber = int.Parse(numberPart) + 1;
                    }

                    foreach (var chiTiet in chiTietList)
                    {
                        // Chỉ tạo lịch tiêm cho vaccine, không tạo cho gói vaccine
                        if (chiTiet.LoaiSanPham == "VACCINE")
                        {
                            var lichTiem = new LichTiem
                            {
                                MaLT = "LT" + lichTiemNumber.ToString("D8"),
                                NgayHenTiem = ngayHen,
                                NgayTiemThucTe = null,
                                SoMui = 1,
                                TrangThai = "Chưa tiêm",
                                GhiChu = "Đặt lịch qua website - Mã HĐ: " + maHD,
                                MaHSTC = hoSoTiemChung.MaHSTC,
                                MaVC = chiTiet.MaSanPham,
                                MaNV = null,
                                HoSoTiemChung = null,
                                Vaccine = null,
                                NhanVien = null
                            };
                            _context.LichTiems.Add(lichTiem);
                            lichTiemNumber++;
                        }
                    }
                    _context.SaveChanges();

                    // Xóa giỏ hàng sau khi đã lưu hóa đơn
                    foreach (var item in gioHangItems)
                    {
                        _context.GioHangs.Remove(item);
                    }
                    _context.SaveChanges();

                    transaction.Commit();

                    return Json(new
                    {
                        success = true,
                        message = "Thanh toán thành công!",
                        maHD = maHD,
                        redirectUrl = Url.Action("ChiTiet", "HoaDon", new { id = maHD })
                    });
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    transaction.Rollback();
                    var errorMessages = new List<string>();
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            errorMessages.Add($"{validationErrors.Entry.Entity.GetType().Name}.{validationError.PropertyName}: {validationError.ErrorMessage}");
                        }
                    }
                    var fullErrorMessage = string.Join(" | ", errorMessages);
                    return Json(new { success = false, message = "Validation: " + fullErrorMessage });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var innerMessage = ex.InnerException != null ? " Inner: " + ex.InnerException.Message : "";
                    return Json(new { success = false, message = "Error: " + ex.Message + innerMessage });
                }
            }
        }

        // GET: HoaDon/ChiTiet/{id}
        public ActionResult ChiTiet(string id)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để xem hóa đơn.";
                return RedirectToAction("Login", "Account");
            }

            var hoaDon = _context.HoaDons
                .Include(hd => hd.ChiTietHoaDon)
                .Include(hd => hd.KhuyenMai)
                .FirstOrDefault(hd => hd.MaHD == id && hd.MaKH == kh.MaKH);

            if (hoaDon == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy hóa đơn.";
                return RedirectToAction("Index");
            }

            var chiTietVMs = new List<ChiTietHoaDonViewModel>();

            foreach (var ct in hoaDon.ChiTietHoaDon)
            {
                string tenSanPham = "";
                string hinhAnh = "";

                if (ct.LoaiSanPham == "VACCINE")
                {
                    var vaccine = _context.Vaccines.Find(ct.MaSanPham);
                    if (vaccine != null)
                    {
                        tenSanPham = vaccine.TenVC;
                        hinhAnh = vaccine.HinhAnh;
                    }
                }
                else if (ct.LoaiSanPham == "GOIVACCINE")
                {
                    var goi = _context.GoiVaccines.Find(ct.MaSanPham);
                    if (goi != null)
                    {
                        tenSanPham = goi.TenGoi;
                        hinhAnh = goi.HinhAnh;
                    }
                }

                chiTietVMs.Add(new ChiTietHoaDonViewModel
                {
                    MaCTHD = ct.MaCTHD,
                    SoLuong = ct.SoLuong,
                    DonGia = ct.DonGia,
                    MaSanPham = ct.MaSanPham,
                    LoaiSanPham = ct.LoaiSanPham,
                    MaHD = ct.MaHD,
                    TenSanPham = tenSanPham,
                    HinhAnh = hinhAnh
                });
            }

            var model = new HoaDonViewModel
            {
                MaHD = hoaDon.MaHD,
                NgayLap = hoaDon.NgayLap,
                TongTien = hoaDon.TongTien,
                TrangThai = hoaDon.TrangThai.HasValue && hoaDon.TrangThai.Value ? "Đã thanh toán" : "Chưa thanh toán",
                MaKH = hoaDon.MaKH,
                MaKM = hoaDon.MaKM,
                TenKH = kh.HoTen,
                TenKM = hoaDon.KhuyenMai?.TenKM,
                ChiTietHoaDon = chiTietVMs
            };

            return View(model);
        }

        // Helper: Load giỏ hàng items
        private List<GioHangItemViewModel> LoadGioHangItems(string maKH)
        {
            var gioHang = _context.GioHangs
                .Where(g => g.MaKH == maKH)
                .ToList();

            var items = new List<GioHangItemViewModel>();

            foreach (var item in gioHang)
            {
                if (item.LoaiSanPham == "VACCINE")
                {
                    var vaccine = _context.Vaccines.Find(item.MaSanPham);
                    if (vaccine != null)
                    {
                        items.Add(new GioHangItemViewModel
                        {
                            MaGH = item.MaGH,
                            MaSanPham = item.MaSanPham,
                            TenSanPham = vaccine.TenVC,
                            LoaiSanPham = item.LoaiSanPham,
                            DonGia = vaccine.GiaBan,
                            SoLuong = item.SoLuong,
                            ThanhTien = vaccine.GiaBan * item.SoLuong,
                            HinhAnh = vaccine.HinhAnh
                        });
                    }
                }
                else if (item.LoaiSanPham == "GOIVACCINE")
                {
                    var goi = _context.GoiVaccines.Find(item.MaSanPham);
                    if (goi != null)
                    {
                        items.Add(new GioHangItemViewModel
                        {
                            MaGH = item.MaGH,
                            MaSanPham = item.MaSanPham,
                            TenSanPham = goi.TenGoi,
                            LoaiSanPham = item.LoaiSanPham,
                            DonGia = goi.GiaGoi,
                            SoLuong = item.SoLuong,
                            ThanhTien = goi.GiaGoi * item.SoLuong,
                            HinhAnh = goi.HinhAnh
                        });
                    }
                }
            }

            return items;
        }

        // Helper: Tạo mã hóa đơn tự động
        private string TaoMaHoaDon()
        {
            var lastHD = _context.HoaDons
                .OrderByDescending(hd => hd.MaHD)
                .FirstOrDefault();

            if (lastHD == null)
            {
                return "HD00000001";
            }

            string prefix = "HD";
            string numberPart = lastHD.MaHD.Substring(2);
            int nextNumber = int.Parse(numberPart) + 1;

            return prefix + nextNumber.ToString("D8");
        }

        // Helper: Tạo mã chi tiết hóa đơn tự động
        private string TaoMaChiTietHoaDon()
        {
            var lastCTHD = _context.ChiTietHoaDons
                .OrderByDescending(ct => ct.MaCTHD)
                .FirstOrDefault();

            if (lastCTHD == null)
            {
                return "CTHD000001";
            }

            string prefix = "CTHD";
            string numberPart = lastCTHD.MaCTHD.Substring(4);
            int nextNumber = int.Parse(numberPart) + 1;

            return prefix + nextNumber.ToString("D6");
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
