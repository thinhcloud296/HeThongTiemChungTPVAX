using System;

namespace TPVAXWebsite.Models.ViewModels
{
    public class KhuyenMaiViewModel
    {
        public string MaKM { get; set; }
        public string TenKM { get; set; }
        public string MoTa { get; set; }
        public string LoaiKM { get; set; }
        public string KieuGiam { get; set; }
        public decimal GiaTriGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool TrangThai { get; set; }
        public string TrangThaiHienThi { get; set; }
        public int SoNgayConLai { get; set; }
        public string HinhAnh { get; set; }
        
        // Formatted properties for display
        public string GiaTriGiamFormatted
        {
            get
            {
                if (KieuGiam == "Phần trăm" || KieuGiam == "%")
                    return $"{GiaTriGiam}%";
                else
                    return $"{GiaTriGiam:N0}₫";
            }
        }

        public string NgayBatDauFormatted => NgayBatDau.ToString("dd/MM/yyyy");
        public string NgayKetThucFormatted => NgayKetThuc.ToString("dd/MM/yyyy");
    }
}
