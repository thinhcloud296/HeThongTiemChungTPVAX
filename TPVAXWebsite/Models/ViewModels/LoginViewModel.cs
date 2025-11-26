using System.ComponentModel.DataAnnotations;

namespace TPVAXWebsite.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email hoặc số điện thoại")]
        [Display(Name = "Email hoặc SĐT")]
        public string TenDangNhap { get; set; } // dùng chung cho email hoặc SĐT

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; }
    }
}
