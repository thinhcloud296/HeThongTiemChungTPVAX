using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("LichTiem")]
    public class LichTiem
    {
        [Key]
        [StringLength(8)]
        public string MaLT { get; set; }

        [Required]
        public DateTime NgayHenTiem { get; set; }

        public DateTime? NgayTiemThucTe { get; set; }

        public int? SoMui { get; set; }

        public bool TrangThai { get; set; }

        public string GhiChu { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHSTC { get; set; }

        [StringLength(8)]
        public string MaVC { get; set; }

        // Navigation properties
        [ForeignKey("MaHSTC")]
        public virtual HoSoTiemChung HoSoTiemChung { get; set; }

        [ForeignKey("MaVC")]
        public virtual Vaccine Vaccine { get; set; }
    }
}
