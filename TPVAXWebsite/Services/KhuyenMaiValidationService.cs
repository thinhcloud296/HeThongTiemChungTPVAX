using System;
using System.Data.Entity;
using System.Linq;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Services
{
    /// <summary>
    /// Service validate và quản lý khuyến mãi
    /// Fix lỗi #5: Khuyến mãi không kiểm tra điều kiện áp dụng
    /// </summary>
    public class KhuyenMaiValidationService : IDisposable
    {
        private readonly TPVAXDbContext _context;

        // Giới hạn số lần sử dụng khuyến mãi mỗi khách hàng
        private const int MAX_SU_DUNG_MOI_KHACH = 1;

        public KhuyenMaiValidationService()
        {
            _context = new TPVAXDbContext();
        }

        public KhuyenMaiValidationService(TPVAXDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Kết quả validation khuyến mãi
        /// </summary>
        public class KhuyenMaiValidationResult
        {
            public bool IsValid { get; set; }
            public string ErrorMessage { get; set; }
            public KhuyenMai KhuyenMai { get; set; }
            public decimal TienGiam { get; set; }

            public static KhuyenMaiValidationResult Success(KhuyenMai km, decimal tienGiam)
            {
                return new KhuyenMaiValidationResult
                {
                    IsValid = true,
                    KhuyenMai = km,
                    TienGiam = tienGiam
                };
            }

            public static KhuyenMaiValidationResult Fail(string message)
            {
                return new KhuyenMaiValidationResult
                {
                    IsValid = false,
                    ErrorMessage = message
                };
            }
        }

        /// <summary>
        /// Kiểm tra khách hàng có thể sử dụng mã khuyến mãi này không
        /// </summary>
        public KhuyenMaiValidationResult ValidateKhuyenMai(string maKM, string maKH, decimal tongTienDonHang)
        {
            if (string.IsNullOrEmpty(maKM))
            {
                return KhuyenMaiValidationResult.Fail("Mã khuyến mãi không được để trống.");
            }

            // 1. Kiểm tra khuyến mãi tồn tại và còn hiệu lực
            var khuyenMai = _context.KhuyenMais
                .Include(km => km.ChiTietKhuyenMai)
                .FirstOrDefault(km => km.MaKM == maKM);

            if (khuyenMai == null)
            {
                return KhuyenMaiValidationResult.Fail("Mã khuyến mãi không tồn tại.");
            }

            // 2. Kiểm tra trạng thái
            if (!khuyenMai.TrangThai)
            {
                return KhuyenMaiValidationResult.Fail("Mã khuyến mãi đã bị vô hiệu hóa.");
            }

            // 3. Kiểm tra thời gian hiệu lực
            var now = DateTime.Now;
            if (now < khuyenMai.NgayBatDau)
            {
                return KhuyenMaiValidationResult.Fail(
                    $"Mã khuyến mãi chưa có hiệu lực. Bắt đầu từ {khuyenMai.NgayBatDau:dd/MM/yyyy}.");
            }

            if (now > khuyenMai.NgayKetThuc)
            {
                return KhuyenMaiValidationResult.Fail(
                    $"Mã khuyến mãi đã hết hạn từ {khuyenMai.NgayKetThuc:dd/MM/yyyy}.");
            }

            // 4. Kiểm tra số lần sử dụng của khách hàng này
            int soLanDaSuDung = _context.HoaDons
                .Count(hd => hd.MaKH == maKH 
                          && hd.MaKM == maKM 
                          && hd.TrangThai == true);

            if (soLanDaSuDung >= MAX_SU_DUNG_MOI_KHACH)
            {
                return KhuyenMaiValidationResult.Fail(
                    $"Bạn đã sử dụng mã khuyến mãi này {soLanDaSuDung} lần. " +
                    $"Mỗi khách hàng chỉ được sử dụng tối đa {MAX_SU_DUNG_MOI_KHACH} lần.");
            }

            // 5. Tính tiền giảm
            decimal tienGiam = TinhTienGiam(khuyenMai, tongTienDonHang);

            return KhuyenMaiValidationResult.Success(khuyenMai, tienGiam);
        }

        /// <summary>
        /// Kiểm tra và tính tiền giảm cho sản phẩm cụ thể trong giỏ hàng
        /// </summary>
        public KhuyenMaiValidationResult ValidateKhuyenMaiChoGioHang(
            string maKM, 
            string maKH, 
            System.Collections.Generic.List<GioHangItem> gioHangItems)
        {
            var baseValidation = ValidateKhuyenMai(maKM, maKH, 0);
            if (!baseValidation.IsValid)
            {
                return baseValidation;
            }

            var khuyenMai = baseValidation.KhuyenMai;

            // Lấy danh sách sản phẩm áp dụng khuyến mãi
            var sanPhamApDung = _context.ChiTietKhuyenMais
                .Where(ct => ct.MaKM == maKM)
                .Select(ct => new { ct.MaSanPham, ct.LoaiSanPham })
                .ToList();

            decimal tongTienApDung = 0;

            if (sanPhamApDung.Any())
            {
                // Khuyến mãi áp dụng cho sản phẩm cụ thể
                foreach (var item in gioHangItems)
                {
                    if (sanPhamApDung.Any(sp => sp.MaSanPham == item.MaSanPham 
                                             && sp.LoaiSanPham == item.LoaiSanPham))
                    {
                        tongTienApDung += item.ThanhTien;
                    }
                }

                if (tongTienApDung == 0)
                {
                    return KhuyenMaiValidationResult.Fail(
                        "Mã khuyến mãi này không áp dụng cho các sản phẩm trong giỏ hàng của bạn.");
                }
            }
            else
            {
                // Khuyến mãi áp dụng toàn bộ đơn hàng
                tongTienApDung = gioHangItems.Sum(item => item.ThanhTien);
            }

            decimal tienGiam = TinhTienGiam(khuyenMai, tongTienApDung);

            return KhuyenMaiValidationResult.Success(khuyenMai, tienGiam);
        }

        /// <summary>
        /// Tính tiền giảm dựa trên loại khuyến mãi
        /// </summary>
        private decimal TinhTienGiam(KhuyenMai khuyenMai, decimal tongTien)
        {
            decimal tienGiam = 0;

            if (khuyenMai.KieuGiam == "PhanTram" || khuyenMai.KieuGiam == "Phần trăm" || khuyenMai.KieuGiam == "%")
            {
                tienGiam = tongTien * khuyenMai.GiaTriGiam / 100;
            }
            else // SoTien, Tiền mặt, VND
            {
                tienGiam = khuyenMai.GiaTriGiam;
            }

            // Không giảm quá tổng tiền
            if (tienGiam > tongTien)
            {
                tienGiam = tongTien;
            }

            return tienGiam;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

    /// <summary>
    /// DTO cho item trong giỏ hàng (dùng cho validation)
    /// </summary>
    public class GioHangItem
    {
        public string MaSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
