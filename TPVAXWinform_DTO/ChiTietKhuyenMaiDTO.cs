using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class ChiTietKhuyenMaiDTO
    {
        public int MaCTKM { get; set; }
        public string MaKM { get; set; }
        public string LoaiSanPham { get; set; } // "VACCINE" hoặc "GOIVACCINE"
        public string MaSanPham { get; set; }

        // Thuộc tính mở rộng (để hiển thị tên sản phẩm lên lưới)
        public string TenSanPham { get; set; }
        public ChiTietKhuyenMaiDTO()
        {
        }

        public ChiTietKhuyenMaiDTO(int maCTKM, string maKM, string loaiSanPham, string maSanPham, string tenSanPham)
        {
            MaCTKM = maCTKM;
            MaKM = maKM;
            LoaiSanPham = loaiSanPham;
            MaSanPham = maSanPham;
            TenSanPham = tenSanPham;
        }
    }
}
