using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("GioHang")]
    public class GioHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaGH { get; set; }

        [Required]
        [StringLength(10)]
        public string MaKH { get; set; }

        [Required]
        [StringLength(10)]
        public string MaSanPham { get; set; }

        [Required]
        [StringLength(20)]
        public string LoaiSanPham { get; set; }

        [Required]
        public int SoLuong { get; set; } = 1;

        // Navigation properties
        [ForeignKey("MaKH")]
        public virtual KhachHang KhachHang { get; set; }
    }
}
