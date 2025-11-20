using System;
using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class HoaDonViewModel
    {
        public string MaHD { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public bool TrangThai { get; set; }
        public string MaKH { get; set; }
        public string TenKhachHang { get; set; }
        public string MaNV { get; set; }
        public string TenNhanVien { get; set; }
        public string MaKM { get; set; }
        public string TenKhuyenMai { get; set; }
        public decimal? GiamGia { get; set; }
        public List<ChiTietHoaDonViewModel> ChiTietHoaDon { get; set; }
    }

    public class ChiTietHoaDonViewModel
    {
        public string MaCTHD { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string MaHD { get; set; }
    }
}
