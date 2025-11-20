using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPVAXWebsite.Models.Domain
{
    [Table("NhaCungCap")]
    public class NhaCungCap
    {
        [Key]
        [StringLength(10)]
        public string MaNCC { get; set; }

        [Required]
        [StringLength(255)]
        public string TenNCC { get; set; }

        [StringLength(500)]
        public string DiaChi { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(10)]
        public string SoDT { get; set; }

        [StringLength(100)]
        public string TenNganHang { get; set; }

        [StringLength(30)]
        public string SoTK { get; set; }

        // Navigation properties
        public virtual ICollection<PhieuNhapVaccine> PhieuNhapVaccine { get; set; }
    }
}
