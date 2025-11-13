using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class GoiVaccineDTO
    {
        public string MaGoi { get; set; } = string.Empty;
        public string TenGoi { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;
        public string DoiTuongApDung { get; set; } = string.Empty;
        public decimal GiaGoi { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        public GoiVaccineDTO() { }

        public GoiVaccineDTO(string maGoi, string tenGoi, string moTa, string doiTuongApDung, decimal giaGoi, string trangThai)
        {
            MaGoi = maGoi;
            TenGoi = tenGoi;
            MoTa = moTa;
            DoiTuongApDung = doiTuongApDung;
            GiaGoi = giaGoi;
            TrangThai = trangThai;
        }
    }
}
