using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("LoaiBenh")]
    public class LoaiBenh
    {
        [Key]
        [StringLength(8)]
        public string MaLoaiBenh { get; set; }

        [Required]
        [StringLength(255)]
        public string TenBenh { get; set; }

        public string MoTa { get; set; }

        [StringLength(255)]
        public string NhomDoiTuong { get; set; }

        // Navigation properties
        public virtual ICollection<VaccinePhongBenh> VaccinePhongBenhs { get; set; }
    }
}
