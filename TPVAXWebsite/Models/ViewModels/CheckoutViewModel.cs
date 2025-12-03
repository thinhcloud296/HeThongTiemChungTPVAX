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

        // Danh sách hồ sơ tiêm chủng của khách hàng (để chọn người tiêm)
        public List<HoSoTiemChungSelectItem> DanhSachHoSo { get; set; }
        
        // FIX: Dictionary lưu MaHSTC đã chọn từ trang DatLich
        // Key format: "{MaGH}_{Index}" -> Value: MaHSTC
        public Dictionary<string, string> SelectedHSTCDict { get; set; }
    }

    /// <summary>
    /// Item để hiển thị trong dropdown chọn hồ sơ tiêm chủng
    /// </summary>
    public class HoSoTiemChungSelectItem
    {
        public string MaHSTC { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string VaiTro { get; set; }
        public string DisplayText => $"{HoTen} - {NgaySinh:dd/MM/yyyy} ({VaiTro})";
    }

    /// <summary>
    /// Thông tin người tiêm cho từng sản phẩm trong giỏ hàng
    /// </summary>
    public class NguoiTiemItem
    {
        public int MaGH { get; set; }           // Mã giỏ hàng
        public string MaSanPham { get; set; }   // Mã vaccine/gói
        public string LoaiSanPham { get; set; } // VACCINE hoặc GOIVACCINE
        public string MaHSTC { get; set; }      // Mã hồ sơ tiêm chủng được chọn
        public int Index { get; set; }          // Thứ tự người tiêm (0, 1, 2...) khi số lượng > 1
        public string NgayHenTiem { get; set; } // Ngày hẹn tiêm riêng cho người này
        public string GioHenTiem { get; set; }  // Giờ hẹn tiêm riêng cho người này
    }
}
