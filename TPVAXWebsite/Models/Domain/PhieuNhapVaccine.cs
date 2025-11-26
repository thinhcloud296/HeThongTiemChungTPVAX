using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("PhieuNhapVaccine")]
    public class PhieuNhapVaccine
    {
        [Key]
        [StringLength(10)]
        public string MaPN { get; set; }

        [Required]
        public DateTime NgayLap { get; set; } = DateTime.Now;

        [StringLength(10)]
        public string MaNV { get; set; }

        [StringLength(10)]
        public string MaNCC { get; set; }

        public bool TrangThai { get; set; } = false;

        // Navigation properties
        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

        [ForeignKey("MaNCC")]
        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
    }
}
