using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class HoaDonDTO
    {
        public string MaHD { get; set; } = string.Empty;
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public decimal TongTien { get; set; }
        public bool? TrangThai { get; set; } 
        public string MaKH { get; set; }
        public string MaNV { get; set; }
        public string MaKM { get; set; }

        public HoaDonDTO() { }

        public HoaDonDTO(string maHD, DateTime ngayLap, decimal tongTien, bool? trangThai, string maKH, string maNV, string maKM)
        {
            MaHD = maHD;
            NgayLap = ngayLap;
            TongTien = tongTien;
            TrangThai = trangThai;
            MaKH = maKH;
            MaNV = maNV;
            MaKM = maKM;
        }
    }
}
