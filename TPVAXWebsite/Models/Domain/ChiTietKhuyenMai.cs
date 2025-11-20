using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("ChiTietKhuyenMai")]
    public class ChiTietKhuyenMai
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCTKM { get; set; }

        [StringLength(50)]
        public string LoaiSanPham { get; set; }

        [StringLength(20)]
        public string MaSanPham { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayApDung { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        public string GhiChu { get; set; }

        [Required]
        [StringLength(10)]
        public string MaKM { get; set; }

        // Navigation properties
        [ForeignKey("MaKM")]
        public virtual KhuyenMai KhuyenMai { get; set; }
    }
}
