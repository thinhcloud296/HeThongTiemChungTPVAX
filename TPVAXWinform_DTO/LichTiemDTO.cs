using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class LichTiemDTO
    {
        public string MaLT { get; set; } = string.Empty;
        public DateTime NgayHenTiem { get; set; } = DateTime.Now;
        public DateTime? NgayTiemThucTe { get; set; }
        public int? SoMui { get; set; }
        public bool? TrangThai { get; set; } = false; // Nullable để hỗ trợ null = đã hủy
        public string GhiChu { get; set; } = string.Empty;
        public string MaHSTC { get; set; } = string.Empty;
        public string MaVC { get; set; } = string.Empty;
        public string MaNV { get; set; } = string.Empty;

        public LichTiemDTO() { }

        public LichTiemDTO(string maLT, DateTime ngayHenTiem, DateTime? ngayTiemThucTe, int? soMui, bool? trangThai, string ghiChu, string maHSTC, string maVC, string maNV)
        {
            MaLT = maLT;
            NgayHenTiem = ngayHenTiem;
            NgayTiemThucTe = ngayTiemThucTe;
            SoMui = soMui;
            TrangThai = trangThai;
            GhiChu = ghiChu;
            MaHSTC = maHSTC;
            MaVC = maVC;
            MaNV = maNV;
        }
    }
}
