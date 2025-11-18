using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("Vaccine")]
    public class Vaccine
    {
        [Key]
        [StringLength(8)]
        public string MaVC { get; set; }

        [Required]
        [StringLength(255)]
        public string TenVC { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,0)")]
        public decimal GiaBan { get; set; }

        [Required]
        public int SoLuongTon { get; set; } = 0;

        [StringLength(8)]
        public string MaLoai { get; set; }

    [StringLength(255)]
    public string HinhAnh { get; set; }

        // Navigation properties
        [ForeignKey("MaLoai")]
        public virtual LoaiVaccine LoaiVaccine { get; set; }

        public virtual ICollection<VaccinePhongBenh> VaccinePhongBenhs { get; set; }
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public virtual ICollection<ChiTietGoiVaccine> ChiTietGoiVaccines { get; set; }
        public virtual ICollection<LichTiem> LichTiems { get; set; }
    }
}
