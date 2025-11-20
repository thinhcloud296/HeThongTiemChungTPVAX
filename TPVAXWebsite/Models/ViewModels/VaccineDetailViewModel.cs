using System.Collections.Generic;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Models.ViewModels
{
    public class VaccineDetailViewModel
    {
        public VaccineInfo Vaccine { get; set; }
        public List<string> CacBenhPhong { get; set; }
        public List<Domain.Vaccine> VaccinesLienQuan { get; set; }

        public class VaccineInfo
        {
            public string MaVC { get; set; }
            public string TenVaccine { get; set; }
            public decimal GiaBan { get; set; }
            public int SoLuongTon { get; set; }
            public int? SoMuiToiDa { get; set; }
            public int? SoThangCho { get; set; }
            public string MoTa { get; set; }
            public string HinhAnh { get; set; }
            public string TenLoaiVaccine { get; set; }
        }
    }
}
