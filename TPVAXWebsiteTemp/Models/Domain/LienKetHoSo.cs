using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("LienKetHoSo")]
    public class LienKetHoSo
    {
        [Key]
        [StringLength(10)]
        public string MaLK { get; set; }

        [StringLength(100)]
        public string VaiTro { get; set; }

        [Required]
        public DateTime NgayLienKet { get; set; }

        [Required]
        [StringLength(10)]
        public string MaKH { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHSTC { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("MaKH")]
        public virtual KhachHang KhachHang { get; set; }

        [ForeignKey("MaHSTC")]
        public virtual HoSoTiemChung HoSoTiemChung { get; set; }
    }
}
