using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class GioHangViewModel
    {
        public string MaKH { get; set; }
        public List<GioHangItemViewModel> Items { get; set; }
        public decimal TongTien { get; set; }
        public int TongSoLuong { get; set; }
    }

    public class GioHangItemViewModel
    {
        public int MaGH { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }
    }
}
