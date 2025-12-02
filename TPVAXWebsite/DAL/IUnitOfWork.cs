using System;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.DAL
{
    /// <summary>
    /// Unit of Work Interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // Repositories
        IRepository<TaiKhoan> TaiKhoans { get; }
        IRepository<KhachHang> KhachHangs { get; }
        IRepository<NhanVien> NhanViens { get; }
        IRepository<HoSoTiemChung> HoSoTiemChungs { get; }
        IRepository<LienKetHoSo> LienKetHoSos { get; }
        IRepository<LoaiVaccine> LoaiVaccines { get; }
        IRepository<LoaiBenh> LoaiBenhs { get; }
        IRepository<Vaccine> Vaccines { get; }
        IRepository<VaccinePhongBenh> VaccinePhongBenhs { get; }
        IRepository<GoiVaccine> GoiVaccines { get; }
        IRepository<ChiTietGoiVaccine> ChiTietGoiVaccines { get; }
        IRepository<GioHang> GioHangs { get; }
        IRepository<KhuyenMai> KhuyenMais { get; }
        IRepository<ChiTietKhuyenMai> ChiTietKhuyenMais { get; }
        IRepository<HoaDon> HoaDons { get; }
        IRepository<ChiTietHoaDon> ChiTietHoaDons { get; }
        IRepository<LichTiem> LichTiems { get; }
        IRepository<NhaCungCap> NhaCungCaps { get; }
        IRepository<PhieuNhapVaccine> PhieuNhapVaccines { get; }
        IRepository<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; }
        IRepository<BaiViet> BaiViets { get; }

        // Methods
        int SaveChanges();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
