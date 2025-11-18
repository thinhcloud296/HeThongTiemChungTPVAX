using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class NhanVienDTO
    {
        public NhanVienDTO()
        {
        }

        public NhanVienDTO(string maNV, string hoTen, string gioiTinh, DateTime? ngaySinh, string cCCD, DateTime ngayVaoLam, int? chucVu, string trangThai, string soDT, string diaChi, string email, string maTK)
        {
            MaNV = maNV;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            CCCD = cCCD;
            NgayVaoLam = ngayVaoLam;
            ChucVu = chucVu;
            TrangThai = trangThai;
            SoDT = soDT;
            DiaChi = diaChi;
            Email = email;
            MaTK = maTK;
        }

        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string CCCD { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public int? ChucVu { get; set; } 
        public string TrangThai { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string MaTK { get; set; }
    }
}
