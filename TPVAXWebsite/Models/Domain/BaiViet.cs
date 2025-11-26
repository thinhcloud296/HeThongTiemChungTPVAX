using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TPVAXWebsite.Models.Domain
{
    [Table("BaiViet")]
    public class BaiViet
    {
        [Key]
        public int MaBV { get; set; }
        public string TieuDe { get; set; }
        public string TomTat { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public string DanhMuc { get; set; }
        public string Tag { get; set; }
        public DateTime NgayDang { get; set; }
        public bool TrangThai { get; set; }
    }
}