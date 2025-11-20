using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("Vaccine")]
    public class Vaccine
    {
        [Key]
        [StringLength(10)]
        public string MaVC { get; set; }

        [Required]
        [StringLength(255)]
        public string TenVC { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal GiaBan { get; set; }

        [Required]
        public int SoLuongTon { get; set; } = 0;

        public int? SoMuiToiDa { get; set; }

        public int? SoThangCho { get; set; }

        [StringLength(10)]
        public string MaLoai { get; set; }

        public string MoTa { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        // Navigation properties
        [ForeignKey("MaLoai")]
        public virtual LoaiVaccine LoaiVaccine { get; set; }

        public virtual ICollection<VaccinePhongBenh> VaccinePhongBenh { get; set; }
        public virtual ICollection<ChiTietGoiVaccine> ChiTietGoiVaccine { get; set; }
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
        public virtual ICollection<LichTiem> LichTiem { get; set; }
    }
}
