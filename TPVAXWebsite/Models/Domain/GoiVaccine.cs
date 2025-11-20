using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("GoiVaccine")]
    public class GoiVaccine
    {
        [Key]
        [StringLength(10)]
        public string MaGoi { get; set; }

        [Required]
        [StringLength(255)]
        public string TenGoi { get; set; }

        public string MoTa { get; set; }

        [StringLength(255)]
        public string DoiTuongApDung { get; set; }

        [Required]
        public decimal GiaGoi { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        // Navigation properties
        public virtual ICollection<ChiTietGoiVaccine> ChiTietGoiVaccine { get; set; }
    }
}
