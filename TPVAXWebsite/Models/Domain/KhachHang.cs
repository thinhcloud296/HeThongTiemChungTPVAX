using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [StringLength(10)]
        public string MaKH { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(12)]
        public string CCCD { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        [StringLength(500)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(10)]
        public string SoDT { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(8)]
        public string MaTK { get; set; }

        // Navigation properties
        [ForeignKey("MaTK")]
        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual ICollection<LienKetHoSo> LienKetHoSos { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
    }
}
