using System.Collections.Generic;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho danh sách gói vắc xin
    /// </summary>
    public class GoiVaccineViewModel
    {
        public string MaGoi { get; set; }
        public string TenGoi { get; set; }
        public string MoTa { get; set; }
        public decimal GiaGoi { get; set; }
        public string DoiTuongApDung { get; set; }
        public int SoLuongVaccine { get; set; }
        public List<Vaccine> DanhSachVaccine { get; set; }

        public GoiVaccineViewModel()
        {
            DanhSachVaccine = new List<Vaccine>();
        }
    }

    /// <summary>
    /// ViewModel cho chi tiết gói vắc xin
    /// </summary>
    public class GoiVaccineDetailViewModel
    {
        public GoiVaccine GoiVaccine { get; set; }
        public List<ChiTietGoiVaccine> ChiTietGoiVaccine { get; set; }
        public List<Vaccine> DanhSachVaccine { get; set; }

        public GoiVaccineDetailViewModel()
        {
            ChiTietGoiVaccine = new List<ChiTietGoiVaccine>();
            DanhSachVaccine = new List<Vaccine>();
        }
    }
}
