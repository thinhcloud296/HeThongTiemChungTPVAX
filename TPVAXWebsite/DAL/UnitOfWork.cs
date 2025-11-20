using System;
using System.Data.Entity;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.DAL
{
    /// <summary>
    /// Unit of Work Implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TPVAXDbContext _context;
        private DbContextTransaction _transaction;

        // Private repository fields
        private IRepository<TaiKhoan> _taiKhoans;
        private IRepository<KhachHang> _khachHangs;
        private IRepository<NhanVien> _nhanViens;
        private IRepository<HoSoTiemChung> _hoSoTiemChungs;
        private IRepository<LienKetHoSo> _lienKetHoSos;
        private IRepository<LoaiVaccine> _loaiVaccines;
        private IRepository<LoaiBenh> _loaiBenhs;
        private IRepository<Vaccine> _vaccines;
        private IRepository<VaccinePhongBenh> _vaccinePhongBenhs;
        private IRepository<GoiVaccine> _goiVaccines;
        private IRepository<ChiTietGoiVaccine> _chiTietGoiVaccines;
        private IRepository<GioHang> _gioHangs;
        private IRepository<KhuyenMai> _khuyenMais;
        private IRepository<ChiTietKhuyenMai> _chiTietKhuyenMais;
        private IRepository<HoaDon> _hoaDons;
        private IRepository<ChiTietHoaDon> _chiTietHoaDons;
        private IRepository<LichTiem> _lichTiems;
        private IRepository<NhaCungCap> _nhaCungCaps;
        private IRepository<PhieuNhapVaccine> _phieuNhapVaccines;
        private IRepository<ChiTietPhieuNhap> _chiTietPhieuNhaps;

        public UnitOfWork()
        {
            _context = new TPVAXDbContext();
        }

        public UnitOfWork(TPVAXDbContext context)
        {
            _context = context;
        }

        // Repository properties - Lazy initialization
        public IRepository<TaiKhoan> TaiKhoans => _taiKhoans ?? (_taiKhoans = new Repository<TaiKhoan>(_context));
        public IRepository<KhachHang> KhachHangs => _khachHangs ?? (_khachHangs = new Repository<KhachHang>(_context));
        public IRepository<NhanVien> NhanViens => _nhanViens ?? (_nhanViens = new Repository<NhanVien>(_context));
        public IRepository<HoSoTiemChung> HoSoTiemChungs => _hoSoTiemChungs ?? (_hoSoTiemChungs = new Repository<HoSoTiemChung>(_context));
        public IRepository<LienKetHoSo> LienKetHoSos => _lienKetHoSos ?? (_lienKetHoSos = new Repository<LienKetHoSo>(_context));
        public IRepository<LoaiVaccine> LoaiVaccines => _loaiVaccines ?? (_loaiVaccines = new Repository<LoaiVaccine>(_context));
        public IRepository<LoaiBenh> LoaiBenhs => _loaiBenhs ?? (_loaiBenhs = new Repository<LoaiBenh>(_context));
        public IRepository<Vaccine> Vaccines => _vaccines ?? (_vaccines = new Repository<Vaccine>(_context));
        public IRepository<VaccinePhongBenh> VaccinePhongBenhs => _vaccinePhongBenhs ?? (_vaccinePhongBenhs = new Repository<VaccinePhongBenh>(_context));
        public IRepository<GoiVaccine> GoiVaccines => _goiVaccines ?? (_goiVaccines = new Repository<GoiVaccine>(_context));
        public IRepository<ChiTietGoiVaccine> ChiTietGoiVaccines => _chiTietGoiVaccines ?? (_chiTietGoiVaccines = new Repository<ChiTietGoiVaccine>(_context));
        public IRepository<GioHang> GioHangs => _gioHangs ?? (_gioHangs = new Repository<GioHang>(_context));
        public IRepository<KhuyenMai> KhuyenMais => _khuyenMais ?? (_khuyenMais = new Repository<KhuyenMai>(_context));
        public IRepository<ChiTietKhuyenMai> ChiTietKhuyenMais => _chiTietKhuyenMais ?? (_chiTietKhuyenMais = new Repository<ChiTietKhuyenMai>(_context));
        public IRepository<HoaDon> HoaDons => _hoaDons ?? (_hoaDons = new Repository<HoaDon>(_context));
        public IRepository<ChiTietHoaDon> ChiTietHoaDons => _chiTietHoaDons ?? (_chiTietHoaDons = new Repository<ChiTietHoaDon>(_context));
        public IRepository<LichTiem> LichTiems => _lichTiems ?? (_lichTiems = new Repository<LichTiem>(_context));
        public IRepository<NhaCungCap> NhaCungCaps => _nhaCungCaps ?? (_nhaCungCaps = new Repository<NhaCungCap>(_context));
        public IRepository<PhieuNhapVaccine> PhieuNhapVaccines => _phieuNhapVaccines ?? (_phieuNhapVaccines = new Repository<PhieuNhapVaccine>(_context));
        public IRepository<ChiTietPhieuNhap> ChiTietPhieuNhaps => _chiTietPhieuNhaps ?? (_chiTietPhieuNhaps = new Repository<ChiTietPhieuNhap>(_context));

        // Methods
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction?.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        // Dispose pattern
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
