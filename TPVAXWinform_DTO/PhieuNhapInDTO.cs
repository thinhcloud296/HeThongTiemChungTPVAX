using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DTO
{
    public class PhieuNhapInDTO
    {
        public string MaPN { get; set; }
        public DateTime NgayLap { get; set; }
        public string TenNhanVien { get; set; }
        public string TenNhaCungCap { get; set; }
        public string DiaChiNCC { get; set; }
        public string SDTNCC { get; set; }

        // Thông tin Chi tiết
        public string TenVaccine { get; set; }
        public string NuocSanXuat { get; set; }
        public DateTime? HanSuDung { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal ThanhTien { get; set; }
        public decimal TongTienPhieuNhap { get; set; }
    }
}
