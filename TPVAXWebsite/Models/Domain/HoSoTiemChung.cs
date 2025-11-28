using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("HoSoTiemChung")]
    public class HoSoTiemChung
    {
        [Key]
        [StringLength(10)]
        public string MaHSTC { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(10)]
        public string GioiTinh { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        [StringLength(12)]
        public string CCCD { get; set; }

        public string GhiChu { get; set; }

        public bool TrangThai { get; set; } = true;

        // Navigation properties
        public virtual ICollection<LienKetHoSo> LienKetHoSo { get; set; }
        public virtual ICollection<LichTiem> LichTiem { get; set; }
    }
}
