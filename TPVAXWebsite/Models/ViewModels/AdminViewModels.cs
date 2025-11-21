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

    public class AdminNhaCungCapViewModel
    {
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDT { get; set; }
        public string TenNganHang { get; set; }
        public string SoTK { get; set; }
    }

    public class AdminGoiVaccineViewModel
    {
        public string MaGoi { get; set; }
        public string TenGoi { get; set; }
        public string MoTa { get; set; }
        public string DoiTuongApDung { get; set; }
        public decimal GiaGoi { get; set; }
        public string TrangThai { get; set; }
        public string HinhAnh { get; set; }
    }

    public class AdminNhanVienViewModel
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string CCCD { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public int? ChucVu { get; set; }
        public string TrangThai { get; set; }
    }

    public class AdminAppointmentViewModel
    {
        public string MaLT { get; set; }
        public string MaHSTC { get; set; }
        public string TenNguoiTiem { get; set; }
        public string TenVaccine { get; set; }
        public DateTime NgayHenTiem { get; set; }
        public DateTime? NgayTiemThucTe { get; set; }
        public int? SoMui { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string MaNV { get; set; }
        public string TenNhanVien { get; set; }
    }
}
