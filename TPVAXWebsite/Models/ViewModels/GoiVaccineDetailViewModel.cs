using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class GoiVaccineDetailViewModel
    {
        public GoiVaccineInfo GoiVaccine { get; set; }
        public List<VaccineInPackage> ChiTietVaccines { get; set; }
    }

    public class GoiVaccineInfo
    {
        public string MaGoi { get; set; }
        public string TenGoi { get; set; }
        public string MoTa { get; set; }
        public string DoiTuongApDung { get; set; }
        public decimal GiaBan { get; set; }
        public decimal? GiaGoc { get; set; }
        public string TrangThai { get; set; }
        public string HinhAnh { get; set; }
    }

    public class VaccineInPackage
    {
        public string MaVC { get; set; }
        public string TenVC { get; set; }
        public int SoLieu { get; set; }
        public decimal DonGia { get; set; }
        public string HinhAnh { get; set; }
    }
}
