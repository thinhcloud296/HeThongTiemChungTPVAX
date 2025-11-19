using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace TPVAXWebsite.Services.Interfaces
{
    /// <summary>
    /// Service xử lý logic nghiệp vụ cho lịch tiêm
    /// </summary>
    public interface ILichTiemService
    {
        // Đặt lịch tiêm
        bool DatLichTiem(DatLichTiemViewModel model, out string errorMessage);
        List<DateTime> GetAvailableSlots(DateTime ngayHen);
        
        // Quản lý lịch tiêm
        List<LichTiem> GetLichTiemByKhachHang(string maKH);
        List<LichTiem> GetLichTiemByHoSo(string maHSTC);
        LichTiem GetLichTiemDetail(string maLT);
        
        // Lịch sử tiêm chủng
        List<LichSuTiemChungViewModel> GetLichSuTiemChung(string maHSTC);
        bool HuyLichTiem(string maLT, out string errorMessage);
    }
}
