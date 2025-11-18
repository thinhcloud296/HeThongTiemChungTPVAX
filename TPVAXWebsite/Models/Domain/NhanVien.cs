using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        [StringLength(8)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(12)]
        public string CCCD { get; set; }

        [Required]
        public DateTime NgayVaoLam { get; set; }

        [StringLength(10)]
        public string SoDT { get; set; }

        [StringLength(500)]
        public string DiaChi { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public int? ChucVu { get; set; }

        public bool TrangThai { get; set; } = false;

        [StringLength(8)]
        public string MaTK { get; set; }

        // Navigation properties
        [ForeignKey("MaTK")]
        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<PhieuNhapVaccine> PhieuNhapVaccines { get; set; }
    }
}
