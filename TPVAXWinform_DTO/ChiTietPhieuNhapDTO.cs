using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class ChiTietPhieuNhapDTO
    {
        public string MaCTPN { get; set; } = string.Empty;
        public string NuocSanXuat { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public DateTime? HanSuDung { get; set; }
        public string MaPN { get; set; } = string.Empty;
        public string MaVC { get; set; } = string.Empty;

        public ChiTietPhieuNhapDTO() { }

        public ChiTietPhieuNhapDTO(string maCTPN, string nuocSanXuat, int soLuong, decimal giaNhap, DateTime? hanSuDung, string maPN, string maVC)
        {
            MaCTPN = maCTPN;
            NuocSanXuat = nuocSanXuat;
            SoLuong = soLuong;
            GiaNhap = giaNhap;
            HanSuDung = hanSuDung;
            MaPN = maPN;
            MaVC = maVC;
        }
    }
}
