using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class ChiTietGoiVaccineDTO
    {
        public string MaCTGoi { get; set; } = string.Empty;
        public int? SoMui { get; set; }
        public string GhiChu { get; set; } = string.Empty;
        public string MaGoi { get; set; } = string.Empty;
        public string MaVC { get; set; } = string.Empty;

        public ChiTietGoiVaccineDTO() { }

        public ChiTietGoiVaccineDTO(string maCTGoi, int? soMui, int? thangTiem, string ghiChu, string maGoi, string maVC)
        {
            MaCTGoi = maCTGoi;
            SoMui = soMui;
            GhiChu = ghiChu;
            MaGoi = maGoi;
            MaVC = maVC;
        }
    }
}
