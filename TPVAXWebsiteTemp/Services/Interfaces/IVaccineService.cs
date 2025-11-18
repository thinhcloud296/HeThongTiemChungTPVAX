using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Services.Interfaces
{
    /// <summary>
    /// Service xử lý logic nghiệp vụ cho vaccine và gói vaccine
    /// </summary>
    public interface IVaccineService
    {
        // Vaccine đơn lẻ
        List<Vaccine> GetAllVaccines();
        List<Vaccine> SearchVaccines(string keyword, string maLoai = null);
        VaccineDetailViewModel GetVaccineDetail(string maVC);
        
        // Gói vaccine
        List<GoiVaccine> GetAllGoiVaccines();
        GoiVaccine GetGoiVaccineDetail(string maGoi);
        List<ChiTietGoiVaccine> GetChiTietGoiVaccine(string maGoi);
        
        // Loại vaccine và bệnh
        List<LoaiVaccine> GetAllLoaiVaccines();
        List<LoaiBenh> GetBenhByVaccine(string maVC);
        
        // Khuyến mãi
        List<KhuyenMai> GetActivePromotions();
        decimal CalculateDiscountedPrice(string maSanPham, string loaiSanPham);
    }
}
