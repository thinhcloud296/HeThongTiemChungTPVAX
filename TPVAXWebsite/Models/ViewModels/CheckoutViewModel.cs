using System;
using System.Collections.Generic;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public KhachHang KhachHang { get; set; }
        public List<GioHangItemViewModel> GioHang { get; set; }
        public decimal TongTienTruocGiam { get; set; }
        public decimal TienGiam { get; set; }
        public decimal TongTienSauGiam { get; set; }
        public List<KhuyenMai> KhuyenMais { get; set; }
        public string MaKMApDung { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string GhiChu { get; set; }
        
        // Thông tin lịch hẹn tiêm
        public DateTime? NgayHenTiem { get; set; }
        public string GioHenTiem { get; set; }
    }
}
