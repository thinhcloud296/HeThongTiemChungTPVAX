using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class LoaiVaccineDTO
    {
        public string MaLoai { get; set; } = string.Empty;
        public string TenLoai { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;

        public LoaiVaccineDTO() { }

        public LoaiVaccineDTO(string maLoai, string tenLoai, string moTa)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
            MoTa = moTa;
        }
    }
}
