using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class NhaCungCapDTO
    {
        public string MaNCC { get; set; } = string.Empty;
        public string TenNCC { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SoDT { get; set; } = string.Empty;
        public string TenNganHang { get; set; } = string.Empty;
        public string SoTK { get; set; } = string.Empty;

        public NhaCungCapDTO() { }

        public NhaCungCapDTO(string maNCC, string tenNCC, string diaChi, string email, string soDT, string tenNganHang, string soTK)
        {
            MaNCC = maNCC;
            TenNCC = tenNCC;
            DiaChi = diaChi;
            Email = email;
            SoDT = soDT;
            TenNganHang = tenNganHang;
            SoTK = soTK;
        }
    }
}
