using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class PhieuNhapDTO
    {
        public string MaPN { get; set; } = string.Empty;
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public string MaNV { get; set; } = string.Empty;
        public string MaNCC { get; set; } = string.Empty;
        public bool TrangThai { get; set; } = false;
        public PhieuNhapDTO() { }


        public PhieuNhapDTO(string maPN, DateTime ngayLap, string maNV, string maNCC, bool trangThai)
        {
            MaPN = maPN;
            NgayLap = ngayLap;
            MaNV = maNV;
            MaNCC = maNCC;
            TrangThai = trangThai;
        }
    }
}
