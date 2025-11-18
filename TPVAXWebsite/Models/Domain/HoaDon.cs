using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        [StringLength(8)]
        public string MaHD { get; set; }

        [Required]
        public DateTime NgayLap { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(18,0)")]
        public decimal TongTien { get; set; }

        public bool TrangThai { get; set; }

        [StringLength(10)]
        public string MaKH { get; set; }

        [StringLength(8)]
        public string MaNV { get; set; }

        [StringLength(8)]
        public string MaKM { get; set; }

        // Navigation properties
        [ForeignKey("MaKH")]
        public virtual KhachHang KhachHang { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

        [ForeignKey("MaKM")]
        public virtual KhuyenMai KhuyenMai { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
