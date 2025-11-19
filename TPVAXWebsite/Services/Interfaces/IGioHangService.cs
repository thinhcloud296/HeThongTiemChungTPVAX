using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace TPVAXWebsite.Services.Interfaces
{
    /// <summary>
    /// Service xử lý logic nghiệp vụ cho giỏ hàng
    /// </summary>
    public interface IGioHangService
    {
        // Quản lý giỏ hàng
        List<GioHangViewModel> GetGioHangByKhachHang(string maKH);
        bool AddToCart(string maKH, string maSanPham, string loaiSanPham, int soLuong);
        bool UpdateQuantity(string maGH, int soLuong);
        bool RemoveFromCart(string maGH);
        bool ClearCart(string maKH);
        
        // Tính toán
        decimal GetTotalAmount(string maKH);
        int GetCartItemCount(string maKH);
    }
}
