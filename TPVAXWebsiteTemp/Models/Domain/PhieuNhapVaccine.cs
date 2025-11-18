using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("PhieuNhapVaccine")]
    public class PhieuNhapVaccine
    {
        [Key]
        [StringLength(8)]
        public string MaPN { get; set; }

        [Required]
        public DateTime NgayLap { get; set; } = DateTime.Now;

        [StringLength(8)]
        public string MaNV { get; set; }

        [StringLength(8)]
        public string MaNCC { get; set; }

        // Navigation properties
        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

        [ForeignKey("MaNCC")]
        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }
}
