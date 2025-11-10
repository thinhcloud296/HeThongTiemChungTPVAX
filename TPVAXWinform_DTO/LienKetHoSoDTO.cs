using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class LienKetHoSoDTO
    {

        public LienKetHoSoDTO()
        {
        }

        public LienKetHoSoDTO(string maLK, string vaiTro, DateTime ngayLienKet, string maKH, string maHSTC)
        {
            MaLK = maLK;
            VaiTro = vaiTro;
            NgayLienKet = ngayLienKet;
            MaKH = maKH;
            MaHSTC = maHSTC;
        }

        public string MaLK { get; set; } = string.Empty; 
        public string VaiTro { get; set; } = string.Empty;
        public DateTime NgayLienKet { get; set; }        
        public string MaKH { get; set; } = string.Empty;  
        public string MaHSTC { get; set; } = string.Empty;

    }
}
