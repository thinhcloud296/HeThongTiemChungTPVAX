using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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

            // Xóa thông báo lỗi cũ khi vào trang checkout
            TempData.Remove("ErrorMessage");
            TempData.Remove("SuccessMessage");

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

            // Load danh sách hồ sơ tiêm chủng của khách hàng
            var danhSachHoSo = LoadDanhSachHoSo(kh.MaKH);

            decimal tongTien = gioHangItems.Sum(item => item.ThanhTien);

            var model = new CheckoutViewModel
            {
                KhachHang = kh,
                GioHang = gioHangItems,
                TongTienTruocGiam = tongTien,
                TienGiam = 0,
                TongTienSauGiam = tongTien,
                KhuyenMais = khuyenMais,
                DiaChiGiaoHang = kh.DiaChi,
                DanhSachHoSo = danhSachHoSo
            };

            return View(model);
        }

        /// <summary>
        /// Load danh sách hồ sơ tiêm chủng của khách hàng
        /// </summary>
        private List<HoSoTiemChungSelectItem> LoadDanhSachHoSo(string maKH)
        {
            var hoSoList = (from lk in _context.LienKetHoSos
                            join hs in _context.HoSoTiemChungs on lk.MaHSTC equals hs.MaHSTC
                            where lk.MaKH == maKH && hs.TrangThai == true
                            select new HoSoTiemChungSelectItem
                            {
                                MaHSTC = hs.MaHSTC,
                                HoTen = hs.HoTen,
                                NgaySinh = hs.NgaySinh,
                                VaiTro = lk.VaiTro
                            }).ToList();

            return hoSoList;
        }

        // POST: HoaDon/ApDungKhuyenMai
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        // Hỗ trợ thanh toán cho nhiều người (hồ sơ) và tạo nhiều lịch tiêm theo SoMuiToiDa
        // Mỗi người tiêm có ngày giờ hẹn riêng trong DanhSachNguoiTiem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult XacNhanThanhToan(string MaKM, string DanhSachNguoiTiem)
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

                    // Parse danh sách người tiêm từ JSON
                    // Format: [{"MaGH": 1, "MaSanPham": "VC00000001", "LoaiSanPham": "VACCINE", "MaHSTC": "HSTC000001"}, ...]
                    var nguoiTiemList = new List<NguoiTiemItem>();
                    if (!string.IsNullOrEmpty(DanhSachNguoiTiem))
                    {
                        try
                        {
                            var serializer = new JavaScriptSerializer();
                            nguoiTiemList = serializer.Deserialize<List<NguoiTiemItem>>(DanhSachNguoiTiem);
                        }
                        catch
                        {
                            // Nếu parse lỗi, để trống
                        }
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

                            // FIX: Kiểm tra và trừ tồn kho các vaccine trong gói
                            var chiTietGoi = _context.ChiTietGoiVaccines
                                .Where(ct => ct.MaGoi == item.MaSanPham)
                                .ToList();

                            foreach (var ctGoi in chiTietGoi)
                            {
                                var vaccineInGoi = _context.Vaccines.Find(ctGoi.MaVC);
                                if (vaccineInGoi == null)
                                {
                                    throw new Exception($"Vaccine {ctGoi.MaVC} trong gói {goi.TenGoi} không tồn tại.");
                                }

                                // Tính số lượng cần: số mũi trong gói * số lượng gói đặt
                                int soLuongCan = (ctGoi.SoMui ?? 1) * item.SoLuong;
                                
                                if (vaccineInGoi.SoLuong < soLuongCan)
                                {
                                    throw new Exception($"Vaccine {vaccineInGoi.TenVC} trong gói {goi.TenGoi} không đủ số lượng. Cần {soLuongCan}, còn {vaccineInGoi.SoLuong}.");
                                }

                                // Trừ tồn kho vaccine trong gói
                                vaccineInGoi.SoLuong -= soLuongCan;
                            }
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

                    // Thêm chi tiết hóa đơn với KeyGenerator thread-safe
                    foreach (var chiTiet in chiTietList)
                    {
                        string maCTHD = TPVAXWebsite.Common.KeyGenerator.GenMaCTHD();
                        int cthdAttempt = 0;
                        while (_context.ChiTietHoaDons.Any(ct => ct.MaCTHD == maCTHD) && cthdAttempt < 10)
                        {
                            maCTHD = TPVAXWebsite.Common.KeyGenerator.GenMaCTHD();
                            cthdAttempt++;
                        }
                        
                        chiTiet.MaCTHD = maCTHD;
                        chiTiet.MaHD = maHD;
                        chiTiet.HoaDon = null; // Không set navigation property
                        _context.ChiTietHoaDons.Add(chiTiet);
                    }
                    _context.SaveChanges();

                    // Tạo lịch tiêm cho từng sản phẩm trong giỏ hàng
                    // Hỗ trợ nhiều người tiêm cho cùng 1 sản phẩm (khi số lượng > 1)
                    foreach (var gioHangItem in gioHangItems)
                    {
                        // Lấy danh sách người tiêm cho sản phẩm này (có thể có nhiều người nếu số lượng > 1)
                        var nguoiTiemChoSanPham = nguoiTiemList
                            .Where(nt => nt.MaGH == gioHangItem.MaGH || 
                                   (nt.MaSanPham == gioHangItem.MaSanPham && nt.LoaiSanPham == gioHangItem.LoaiSanPham))
                            .OrderBy(nt => nt.Index)
                            .ToList();

                        // Nếu không có người tiêm nào được chọn, tạo 1 entry mặc định
                        if (!nguoiTiemChoSanPham.Any())
                        {
                            for (int i = 0; i < gioHangItem.SoLuong; i++)
                            {
                                nguoiTiemChoSanPham.Add(new NguoiTiemItem
                                {
                                    MaGH = gioHangItem.MaGH,
                                    MaSanPham = gioHangItem.MaSanPham,
                                    LoaiSanPham = gioHangItem.LoaiSanPham,
                                    MaHSTC = null,
                                    Index = i
                                });
                            }
                        }

                        // Tạo lịch tiêm cho từng người tiêm
                        foreach (var nguoiTiem in nguoiTiemChoSanPham)
                        {
                            string maHSTC = nguoiTiem.MaHSTC;

                            // Nếu không có hồ sơ được chọn, lấy hồ sơ mặc định của khách hàng
                            if (string.IsNullOrEmpty(maHSTC))
                            {
                                maHSTC = GetOrCreateDefaultHoSo(kh);
                            }

                            // VALIDATION: Kiểm tra MaHSTC tồn tại trong database
                            var hoSoTonTai = _context.HoSoTiemChungs.Find(maHSTC);
                            if (hoSoTonTai == null)
                            {
                                throw new Exception($"Hồ sơ tiêm chủng {maHSTC} không tồn tại. Vui lòng chọn lại người tiêm.");
                            }

                            // Parse ngày giờ hẹn riêng cho người này
                            DateTime ngayHenMui1 = DateTime.Now.AddDays(1); // Mặc định: ngày mai
                            if (!string.IsNullOrEmpty(nguoiTiem.NgayHenTiem))
                            {
                                if (DateTime.TryParse(nguoiTiem.NgayHenTiem, out DateTime parsedDate))
                                {
                                    ngayHenMui1 = parsedDate;
                                }
                                
                                // Thêm giờ nếu có
                                if (!string.IsNullOrEmpty(nguoiTiem.GioHenTiem))
                                {
                                    var gioParts = nguoiTiem.GioHenTiem.Split(':');
                                    if (gioParts.Length >= 2 && int.TryParse(gioParts[0], out int gio) && int.TryParse(gioParts[1], out int phut))
                                    {
                                        ngayHenMui1 = new DateTime(ngayHenMui1.Year, ngayHenMui1.Month, ngayHenMui1.Day, gio, phut, 0);
                                    }
                                }
                            }

                            // VALIDATION: Kiểm tra ngày hẹn không được trong quá khứ
                            if (ngayHenMui1.Date < DateTime.Now.Date)
                            {
                                throw new Exception("Ngày hẹn tiêm không được là ngày trong quá khứ.");
                            }

                            // VALIDATION: Kiểm tra ngày hẹn không quá xa (tối đa 1 năm)
                            if (ngayHenMui1.Date > DateTime.Now.AddYears(1).Date)
                            {
                                throw new Exception("Ngày hẹn tiêm không được quá 1 năm kể từ hôm nay.");
                            }

                            // Xử lý theo loại sản phẩm
                            if (gioHangItem.LoaiSanPham == "VACCINE")
                            {
                                var vaccine = _context.Vaccines.Find(gioHangItem.MaSanPham);
                                if (vaccine != null)
                                {
                                    // Tạo lịch tiêm cho TẤT CẢ các mũi theo SoMuiToiDa
                                    int soMuiToiDa = vaccine.SoMuiToiDa ?? 1;
                                    int soThangCho = vaccine.SoThangCho ?? 1;

                                    // Nếu SoMuiToiDa = 99 (tiêm nhắc hàng năm), chỉ tạo 1 lịch
                                    if (soMuiToiDa >= 99)
                                    {
                                        soMuiToiDa = 1;
                                    }

                                    for (int mui = 1; mui <= soMuiToiDa; mui++)
                                    {
                                        // Tính ngày hẹn cho mũi này
                                        DateTime ngayHenMui = ngayHenMui1;
                                        if (mui > 1)
                                        {
                                            // Mũi 2 trở đi: cộng thêm (mui - 1) * SoThangCho tháng
                                            ngayHenMui = ngayHenMui1.AddMonths((mui - 1) * soThangCho);
                                        }

                                        // Tạo mã lịch tiêm thread-safe
                                        string maLT = TPVAXWebsite.Common.KeyGenerator.GenMaLT();
                                        int ltAttempt = 0;
                                        while (_context.LichTiems.Any(lt => lt.MaLT == maLT) && ltAttempt < 10)
                                        {
                                            maLT = TPVAXWebsite.Common.KeyGenerator.GenMaLT();
                                            ltAttempt++;
                                        }

                                        var lichTiem = new LichTiem
                                        {
                                            MaLT = maLT,
                                            NgayHenTiem = ngayHenMui,
                                            NgayTiemThucTe = null,
                                            SoMui = mui,
                                            TrangThai = "Chưa tiêm",
                                            GhiChu = $"Đặt lịch qua website - Mã HĐ: {maHD} - Mũi {mui}/{soMuiToiDa}",
                                            MaHSTC = maHSTC,
                                            MaVC = gioHangItem.MaSanPham,
                                            MaNV = null,
                                            HoSoTiemChung = null,
                                            Vaccine = null,
                                            NhanVien = null
                                        };
                                        _context.LichTiems.Add(lichTiem);
                                    }
                                }
                            }
                            else if (gioHangItem.LoaiSanPham == "GOIVACCINE")
                            {
                                // Lấy chi tiết gói vaccine
                                var chiTietGoi = _context.ChiTietGoiVaccines
                                    .Include(ct => ct.Vaccine)
                                    .Where(ct => ct.MaGoi == gioHangItem.MaSanPham)
                                    .ToList();

                                // Nhóm theo MaVC để tính số mũi
                                var vaccineGroups = chiTietGoi.GroupBy(ct => ct.MaVC);

                                foreach (var group in vaccineGroups)
                                {
                                    var vaccine = group.First().Vaccine;
                                    if (vaccine == null) continue;

                                    int soThangCho = vaccine.SoThangCho ?? 1;
                                    int muiIndex = 0;

                                    // Tạo lịch cho từng mũi trong gói
                                    foreach (var ctGoi in group.OrderBy(ct => ct.SoMui))
                                    {
                                        DateTime ngayHenMui = ngayHenMui1;
                                        if (muiIndex > 0)
                                        {
                                            ngayHenMui = ngayHenMui1.AddMonths(muiIndex * soThangCho);
                                        }

                                        // Tạo mã lịch tiêm thread-safe cho gói vaccine
                                        string maLTGoi = TPVAXWebsite.Common.KeyGenerator.GenMaLT();
                                        int ltGoiAttempt = 0;
                                        while (_context.LichTiems.Any(lt => lt.MaLT == maLTGoi) && ltGoiAttempt < 10)
                                        {
                                            maLTGoi = TPVAXWebsite.Common.KeyGenerator.GenMaLT();
                                            ltGoiAttempt++;
                                        }

                                        var lichTiem = new LichTiem
                                        {
                                            MaLT = maLTGoi,
                                            NgayHenTiem = ngayHenMui,
                                            NgayTiemThucTe = null,
                                            SoMui = ctGoi.SoMui ?? (muiIndex + 1),
                                            TrangThai = "Chưa tiêm",
                                            GhiChu = $"Đặt lịch qua website - Mã HĐ: {maHD} - Gói: {gioHangItem.MaSanPham}",
                                            MaHSTC = maHSTC,
                                            MaVC = ctGoi.MaVC,
                                            MaNV = null,
                                            HoSoTiemChung = null,
                                            Vaccine = null,
                                            NhanVien = null
                                        };
                                        _context.LichTiems.Add(lichTiem);
                                        muiIndex++;
                                    }
                                }
                            }
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

        /// <summary>
        /// Lấy hoặc tạo hồ sơ tiêm chủng mặc định cho khách hàng (thread-safe)
        /// </summary>
        private string GetOrCreateDefaultHoSo(KhachHang kh)
        {
            // Tìm hồ sơ tiêm chủng của khách hàng thông qua bảng LienKetHoSo
            var lienKet = _context.LienKetHoSos
                .FirstOrDefault(lk => lk.MaKH == kh.MaKH);

            if (lienKet != null)
            {
                return lienKet.MaHSTC;
            }

            // Nếu chưa có hồ sơ tiêm chủng, tự động tạo với KeyGenerator thread-safe
            string maHSTC = TPVAXWebsite.Common.KeyGenerator.GenMaHSTC();
            
            // Kiểm tra trùng và retry nếu cần
            int maxAttempts = 10;
            int attempt = 0;
            while (_context.HoSoTiemChungs.Any(h => h.MaHSTC == maHSTC) && attempt < maxAttempts)
            {
                maHSTC = TPVAXWebsite.Common.KeyGenerator.GenMaHSTC();
                attempt++;
            }

            var hoSoTiemChung = new HoSoTiemChung
            {
                MaHSTC = maHSTC,
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

            // Tạo liên kết với KeyGenerator thread-safe
            string maLK = TPVAXWebsite.Common.KeyGenerator.GenMaLK();
            attempt = 0;
            while (_context.LienKetHoSos.Any(lk => lk.MaLK == maLK) && attempt < maxAttempts)
            {
                maLK = TPVAXWebsite.Common.KeyGenerator.GenMaLK();
                attempt++;
            }

            var newLienKet = new LienKetHoSo
            {
                MaLK = maLK,
                VaiTro = "Bản thân",
                NgayLienKet = DateTime.Now,
                MaKH = kh.MaKH,
                MaHSTC = hoSoTiemChung.MaHSTC,
                KhachHang = null,
                HoSoTiemChung = null
            };
            _context.LienKetHoSos.Add(newLienKet);
            _context.SaveChanges();

            return hoSoTiemChung.MaHSTC;
        }

        // Helper: Tạo mã hóa đơn tự động (thread-safe)
        private string TaoMaHoaDon()
        {
            string maHD;
            int maxAttempts = 10;
            int attempt = 0;
            
            do
            {
                maHD = TPVAXWebsite.Common.KeyGenerator.GenMaHD();
                attempt++;
            } while (_context.HoaDons.Any(hd => hd.MaHD == maHD) && attempt < maxAttempts);
            
            if (attempt >= maxAttempts)
            {
                // Fallback: dùng GUID nếu vẫn trùng
                maHD = "HD" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            }
            
            return maHD;
        }

        // Helper: Tạo mã chi tiết hóa đơn tự động (thread-safe)
        private string TaoMaChiTietHoaDon()
        {
            string maCTHD;
            int maxAttempts = 10;
            int attempt = 0;
            
            do
            {
                maCTHD = TPVAXWebsite.Common.KeyGenerator.GenMaCTHD();
                attempt++;
            } while (_context.ChiTietHoaDons.Any(ct => ct.MaCTHD == maCTHD) && attempt < maxAttempts);
            
            if (attempt >= maxAttempts)
            {
                // Fallback: dùng GUID nếu vẫn trùng
                maCTHD = "CTHD" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            }
            
            return maCTHD;
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
