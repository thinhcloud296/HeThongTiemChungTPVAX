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
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
