using System;
using System.Collections.Generic;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho Dashboard Admin
    /// </summary>
    public class AdminDashboardViewModel
    {
        public int TongSoVaccine { get; set; }
        public int TongSoKhachHang { get; set; }
        public int TongSoLichHen { get; set; }
        public decimal TongDoanhThu { get; set; }
        public int LichHenHomNay { get; set; }
        public int VaccineSapHet { get; set; }
        public List<LichTiem> LichHenSapToi { get; set; }
        public List<HoaDon> HoaDonMoiNhat { get; set; }

        public AdminDashboardViewModel()
        {
            LichHenSapToi = new List<LichTiem>();
            HoaDonMoiNhat = new List<HoaDon>();
        }
    }

    /// <summary>
    /// ViewModel cho báo cáo thống kê admin
    /// </summary>
    public class AdminReportsViewModel
    {
        public decimal DoanhThuThang { get; set; }
        public int SoLuongTiemThang { get; set; }
        public object TopVaccines { get; set; }
        public object DoanhThuTheoNgay { get; set; }
    }

    /// <summary>
    /// ViewModel cho chi tiết hóa đơn
    /// </summary>
    public class InvoiceDetailsViewModel
    {
        public Domain.HoaDon HoaDon { get; set; }
        public List<Domain.ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public Domain.KhachHang KhachHang { get; set; }

        public InvoiceDetailsViewModel()
        {
            ChiTietHoaDon = new List<Domain.ChiTietHoaDon>();
        }
    }
}
