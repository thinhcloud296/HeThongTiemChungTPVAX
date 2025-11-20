using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("VaccinePhongBenh")]
    public class VaccinePhongBenh
    {
        [Key, Column(Order = 0)]
        [StringLength(10)]
        public string MaVC { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(10)]
        public string MaLoaiBenh { get; set; }

        public string GhiChu { get; set; }

        // Navigation properties
        [ForeignKey("MaVC")]
        public virtual Vaccine Vaccine { get; set; }

        [ForeignKey("MaLoaiBenh")]
        public virtual LoaiBenh LoaiBenh { get; set; }
    }
}
