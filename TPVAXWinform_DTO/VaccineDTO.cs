using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class VaccineDTO
    {
        public string MaVC { get; set; } = string.Empty;
        public string TenVC { get; set; } = string.Empty;
        public int SoMuiToiDa { get; set; }
        public int SoThangCho { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public string MaLoai { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;
        public string HinhAnh { get; set; } = string.Empty;

        public VaccineDTO() { }

        public VaccineDTO(string maVC, string tenVC, int soMuiToiDa, int soThangCho, decimal giaBan, int soLuongTon, string maLoai, string moTa, string hinhAnh)
        {
            MaVC = maVC;
            TenVC = tenVC;
            SoMuiToiDa = soMuiToiDa;
            SoThangCho = soThangCho;
            GiaBan = giaBan;
            SoLuongTon = soLuongTon;
            MaLoai = maLoai;
            MoTa = moTa;
            HinhAnh = hinhAnh;
        }
    }
}
