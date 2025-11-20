using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("KhuyenMai")]
    public class KhuyenMai
    {
        [Key]
        [StringLength(10)]
        public string MaKM { get; set; }

        [Required]
        [StringLength(255)]
        public string TenKM { get; set; }

        public string MoTa { get; set; }

        [StringLength(100)]
        public string LoaiKM { get; set; }

        [StringLength(50)]
        public string KieuGiam { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GiaTriGiam { get; set; }

        [Required]
        public DateTime NgayBatDau { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        public bool TrangThai { get; set; } = false;

        // Navigation properties
        public virtual ICollection<HoaDon> HoaDon { get; set; }
        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMai { get; set; }
    }
}
