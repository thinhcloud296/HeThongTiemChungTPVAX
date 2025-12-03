using System;
using System.ComponentModel.DataAnnotations;

namespace TPVAXWebsite.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho việc đăng ký tài khoản cho khách hàng đã tồn tại trong hệ thống
    /// (Khách hàng có thông tin nhưng MaTK = null)
    /// </summary>
    public class RegisterExistingCustomerViewModel
    {
        [Display(Name = "Số CCCD/CMND")]
        [Required(ErrorMessage = "Vui lòng nhập số CCCD")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "CCCD phải từ 9-12 ký tự")]
        public string CCCD { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ (10 số, bắt đầu bằng 0)")]
        public string SoDT { get; set; }
    }

    /// <summary>
    /// ViewModel cho bước 2: Xác nhận thông tin và tạo mật khẩu
    /// </summary>
    public class ConfirmAndCreateAccountViewModel
    {
        // Thông tin khách hàng (chỉ hiển thị, không cho sửa)
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SoDT { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }

        // Thông tin tạo tài khoản
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
        public string MatKhau { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string XacNhanMatKhau { get; set; }
    }

    /// <summary>
    /// ViewModel cho việc tìm hồ sơ tiêm chủng theo CCCD (chỉ cần CCCD)
    /// </summary>
    public class TimHoSoTiemChungViewModel
    {
        [Display(Name = "Số CCCD/CMND")]
        [Required(ErrorMessage = "Vui lòng nhập số CCCD")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "CCCD phải từ 9-12 ký tự")]
        public string CCCD { get; set; }
    }

    /// <summary>
    /// ViewModel cho trang xác nhận hồ sơ tiêm chủng và tạo tài khoản
    /// </summary>
    public class XacNhanHoSoTiemChungViewModel
    {
        // Thông tin từ hồ sơ tiêm chủng (chỉ hiển thị)
        public string MaHSTC { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }

        // Thông tin cần nhập thêm
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ (10 số, bắt đầu bằng 0)")]
        public string SoDT { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
        public string MatKhau { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string XacNhanMatKhau { get; set; }
    }
}
