using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class GioHangViewModel
    {
        public List<GioHangItemViewModel> Items { get; set; }
        public decimal TongTien { get; set; }
        public decimal TienGiam { get; set; }
        public decimal ThanhToan { get; set; }
        public string MaKhuyenMai { get; set; }
    }

    public class GioHangItemViewModel
    {
        public int MaGH { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; } // VACCINE hoặc GOIVACCINE
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;
        public string HinhAnh { get; set; }
    }
}
