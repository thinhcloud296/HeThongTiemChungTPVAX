using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class ChiTietHoaDonDTO
    {
        public string MaCTHD { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string MaSanPham { get; set; } = string.Empty;
        public string LoaiSanPham { get; set; } = string.Empty;
        public string MaHD { get; set; } = string.Empty;

        public ChiTietHoaDonDTO() { }

        public ChiTietHoaDonDTO(string maCTHD, int soLuong, decimal donGia, string maSanPham, string loaiSanPham, string maHD)
        {
            MaCTHD = maCTHD;
            SoLuong = soLuong;
            DonGia = donGia;
            MaSanPham = maSanPham;
            LoaiSanPham = loaiSanPham;
            MaHD = maHD;
        }
    }
}
