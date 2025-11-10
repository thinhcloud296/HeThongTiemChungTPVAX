using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class LoaiBenhDTO
    {
        public string MaLoaiBenh { get; set; } = string.Empty;
        public string TenBenh { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;
        public string NhomDoiTuong { get; set; } = string.Empty;

        public LoaiBenhDTO() { }

        public LoaiBenhDTO(string maLoaiBenh, string tenBenh, string moTa, string nhomDoiTuong)
        {
            MaLoaiBenh = maLoaiBenh;
            TenBenh = tenBenh;
            MoTa = moTa;
            NhomDoiTuong = nhomDoiTuong;
        }
    }
}
