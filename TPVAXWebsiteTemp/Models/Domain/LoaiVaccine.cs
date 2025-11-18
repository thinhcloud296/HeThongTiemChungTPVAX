using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("LoaiVaccine")]
    public class LoaiVaccine
    {
        [Key]
        [StringLength(8)]
        public string MaLoai { get; set; }

        [Required]
        [StringLength(255)]
        public string TenLoai { get; set; }

        public string MoTa { get; set; }

        // Navigation properties
        public virtual ICollection<Vaccine> Vaccines { get; set; }
    }
}
