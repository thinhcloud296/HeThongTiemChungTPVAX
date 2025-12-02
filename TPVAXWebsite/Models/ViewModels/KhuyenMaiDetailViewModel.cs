using System;
using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho trang chi tiết khuyến mãi
    /// </summary>
    public class KhuyenMaiDetailViewModel
    {
        public string MaKM { get; set; }
        public string TenKM { get; set; }
        public string MoTa { get; set; }
        public string LoaiKM { get; set; }
        public string KieuGiam { get; set; }
        public decimal GiaTriGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool TrangThai { get; set; }
        public string TrangThaiHienThi { get; set; }
        public int SoNgayConLai { get; set; }
        public string HinhAnh { get; set; }

        // Danh sách sản phẩm áp dụng
        public List<SanPhamApDung> SanPhamApDungs { get; set; } = new List<SanPhamApDung>();

        // Formatted properties
        public string GiaTriGiamFormatted
        {
            get
            {
                if (KieuGiam == "Phần trăm" || KieuGiam == "%")
                    return $"{GiaTriGiam}%";
                else
                    return $"{GiaTriGiam:N0}₫";
            }
        }

        public string NgayBatDauFormatted => NgayBatDau.ToString("dd/MM/yyyy");
        public string NgayKetThucFormatted => NgayKetThuc.ToString("dd/MM/yyyy");

        public bool IsActive => TrangThaiHienThi == "Đang diễn ra";
        public bool IsUpcoming => TrangThaiHienThi == "Sắp diễn ra";
        public bool IsExpired => TrangThaiHienThi == "Đã hết hạn";
    }

    /// <summary>
    /// Thông tin sản phẩm áp dụng khuyến mãi
    /// </summary>
    public class SanPhamApDung
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; } // VACCINE hoặc GOIVACCINE
        public decimal GiaGoc { get; set; }
        public decimal GiaSauGiam { get; set; }
        public string HinhAnh { get; set; }
        public string MoTa { get; set; }

        public string GiaGocFormatted => $"{GiaGoc:N0}₫";
        public string GiaSauGiamFormatted => $"{GiaSauGiam:N0}₫";
        public decimal TietKiem => GiaGoc - GiaSauGiam;
        public string TietKiemFormatted => $"{TietKiem:N0}₫";
    }
}
