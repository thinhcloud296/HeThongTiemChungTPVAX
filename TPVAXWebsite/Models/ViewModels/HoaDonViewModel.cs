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
        public string TenKhuyenMai { get; set; }
        public decimal GiaTriGiam { get; set; }
        public List<ChiTietHoaDonViewModel> ChiTietHoaDons { get; set; }
        public string SoDienThoai { get; set; }
        public int SoLuongSanPham { get; set; }
        public string TenKhachHang { get; set; } // <-- Add this property
    }

    public class ChiTietHoaDonViewModel
    {
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
