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
    }
}
