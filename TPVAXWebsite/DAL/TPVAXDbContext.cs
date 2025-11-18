using System.Data.Entity;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.DAL
{
    public class TPVAXDbContext : DbContext
    {
        public TPVAXDbContext() : base("name=TPVAXConnection")
        {
            // Tắt lazy loading để tránh vấn đề với JSON serialization
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        // DbSets cho các entities
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<HoSoTiemChung> HoSoTiemChungs { get; set; }
        public DbSet<LienKetHoSo> LienKetHoSos { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<LoaiVaccine> LoaiVaccines { get; set; }
        public DbSet<LoaiBenh> LoaiBenhs { get; set; }
        public DbSet<VaccinePhongBenh> VaccinePhongBenhs { get; set; }
        public DbSet<GoiVaccine> GoiVaccines { get; set; }
        public DbSet<ChiTietGoiVaccine> ChiTietGoiVaccines { get; set; }
        public DbSet<LichTiem> LichTiems { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<PhieuNhapVaccine> PhieuNhapVaccines { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình composite key cho VaccinePhongBenh
            modelBuilder.Entity<VaccinePhongBenh>()
                .HasKey(vpb => new { vpb.MaVC, vpb.MaLoaiBenh });

            // Cấu hình relationships
            modelBuilder.Entity<KhachHang>()
                .HasOptional(k => k.TaiKhoan)
                .WithMany(t => t.KhachHangs)
                .HasForeignKey(k => k.MaTK);

            modelBuilder.Entity<NhanVien>()
                .HasOptional(n => n.TaiKhoan)
                .WithMany(t => t.NhanViens)
                .HasForeignKey(n => n.MaTK);

            modelBuilder.Entity<LienKetHoSo>()
                .HasRequired(l => l.KhachHang)
                .WithMany(k => k.LienKetHoSos)
                .HasForeignKey(l => l.MaKH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LienKetHoSo>()
                .HasRequired(l => l.HoSoTiemChung)
                .WithMany(h => h.LienKetHoSos)
                .HasForeignKey(l => l.MaHSTC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LichTiem>()
                .HasRequired(l => l.HoSoTiemChung)
                .WithMany(h => h.LichTiems)
                .HasForeignKey(l => l.MaHSTC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LichTiem>()
                .HasOptional(l => l.Vaccine)
                .WithMany(v => v.LichTiems)
                .HasForeignKey(l => l.MaVC);

            modelBuilder.Entity<GioHang>()
                .HasRequired(g => g.KhachHang)
                .WithMany(k => k.GioHangs)
                .HasForeignKey(g => g.MaKH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasOptional(h => h.KhachHang)
                .WithMany(k => k.HoaDons)
                .HasForeignKey(h => h.MaKH);

            modelBuilder.Entity<HoaDon>()
                .HasOptional(h => h.NhanVien)
                .WithMany(n => n.HoaDons)
                .HasForeignKey(h => h.MaNV);

            modelBuilder.Entity<HoaDon>()
                .HasOptional(h => h.KhuyenMai)
                .WithMany(k => k.HoaDons)
                .HasForeignKey(h => h.MaKM);

            modelBuilder.Entity<ChiTietHoaDon>()
                .HasRequired(c => c.HoaDon)
                .WithMany(h => h.ChiTietHoaDons)
                .HasForeignKey(c => c.MaHD)
                .WillCascadeOnDelete(false);

            // Tắt cascade delete cho các FK khác để tránh multiple cascade paths
            modelBuilder.Entity<Vaccine>()
                .HasOptional(v => v.LoaiVaccine)
                .WithMany(l => l.Vaccines)
                .HasForeignKey(v => v.MaLoai)
                .WillCascadeOnDelete(false);
        }
    }
}
