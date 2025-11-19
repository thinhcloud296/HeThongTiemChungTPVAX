using System.ComponentModel.DataAnnotations;

namespace TPVAXWebsite.Models.ViewModels
{
    public class LoginViewModel
    {
        // Tên đăng nhập hoặc email (được view sử dụng)
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập hoặc email")]
        [Display(Name = "Tên đăng nhập")]
        [StringLength(100, ErrorMessage = "Tên đăng nhập không vượt quá 100 ký tự")]
        public string TenDangNhap { get; set; }

        // Nếu bạn vẫn muốn hỗ trợ nhập số điện thoại thay cho TenDangNhap,
        // có thể giữ thêm thuộc tính SoDienThoai tùy lựa chọn
        [Display(Name = "Số điện thoại")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải có 10 ký tự")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; }
    }
}
