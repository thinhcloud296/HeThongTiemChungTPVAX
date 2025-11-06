using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class KhachHangDTO
    {
        public KhachHangDTO(string maKH, string hoTen, string cCCD, DateTime? ngaySinh, string diaChi, string soDT, string email, string maTK)
        {
            MaKH = maKH;
            HoTen = hoTen;
            CCCD = cCCD;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            SoDT = soDT;
            Email = email;
            MaTK = maTK;
        }
        public KhachHangDTO()
        {
        }
        public string MaKH { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string CCCD { get; set; } = string.Empty;
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; } = string.Empty;
        public string SoDT { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MaTK { get; set; } = string.Empty;
    }
}
