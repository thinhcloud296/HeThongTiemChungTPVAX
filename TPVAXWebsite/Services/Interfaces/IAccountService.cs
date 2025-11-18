using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace TPVAXWebsite.Services.Interfaces
{
    /// <summary>
    /// Service xử lý logic nghiệp vụ cho tài khoản và khách hàng
    /// </summary>
    public interface IAccountService
    {
        // Đăng ký, đăng nhập
        bool Register(RegisterViewModel model, out string errorMessage);
        KhachHang Login(string soDienThoai, string matKhau, out string errorMessage);
        bool ChangePassword(string maKH, string oldPassword, string newPassword);
        
        // Quản lý profile
        KhachHang GetKhachHangById(string maKH);
        bool UpdateProfile(KhachHang khachHang);
        
        // Quản lý hồ sơ tiêm chủng
        List<HoSoTiemChung> GetHoSosByKhachHang(string maKH);
        bool AddHoSoTiemChung(HoSoTiemChung hoSo, string maKH, string vaiTro);
    }
}
