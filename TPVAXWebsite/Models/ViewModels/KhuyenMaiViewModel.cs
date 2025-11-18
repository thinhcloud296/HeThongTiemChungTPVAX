using System;
using System.Collections.Generic;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho danh sách khuyến mãi
    /// </summary>
    public class KhuyenMaiViewModel
    {
        public string MaKhuyenMai { get; set; }
        public string TenKhuyenMai { get; set; }
        public string MoTa { get; set; }
        public decimal GiamGia { get; set; }
        public string LoaiGiamGia { get; set; } // "Percent" or "Amount"
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string HinhAnh { get; set; }
    }

    /// <summary>
    /// ViewModel cho chi tiết khuyến mãi
    /// </summary>
    public class KhuyenMaiDetailViewModel
    {
        public KhuyenMai KhuyenMai { get; set; }
        public List<ChiTietKhuyenMai> ChiTietKhuyenMai { get; set; }
        public List<Vaccine> VaccineApDung { get; set; }
        public bool IsActive => DateTime.Now >= KhuyenMai.NgayBatDau && DateTime.Now <= KhuyenMai.NgayKetThuc;

        public KhuyenMaiDetailViewModel()
        {
            ChiTietKhuyenMai = new List<ChiTietKhuyenMai>();
            VaccineApDung = new List<Vaccine>();
        }
    }
}
