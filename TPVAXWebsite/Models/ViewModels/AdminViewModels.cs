using System;
using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TongKhachHang { get; set; }
        public int TongVaccine { get; set; }
        public int TongLichHen { get; set; }
        public decimal DoanhThuThang { get; set; }
    }

    public class AdminVaccineViewModel
    {
        public string MaVC { get; set; }
        public string TenVC { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public int? SoMuiToiDa { get; set; }
        public int? SoThangCho { get; set; }
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
    }

    public class AdminCustomerViewModel
    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string SoDT { get; set; }
        public string Email { get; set; }
        public string MaTK { get; set; }
    }
}
