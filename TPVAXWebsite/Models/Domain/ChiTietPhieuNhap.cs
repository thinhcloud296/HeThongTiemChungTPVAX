using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("ChiTietPhieuNhap")]
    public class ChiTietPhieuNhap
    {
        [Key]
        [StringLength(10)]
        public string MaCTPN { get; set; }

        [StringLength(100)]
        public string NuocSanXuat { get; set; }

        [Required]
        public int SoLuong { get; set; }

        [Required]
        public int SoLuongTonKho { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal GiaNhap { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HanSuDung { get; set; }

        [Required]
        [StringLength(10)]
        public string MaPN { get; set; }

        [Required]
        [StringLength(10)]
        public string MaVC { get; set; }

        // Navigation properties
        [ForeignKey("MaPN")]
        public virtual PhieuNhapVaccine PhieuNhapVaccine { get; set; }

        [ForeignKey("MaVC")]
        public virtual Vaccine Vaccine { get; set; }
    }
}
