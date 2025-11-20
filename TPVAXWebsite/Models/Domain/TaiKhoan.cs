using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        [StringLength(10)]
        public string MaTK { get; set; }

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; }

        // Navigation properties
        public virtual KhachHang KhachHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
