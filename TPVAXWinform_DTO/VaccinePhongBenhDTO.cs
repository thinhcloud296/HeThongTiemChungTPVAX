using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class VaccinePhongBenhDTO
    {
        public string MaVC { get; set; } = string.Empty;
        public string MaLoaiBenh { get; set; } = string.Empty;
        public string GhiChu { get; set; } = string.Empty;

        public VaccinePhongBenhDTO() { }

        public VaccinePhongBenhDTO(string maVC, string maLoaiBenh, string ghiChu)
        {
            MaVC = maVC;
            MaLoaiBenh = maLoaiBenh;
            GhiChu = ghiChu;
        }
    }
}
