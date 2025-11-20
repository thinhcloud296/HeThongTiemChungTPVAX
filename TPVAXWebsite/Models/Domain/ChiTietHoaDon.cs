using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("ChiTietHoaDon")]
    public class ChiTietHoaDon
    {
        [Key]
        [StringLength(10)]
        public string MaCTHD { get; set; }

        [Required]
        public int SoLuong { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal DonGia { get; set; }

        [Required]
        [StringLength(10)]
        public string MaSanPham { get; set; }

        [Required]
        [StringLength(20)]
        public string LoaiSanPham { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHD { get; set; }

        // Navigation properties
        [ForeignKey("MaHD")]
        public virtual HoaDon HoaDon { get; set; }
    }
}
