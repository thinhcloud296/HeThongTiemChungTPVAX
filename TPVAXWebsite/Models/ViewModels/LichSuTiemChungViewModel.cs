using System;
using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class LichSuTiemChungViewModel
    {
        public string MaHSTC { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CCCD { get; set; }
        public string GhiChu { get; set; }
        public bool TrangThai { get; set; }
        public List<LichTiemViewModel> LichTiem { get; set; }
    }

    public class LichTiemViewModel
    {
        public string MaLT { get; set; }
        public string MaVC { get; set; }
        public string TenVaccine { get; set; }
        public DateTime NgayHenTiem { get; set; }
        public DateTime? NgayTiemThucTe { get; set; }
        public int? SoMui { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string MaHSTC { get; set; }
        public string MaNV { get; set; }
        public string TenNhanVien { get; set; }
    }
}
