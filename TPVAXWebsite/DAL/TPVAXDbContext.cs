using System.Data.Entity;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.DAL
{
    public class TPVAXDbContext : DbContext
    {
        public TPVAXDbContext() : base("name=TPVAXConnection")
        {
            // Disable lazy loading for better performance
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        // DbSet cho từng bảng
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<HoSoTiemChung> HoSoTiemChungs { get; set; }
        public virtual DbSet<LienKetHoSo> LienKetHoSos { get; set; }
        public virtual DbSet<LoaiVaccine> LoaiVaccines { get; set; }
        public virtual DbSet<LoaiBenh> LoaiBenhs { get; set; }
        public virtual DbSet<Vaccine> Vaccines { get; set; }
        public virtual DbSet<VaccinePhongBenh> VaccinePhongBenhs { get; set; }
        public virtual DbSet<GoiVaccine> GoiVaccines { get; set; }
        public virtual DbSet<ChiTietGoiVaccine> ChiTietGoiVaccines { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<LichTiem> LichTiems { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<PhieuNhapVaccine> PhieuNhapVaccines { get; set; }
        public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình Composite Key cho VaccinePhongBenh
            modelBuilder.Entity<VaccinePhongBenh>()
                .HasKey(v => new { v.MaVC, v.MaLoaiBenh });

            // Cấu hình relationships
            ConfigureRelationships(modelBuilder);
        }

        private void ConfigureRelationships(DbModelBuilder modelBuilder)
        {
            // TaiKhoan relationships
            modelBuilder.Entity<KhachHang>()
                .HasOptional(k => k.TaiKhoan)
                .WithMany()
                .HasForeignKey(k => k.MaTK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasOptional(n => n.TaiKhoan)
                .WithMany()
                .HasForeignKey(n => n.MaTK)
                .WillCascadeOnDelete(false);

            // Vaccine relationships
            modelBuilder.Entity<Vaccine>()
                .HasOptional(v => v.LoaiVaccine)
                .WithMany(l => l.Vaccines)
                .HasForeignKey(v => v.MaLoai)
                .WillCascadeOnDelete(false);

            // VaccinePhongBenh relationships
            modelBuilder.Entity<VaccinePhongBenh>()
                .HasRequired(v => v.Vaccine)
                .WithMany(vc => vc.VaccinePhongBenh)
                .HasForeignKey(v => v.MaVC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VaccinePhongBenh>()
                .HasRequired(v => v.LoaiBenh)
                .WithMany(l => l.VaccinePhongBenh)
                .HasForeignKey(v => v.MaLoaiBenh)
                .WillCascadeOnDelete(false);

            // ChiTietGoiVaccine relationships
            modelBuilder.Entity<ChiTietGoiVaccine>()
                .HasRequired(c => c.GoiVaccine)
                .WithMany(g => g.ChiTietGoiVaccine)
                .HasForeignKey(c => c.MaGoi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChiTietGoiVaccine>()
                .HasRequired(c => c.Vaccine)
                .WithMany(v => v.ChiTietGoiVaccine)
                .HasForeignKey(c => c.MaVC)
                .WillCascadeOnDelete(false);

            // LienKetHoSo relationships
            modelBuilder.Entity<LienKetHoSo>()
                .HasRequired(l => l.KhachHang)
                .WithMany(k => k.LienKetHoSo)
                .HasForeignKey(l => l.MaKH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LienKetHoSo>()
                .HasRequired(l => l.HoSoTiemChung)
                .WithMany(h => h.LienKetHoSo)
                .HasForeignKey(l => l.MaHSTC)
                .WillCascadeOnDelete(false);

            // GioHang relationships
            modelBuilder.Entity<GioHang>()
                .HasRequired(g => g.KhachHang)
                .WithMany(k => k.GioHang)
                .HasForeignKey(g => g.MaKH)
                .WillCascadeOnDelete(false);

            // HoaDon relationships
            modelBuilder.Entity<HoaDon>()
                .HasOptional(h => h.KhachHang)
                .WithMany(k => k.HoaDon)
                .HasForeignKey(h => h.MaKH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasOptional(h => h.NhanVien)
                .WithMany(n => n.HoaDon)
                .HasForeignKey(h => h.MaNV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasOptional(h => h.KhuyenMai)
                .WithMany(k => k.HoaDon)
                .HasForeignKey(h => h.MaKM)
                .WillCascadeOnDelete(false);

            // ChiTietHoaDon relationships
            modelBuilder.Entity<ChiTietHoaDon>()
                .HasRequired(c => c.HoaDon)
                .WithMany(h => h.ChiTietHoaDon)
                .HasForeignKey(c => c.MaHD)
                .WillCascadeOnDelete(false);

            // LichTiem relationships
            modelBuilder.Entity<LichTiem>()
                .HasRequired(l => l.HoSoTiemChung)
                .WithMany(h => h.LichTiem)
                .HasForeignKey(l => l.MaHSTC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LichTiem>()
                .HasOptional(l => l.Vaccine)
                .WithMany(v => v.LichTiem)
                .HasForeignKey(l => l.MaVC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LichTiem>()
                .HasOptional(l => l.NhanVien)
                .WithMany(n => n.LichTiem)
                .HasForeignKey(l => l.MaNV)
                .WillCascadeOnDelete(false);

            // PhieuNhapVaccine relationships
            modelBuilder.Entity<PhieuNhapVaccine>()
                .HasOptional(p => p.NhanVien)
                .WithMany(n => n.PhieuNhapVaccine)
                .HasForeignKey(p => p.MaNV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuNhapVaccine>()
                .HasOptional(p => p.NhaCungCap)
                .WithMany(n => n.PhieuNhapVaccine)
                .HasForeignKey(p => p.MaNCC)
                .WillCascadeOnDelete(false);

            // ChiTietPhieuNhap relationships
            modelBuilder.Entity<ChiTietPhieuNhap>()
                .HasRequired(c => c.PhieuNhapVaccine)
                .WithMany(p => p.ChiTietPhieuNhap)
                .HasForeignKey(c => c.MaPN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChiTietPhieuNhap>()
                .HasRequired(c => c.Vaccine)
                .WithMany(v => v.ChiTietPhieuNhap)
                .HasForeignKey(c => c.MaVC)
                .WillCascadeOnDelete(false);

            // ChiTietKhuyenMai relationships
            modelBuilder.Entity<ChiTietKhuyenMai>()
                .HasRequired(c => c.KhuyenMai)
                .WithMany(k => k.ChiTietKhuyenMai)
                .HasForeignKey(c => c.MaKM)
                .WillCascadeOnDelete(false);
        }
    }
}
