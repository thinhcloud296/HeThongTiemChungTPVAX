using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class TaiKhoanDTO
    {
        string MaTK { get; set; } = string.Empty;
        string MatKhau { get; set; } = string.Empty;

        public TaiKhoanDTO() { }

        public TaiKhoanDTO(string maTK, string matKhau)
        {
            MaTK = maTK;
            MatKhau = matKhau;
        }
    }
}
