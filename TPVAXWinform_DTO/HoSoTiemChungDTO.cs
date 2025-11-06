using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class HoSoTiemChungDTO
    {
        public HoSoTiemChungDTO(string maHSTC, string hoTen, string gioiTinh, DateTime ngaySinh, string quanHeKH, string cCCD, string ghiChu, bool trangThai)
        {
            MaHSTC = maHSTC;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            QuanHeKH = quanHeKH;
            CCCD = cCCD;
            GhiChu = ghiChu;
            TrangThai = trangThai;
        }
        public HoSoTiemChungDTO()
        {
        }
        public string MaHSTC { get; set; } = string.Empty;   // CHAR(10) PK – ví dụ: HSTM123456
        public string HoTen { get; set; } = string.Empty;    // NVARCHAR(100) NOT NULL
        public string GioiTinh { get; set; } = string.Empty; // NVARCHAR(10)
        public DateTime NgaySinh { get; set; }               // DATE NOT NULL
        public string QuanHeKH { get; set; } = string.Empty; // NVARCHAR(50)
        public string CCCD { get; set; } = string.Empty;     // VARCHAR(12)
        public string GhiChu { get; set; } = string.Empty;   // NVARCHAR(MAX)
        public bool TrangThai { get; set; } = true;

    }
}
