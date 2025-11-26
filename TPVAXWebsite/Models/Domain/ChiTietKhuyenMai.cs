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

        [Required]
        [StringLength(50)]
        public string LoaiSanPham { get; set; }

        [Required]
        [StringLength(10)]
        public string MaSanPham { get; set; }

        [Required]
        [StringLength(10)]
        public string MaKM { get; set; }

        // Navigation properties
        [ForeignKey("MaKM")]
        public virtual KhuyenMai KhuyenMai { get; set; }
    }
}
