using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class VaccineDetailViewModel
    {
        public string MaVC { get; set; }
        public string TenVC { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public string TenLoai { get; set; }
        public string MoTaLoai { get; set; }
        public List<string> CacBenhPhongChong { get; set; }
        public string NuocSanXuat { get; set; }
        public List<GoiVaccineInfo> CacGoiVaccineChung { get; set; }
    }

    public class GoiVaccineInfo
    {
        public string MaGoi { get; set; }
        public string TenGoi { get; set; }
        public decimal GiaGoi { get; set; }
        public string DoiTuongApDung { get; set; }
    }
}
