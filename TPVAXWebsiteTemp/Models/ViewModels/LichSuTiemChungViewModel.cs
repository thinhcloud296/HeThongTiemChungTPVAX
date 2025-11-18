using System;
using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class LichSuTiemChungViewModel
    {
        public string MaHSTC { get; set; }
        public string HoTenNguoiTiem { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string VaiTro { get; set; } // Bản thân, Con, Người thân...
        public List<MuiTiemInfo> DanhSachMuiTiem { get; set; }
    }

    public class MuiTiemInfo
    {
        public string MaLT { get; set; }
        public string TenVaccine { get; set; }
        public DateTime NgayHenTiem { get; set; }
        public DateTime? NgayTiemThucTe { get; set; }
        public int? SoMui { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}
