using System;

namespace TPVAXWebsite.Models.ViewModels
{
    public class BaiVietViewModel
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string TomTat { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public DateTime NgayDang { get; set; }
        public string DanhMuc { get; set; }
        public string Tag { get; set; }
    }
}
