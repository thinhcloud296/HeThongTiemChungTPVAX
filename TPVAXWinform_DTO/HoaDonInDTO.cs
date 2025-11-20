using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class HoaDonInDTO
    {
        public string MaHD { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTienHoaDon { get; set; }
        public string TenKhachHang { get; set; }
        public string SDTKhachHang { get; set; }
        public string DiaChiKhachHang { get; set; }
        public string TenThuNgan { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }

        // --- THÊM MỚI: Hiển thị giá gốc và giá khuyến mãi ---
        public decimal GiaGoc { get; set; }        // Giá gốc (chưa giảm)
        public decimal DonGia { get; set; } // Giá sau khuyến mãi (giá thực tế)
        public decimal TienGiam { get; set; }        // Số tiền được giảm
        // --- KẾT THÚC THÊM MỚI ---

    }
}
