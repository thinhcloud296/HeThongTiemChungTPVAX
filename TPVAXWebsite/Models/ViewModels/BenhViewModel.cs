using System;
using System.Collections.Generic;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho danh sách bệnh truyền nhiễm
    /// </summary>
    public class BenhViewModel
    {
        public string MaBenh { get; set; }
        public string TenBenh { get; set; }
        public string MoTa { get; set; }
        public string TrieuChung { get; set; }
        public string CachPhongNgua { get; set; }
        public string HinhAnh { get; set; }
        public string LoaiBenh { get; set; } // Virus, Vi khuẩn, etc.
        public List<Vaccine> VaccinePhongNgua { get; set; }

        public BenhViewModel()
        {
            VaccinePhongNgua = new List<Vaccine>();
        }
    }

    /// <summary>
    /// ViewModel cho chi tiết bệnh truyền nhiễm
    /// </summary>
    public class BenhTruyenNhiemDetailViewModel
    {
        public LoaiBenh LoaiBenh { get; set; }
        public List<Vaccine> VaccinePhongNgua { get; set; }
        public List<VaccinePhongBenh> DanhSachVaccinePhongBenh { get; set; }
        public string ThongTinChiTiet { get; set; }

        public BenhTruyenNhiemDetailViewModel()
        {
            VaccinePhongNgua = new List<Vaccine>();
            DanhSachVaccinePhongBenh = new List<VaccinePhongBenh>();
        }
    }
}
