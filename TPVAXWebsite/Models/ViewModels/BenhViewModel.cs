using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class BenhViewModel
    {
        public string MaLoaiBenh { get; set; }
        public string TenBenh { get; set; }
        public string MoTa { get; set; }
        public string NhomDoiTuong { get; set; }
        public List<VaccinePhongBenhViewModel> VaccinePhongBenh { get; set; }
    }

    public class VaccinePhongBenhViewModel
    {
        public string MaVC { get; set; }
        public string TenVC { get; set; }
        public decimal GiaBan { get; set; }
        public string HinhAnh { get; set; }
    }
}
