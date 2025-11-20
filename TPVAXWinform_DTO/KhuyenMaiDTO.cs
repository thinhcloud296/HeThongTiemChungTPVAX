using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class KhuyenMaiDTO
    {
        public KhuyenMaiDTO(string maKM, string tenKM, string moTa, string loaiKM, string kieuGiam, decimal giaTriGiam, DateTime ngayBatDau, DateTime ngayKetThuc, bool trangThai)
        {
            MaKM = maKM;
            TenKM = tenKM;
            MoTa = moTa;
            LoaiKM = loaiKM;
            KieuGiam = kieuGiam;
            GiaTriGiam = giaTriGiam;
            NgayBatDau = ngayBatDau;
            NgayKetThuc = ngayKetThuc;
            TrangThai = trangThai;
        }
        public KhuyenMaiDTO()
        {
        }
        public string MaKM { get; set; }
        public string TenKM { get; set; }
        public string MoTa { get; set; }
        public string LoaiKM { get; set; }
        public string KieuGiam { get; set; } // "PhanTram" hoặc "SoTien"
        public decimal GiaTriGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool TrangThai { get; set; }
    }
}
