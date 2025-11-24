using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TPVAXWebsite.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TongKhachHang { get; set; }
        public int TongVaccine { get; set; }
        public int TongLichHen { get; set; }
        public decimal DoanhThuThang { get; set; }
    }

    public class AdminVaccineViewModel
    {
        public string MaVC { get; set; }
        public string TenVC { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public int? SoMuiToiDa { get; set; }
        public int? SoThangCho { get; set; }
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
    }

    public class AdminCustomerViewModel
    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string SoDT { get; set; }
        public string Email { get; set; }
        public string MaTK { get; set; }
    }

    public class AdminNhaCungCapViewModel
    {
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDT { get; set; }
        public string TenNganHang { get; set; }
        public string SoTK { get; set; }
    }

    public class AdminGoiVaccineViewModel
    {
        public string MaGoi { get; set; }
        public string TenGoi { get; set; }
        public string MoTa { get; set; }
        public string DoiTuongApDung { get; set; }
        public decimal GiaGoi { get; set; }
        public string TrangThai { get; set; }
        public string HinhAnh { get; set; }
    }

    public class AdminNhanVienViewModel
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string CCCD { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public int? ChucVu { get; set; }
        public string TrangThai { get; set; }
    }

    public class AdminAppointmentViewModel
    {
        public string MaLT { get; set; }
        public string MaHSTC { get; set; }
        public string TenNguoiTiem { get; set; }
        public string TenVaccine { get; set; }
        public DateTime NgayHenTiem { get; set; }
        public DateTime? NgayTiemThucTe { get; set; }
        public int? SoMui { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string MaNV { get; set; }
        public string TenNhanVien { get; set; }
    }

    /// <summary>
    /// ViewModel cho tạo/sửa vaccine
    /// </summary>
    public class AdminVaccineCreateEditViewModel
    {
        [StringLength(10)]
        public string MaVC { get; set; }

        [Required(ErrorMessage = "Tên vaccine không được để trống")]
        [StringLength(255, ErrorMessage = "Tên vaccine không quá 255 ký tự")]
        public string TenVC { get; set; }

        [Required(ErrorMessage = "Giá bán không được để trống")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0")]
        public decimal GiaBan { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được âm")]
        public int SoLuong { get; set; }

        [Range(1, 10, ErrorMessage = "Số mũi tối đa phải từ 1-10")]
        public int? SoMuiToiDa { get; set; }

        [Range(0, 120, ErrorMessage = "Số tháng chờ phải từ 0-120")]
        public int? SoThangCho { get; set; }

        [Required(ErrorMessage = "Loại vaccine không được để trống")]
        [StringLength(10)]
        public string MaLoai { get; set; }

        [StringLength(255)]
        public string TenLoai { get; set; }

        [StringLength(int.MaxValue)]
        public string MoTa { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        [StringLength(255)]
        public string HinhAnhCu { get; set; }

        /// <summary>
        /// File upload hình ảnh từ form
        /// </summary>
        public HttpPostedFileBase ImageFile { get; set; }

        /// <summary>
        /// Danh sách ID loại bệnh được chọn
        /// </summary>
        public List<string> SelectedLoaiBenhIds { get; set; } = new List<string>();

        /// <summary>
        /// Danh sách loại bệnh để hiển thị dropdown
        /// </summary>
        public List<LoaiBenhDropdownItem> AvailableLoaiBenhs { get; set; } = new List<LoaiBenhDropdownItem>();

        /// <summary>
        /// Danh sách loại vaccine để dropdown
        /// </summary>
        public List<LoaiVaccineDropdownItem> AvailableLoaiVaccines { get; set; } = new List<LoaiVaccineDropdownItem>();
    }

    /// <summary>
    /// Dropdown item cho loại vaccine
    /// </summary>
    public class LoaiVaccineDropdownItem
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
    }

    /// <summary>
    /// Dropdown item cho loại bệnh
    /// </summary>
    public class LoaiBenhDropdownItem
    {
        public string MaLoaiBenh { get; set; }
        public string TenBenh { get; set; }
        public bool IsSelected { get; set; }
    }

    /// <summary>
    /// Response API cho delete vaccine
    /// </summary>
    public class VaccineDeleteResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorType { get; set; } // "CannotDelete", "NotFound", "Error"
    }

    /// <summary>
    /// ViewModel cho tạo/sửa nhà cung cấp
    /// </summary>
    public class AdminNhaCungCapCreateEditViewModel
    {
        [StringLength(10)]
        public string MaNCC { get; set; }

        [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc")]
        [StringLength(255, ErrorMessage = "Tên nhà cung cấp tối đa 255 ký tự")]
        public string TenNCC { get; set; }

        [StringLength(500, ErrorMessage = "Địa chỉ tối đa 500 ký tự")]
        public string DiaChi { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100)]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải là 10 chữ số")]
        [StringLength(10)]
        public string SoDT { get; set; }

        [StringLength(100)]
        public string TenNganHang { get; set; }

        [StringLength(30)]
        public string SoTK { get; set; }
    }

    public class NhaCungCapDeleteResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// ViewModel cho tạo/sửa khuyến mãi
    /// </summary>
    public class AdminKhuyenMaiCreateEditViewModel
    {
        [StringLength(10)]
        public string MaKM { get; set; }

        [Required(ErrorMessage = "Tên khuyến mãi không được để trống")]
        [StringLength(255, ErrorMessage = "Tên khuyến mãi không quá 255 ký tự")]
        public string TenKM { get; set; }

        [StringLength(int.MaxValue)]
        public string MoTa { get; set; }

        [StringLength(50)]
        public string LoaiKM { get; set; }

        [Required(ErrorMessage = "Kiểu giảm không được để trống")]
        [StringLength(50)]
        public string KieuGiam { get; set; }

        [Required(ErrorMessage = "Giá trị giảm không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị giảm phải lớn hơn hoặc bằng 0")]
        public decimal GiaTriGiam { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        public DateTime NgayBatDau { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        public DateTime NgayKetThuc { get; set; }

        public bool TrangThai { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        [StringLength(255)]
        public string HinhAnhCu { get; set; }

        /// <summary>
        /// File upload hình ảnh từ form
        /// </summary>
        public HttpPostedFileBase ImageFile { get; set; }
    }
}

