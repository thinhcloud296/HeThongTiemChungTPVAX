using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("ChiTietGoiVaccine")]
    public class ChiTietGoiVaccine
    {
        [Key]
        [StringLength(10)]
        public string MaCTGoi { get; set; }

        public int? SoMui { get; set; }

        public string GhiChu { get; set; }

        [Required]
        [StringLength(10)]
        public string MaGoi { get; set; }

        [Required]
        [StringLength(10)]
        public string MaVC { get; set; }

        // Navigation properties
        [ForeignKey("MaGoi")]
        public virtual GoiVaccine GoiVaccine { get; set; }

        [ForeignKey("MaVC")]
        public virtual Vaccine Vaccine { get; set; }
    }
}
