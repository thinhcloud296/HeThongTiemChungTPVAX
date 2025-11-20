using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class GoiVaccineViewModel
    {
        public string MaGoi { get; set; }
        public string TenGoi { get; set; }
        public string MoTa { get; set; }
        public string DoiTuongApDung { get; set; }
        public decimal GiaGoi { get; set; }
        public string TrangThai { get; set; }
        public decimal TongGiaTriVaccine { get; set; }
        public decimal TietKiem { get; set; }
        public string HinhAnh { get; set; }
        public List<ChiTietGoiVaccineViewModel> ChiTietGoiVaccine { get; set; }
    }

    public class ChiTietGoiVaccineViewModel
    {
        public string MaCTGoi { get; set; }
        public string MaVC { get; set; }
        public string TenVC { get; set; }
        public string TenLoaiVC { get; set; }
        public int SoMui { get; set; }
        public string GhiChu { get; set; }
        public string MaGoi { get; set; }
        public decimal? GiaVC { get; set; }
    }
}
