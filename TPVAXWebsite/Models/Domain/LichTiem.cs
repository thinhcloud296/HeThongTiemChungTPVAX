using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("LichTiem")]
    public class LichTiem
    {
        [Key]
        [StringLength(10)]
        public string MaLT { get; set; }

        [Required]
        public DateTime NgayHenTiem { get; set; }

        public DateTime? NgayTiemThucTe { get; set; }

        public int? SoMui { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; } = "Chưa tiêm";

        public string GhiChu { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHSTC { get; set; }

        [StringLength(10)]
        public string MaVC { get; set; }

        [StringLength(10)]
        public string MaNV { get; set; }

        // Navigation properties
        [ForeignKey("MaHSTC")]
        public virtual HoSoTiemChung HoSoTiemChung { get; set; }

        [ForeignKey("MaVC")]
        public virtual Vaccine Vaccine { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }
    }
}
