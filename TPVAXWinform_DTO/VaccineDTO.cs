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
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public string MaLoai { get; set; } = string.Empty;

        public VaccineDTO() { }

        public VaccineDTO(string maVC, string tenVC, decimal giaBan, int soLuongTon, string maLoai)
        {
            MaVC = maVC;
            TenVC = tenVC;
            GiaBan = giaBan;
            SoLuongTon = soLuongTon;
            MaLoai = maLoai;
        }
    }
}
