using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Services.Interfaces
{
    /// <summary>
    /// Service xử lý logic nghiệp vụ cho hóa đơn và thanh toán
    /// </summary>
    public interface IHoaDonService
    {
        // Tạo hóa đơn
        HoaDon CreateHoaDonFromCart(string maKH, string maKM = null);
        HoaDon CreateHoaDonFromGoiVaccine(string maKH, string maGoi, string maKM = null);
        
        // Thanh toán
        bool ProcessPayment(string maHD, string paymentMethod);
        bool ConfirmPayment(string maHD);
        
        // Quản lý hóa đơn
        List<HoaDonViewModel> GetHoaDonsByKhachHang(string maKH);
        HoaDonViewModel GetHoaDonDetail(string maHD);
        bool CancelHoaDon(string maHD, out string errorMessage);
        
        // Áp dụng khuyến mãi
        decimal ApplyPromotion(string maHD, string maKM);
    }
}
