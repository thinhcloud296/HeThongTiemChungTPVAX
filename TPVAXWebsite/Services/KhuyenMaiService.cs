using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Services
{
    /// <summary>
    /// Service xử lý nghiệp vụ liên quan đến Khuyến Mãi
    /// </summary>
    public class KhuyenMaiService : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public KhuyenMaiService()
        {
            _unitOfWork = new UnitOfWork(new TPVAXDbContext());
        }

        public KhuyenMaiService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lấy chi tiết khuyến mãi đầy đủ với danh sách sản phẩm áp dụng
        /// </summary>
        public KhuyenMaiDetailViewModel GetFullDetailViewModel(string maKM)
        {
            var khuyenMai = GetById(maKM);
            if (khuyenMai == null)
                return null;

            var today = DateTime.Now.Date;
            string trangThaiHienThi;

            if (khuyenMai.NgayKetThuc < today)
                trangThaiHienThi = "Đã hết hạn";
            else if (khuyenMai.NgayBatDau > today)
                trangThaiHienThi = "Sắp diễn ra";
            else if (khuyenMai.TrangThai)
                trangThaiHienThi = "Đang diễn ra";
            else
                trangThaiHienThi = "Tạm ngưng";

            var viewModel = new KhuyenMaiDetailViewModel
            {
                MaKM = khuyenMai.MaKM,
                TenKM = khuyenMai.TenKM,
                MoTa = khuyenMai.MoTa,
                LoaiKM = khuyenMai.LoaiKM,
                KieuGiam = khuyenMai.KieuGiam,
                GiaTriGiam = khuyenMai.GiaTriGiam,
                NgayBatDau = khuyenMai.NgayBatDau,
                NgayKetThuc = khuyenMai.NgayKetThuc,
                TrangThai = khuyenMai.TrangThai,
                TrangThaiHienThi = trangThaiHienThi,
                SoNgayConLai = Math.Max(0, (khuyenMai.NgayKetThuc - today).Days),
                HinhAnh = khuyenMai.HinhAnh,
                SanPhamApDungs = new List<SanPhamApDung>()
            };

            // Lấy danh sách sản phẩm áp dụng từ ChiTietKhuyenMai
            if (khuyenMai.ChiTietKhuyenMai != null && khuyenMai.ChiTietKhuyenMai.Any())
            {
                foreach (var ct in khuyenMai.ChiTietKhuyenMai)
                {
                    SanPhamApDung sanPham = null;

                    if (ct.LoaiSanPham == "VACCINE")
                    {
                        var vaccine = _unitOfWork.Vaccines.Query()
                            .FirstOrDefault(v => v.MaVC == ct.MaSanPham);
                        if (vaccine != null)
                        {
                            decimal giaSauGiam = TinhGiaSauGiam(vaccine.GiaBan, khuyenMai.KieuGiam, khuyenMai.GiaTriGiam);
                            sanPham = new SanPhamApDung
                            {
                                MaSanPham = vaccine.MaVC,
                                TenSanPham = vaccine.TenVC,
                                LoaiSanPham = "VACCINE",
                                GiaGoc = vaccine.GiaBan,
                                GiaSauGiam = giaSauGiam,
                                HinhAnh = vaccine.HinhAnh,
                                MoTa = vaccine.MoTa
                            };
                        }
                    }
                    else if (ct.LoaiSanPham == "GOIVACCINE")
                    {
                        var goi = _unitOfWork.GoiVaccines.Query()
                            .FirstOrDefault(g => g.MaGoi == ct.MaSanPham);
                        if (goi != null)
                        {
                            decimal giaSauGiam = TinhGiaSauGiam(goi.GiaGoi, khuyenMai.KieuGiam, khuyenMai.GiaTriGiam);
                            sanPham = new SanPhamApDung
                            {
                                MaSanPham = goi.MaGoi,
                                TenSanPham = goi.TenGoi,
                                LoaiSanPham = "GOIVACCINE",
                                GiaGoc = goi.GiaGoi,
                                GiaSauGiam = giaSauGiam,
                                HinhAnh = goi.HinhAnh,
                                MoTa = goi.MoTa
                            };
                        }
                    }

                    if (sanPham != null)
                    {
                        viewModel.SanPhamApDungs.Add(sanPham);
                    }
                }
            }

            return viewModel;
        }

        /// <summary>
        /// Tính giá sau khi giảm
        /// </summary>
        private decimal TinhGiaSauGiam(decimal giaGoc, string kieuGiam, decimal giaTriGiam)
        {
            if (kieuGiam == "Phần trăm" || kieuGiam == "%")
            {
                return giaGoc - (giaGoc * giaTriGiam / 100);
            }
            else
            {
                return Math.Max(0, giaGoc - giaTriGiam);
            }
        }

        #region Query Operations

        /// <summary>
        /// Lấy tất cả khuyến mãi
        /// </summary>
        public IEnumerable<KhuyenMai> GetAll()
        {
            return _unitOfWork.KhuyenMais.GetAll()
                .OrderByDescending(km => km.NgayBatDau);
        }

        /// <summary>
        /// Lấy khuyến mãi theo mã
        /// </summary>
        public KhuyenMai GetById(string maKM)
        {
            if (string.IsNullOrEmpty(maKM))
                return null;

            return _unitOfWork.KhuyenMais.Query()
                .Include(km => km.ChiTietKhuyenMai)
                .FirstOrDefault(km => km.MaKM == maKM);
        }

        /// <summary>
        /// Lấy khuyến mãi đang hoạt động
        /// </summary>
        public IEnumerable<KhuyenMai> GetActivePromotions()
        {
            var today = DateTime.Now.Date;
            return _unitOfWork.KhuyenMais.Query()
                .Where(km => km.TrangThai == true &&
                            km.NgayBatDau <= today &&
                            km.NgayKetThuc >= today)
                .OrderByDescending(km => km.NgayBatDau)
                .ToList();
        }

        /// <summary>
        /// Lấy khuyến mãi sắp diễn ra
        /// </summary>
        public IEnumerable<KhuyenMai> GetUpcomingPromotions()
        {
            var today = DateTime.Now.Date;
            return _unitOfWork.KhuyenMais.Query()
                .Where(km => km.TrangThai == true && km.NgayBatDau > today)
                .OrderBy(km => km.NgayBatDau)
                .ToList();
        }

        /// <summary>
        /// Lấy khuyến mãi đã hết hạn
        /// </summary>
        public IEnumerable<KhuyenMai> GetExpiredPromotions()
        {
            var today = DateTime.Now.Date;
            return _unitOfWork.KhuyenMais.Query()
                .Where(km => km.NgayKetThuc < today)
                .OrderByDescending(km => km.NgayKetThuc)
                .ToList();
        }

        /// <summary>
        /// Lấy khuyến mãi theo loại
        /// </summary>
        public IEnumerable<KhuyenMai> GetByType(string loaiKM)
        {
            if (string.IsNullOrEmpty(loaiKM))
                return GetAll();

            return _unitOfWork.KhuyenMais.Query()
                .Where(km => km.LoaiKM == loaiKM)
                .OrderByDescending(km => km.NgayBatDau)
                .ToList();
        }

        /// <summary>
        /// Tìm kiếm khuyến mãi theo tên
        /// </summary>
        public IEnumerable<KhuyenMai> SearchByName(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return GetAll();

            keyword = keyword.ToLower().Trim();
            return _unitOfWork.KhuyenMais.Query()
                .Where(km => km.TenKM.ToLower().Contains(keyword) ||
                            km.MoTa.ToLower().Contains(keyword))
                .OrderByDescending(km => km.NgayBatDau)
                .ToList();
        }

        /// <summary>
        /// Lấy chi tiết khuyến mãi với thông tin đầy đủ
        /// </summary>
        public KhuyenMaiViewModel GetDetailViewModel(string maKM)
        {
            var khuyenMai = GetById(maKM);
            if (khuyenMai == null)
                return null;

            var today = DateTime.Now.Date;
            string trangThaiHienThi;

            if (khuyenMai.NgayKetThuc < today)
                trangThaiHienThi = "Đã hết hạn";
            else if (khuyenMai.NgayBatDau > today)
                trangThaiHienThi = "Sắp diễn ra";
            else if (khuyenMai.TrangThai)
                trangThaiHienThi = "Đang diễn ra";
            else
                trangThaiHienThi = "Tạm ngưng";

            var viewModel = new KhuyenMaiViewModel
            {
                MaKM = khuyenMai.MaKM,
                TenKM = khuyenMai.TenKM,
                MoTa = khuyenMai.MoTa,
                LoaiKM = khuyenMai.LoaiKM,
                KieuGiam = khuyenMai.KieuGiam,
                GiaTriGiam = khuyenMai.GiaTriGiam,
                NgayBatDau = khuyenMai.NgayBatDau,
                NgayKetThuc = khuyenMai.NgayKetThuc,
                TrangThai = khuyenMai.TrangThai,
                TrangThaiHienThi = trangThaiHienThi,
                SoNgayConLai = (khuyenMai.NgayKetThuc - today).Days
            };

            return viewModel;
        }

        /// <summary>
        /// Kiểm tra khuyến mãi có đang hoạt động không
        /// </summary>
        public bool IsActive(string maKM)
        {
            var khuyenMai = GetById(maKM);
            if (khuyenMai == null)
                return false;

            var today = DateTime.Now.Date;
            return khuyenMai.TrangThai &&
                   khuyenMai.NgayBatDau <= today &&
                   khuyenMai.NgayKetThuc >= today;
        }

        /// <summary>
        /// Kiểm tra khuyến mãi có tồn tại không
        /// </summary>
        public bool Exists(string maKM)
        {
            return _unitOfWork.KhuyenMais.Query()
                .Any(km => km.MaKM == maKM);
        }

        /// <summary>
        /// Lấy số lượng khuyến mãi
        /// </summary>
        public int GetCount()
        {
            return _unitOfWork.KhuyenMais.Query().Count();
        }

        /// <summary>
        /// Lấy số lượng khuyến mãi đang hoạt động
        /// </summary>
        public int GetActiveCount()
        {
            var today = DateTime.Now.Date;
            return _unitOfWork.KhuyenMais.Query()
                .Count(km => km.TrangThai == true &&
                            km.NgayBatDau <= today &&
                            km.NgayKetThuc >= today);
        }

        /// <summary>
        /// Lấy khuyến mãi nổi bật (sắp hết hạn hoặc giảm nhiều)
        /// </summary>
        public IEnumerable<KhuyenMai> GetFeaturedPromotions(int count = 6)
        {
            var today = DateTime.Now.Date;
            return _unitOfWork.KhuyenMais.Query()
                .Where(km => km.TrangThai == true &&
                            km.NgayBatDau <= today &&
                            km.NgayKetThuc >= today)
                .OrderByDescending(km => km.GiaTriGiam)
                .ThenBy(km => km.NgayKetThuc)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Áp dụng khuyến mãi cho giá trị đơn hàng
        /// </summary>
        public decimal ApplyDiscount(string maKM, decimal giaTriDonHang)
        {
            var khuyenMai = GetById(maKM);
            if (khuyenMai == null || !IsActive(maKM))
                return giaTriDonHang;

            decimal giaTriGiam = 0;

            if (khuyenMai.KieuGiam == "Phần trăm" || khuyenMai.KieuGiam == "%")
            {
                giaTriGiam = giaTriDonHang * khuyenMai.GiaTriGiam / 100;
            }
            else if (khuyenMai.KieuGiam == "Tiền mặt" || khuyenMai.KieuGiam == "VND")
            {
                giaTriGiam = khuyenMai.GiaTriGiam;
            }

            return Math.Max(0, giaTriDonHang - giaTriGiam);
        }

        /// <summary>
        /// Tính giá trị giảm giá
        /// </summary>
        public decimal CalculateDiscountAmount(string maKM, decimal giaTriDonHang)
        {
            var khuyenMai = GetById(maKM);
            if (khuyenMai == null || !IsActive(maKM))
                return 0;

            if (khuyenMai.KieuGiam == "Phần trăm" || khuyenMai.KieuGiam == "%")
            {
                return giaTriDonHang * khuyenMai.GiaTriGiam / 100;
            }
            else if (khuyenMai.KieuGiam == "Tiền mặt" || khuyenMai.KieuGiam == "VND")
            {
                return Math.Min(khuyenMai.GiaTriGiam, giaTriDonHang);
            }

            return 0;
        }

        #endregion

        #region Pagination

        /// <summary>
        /// Lấy khuyến mãi có phân trang
        /// </summary>
        public IEnumerable<KhuyenMai> GetPaged(int page, int pageSize, out int totalRecords)
        {
            var query = _unitOfWork.KhuyenMais.Query()
                .OrderByDescending(km => km.NgayBatDau);

            totalRecords = query.Count();

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Tìm kiếm và phân trang
        /// </summary>
        public IEnumerable<KhuyenMai> SearchAndPaginate(string keyword, string loaiKM, string trangThai, int page, int pageSize, out int totalRecords)
        {
            var query = _unitOfWork.KhuyenMais.Query();
            var today = DateTime.Now.Date;

            // Filter by keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower().Trim();
                query = query.Where(km => km.TenKM.ToLower().Contains(keyword) ||
                                         km.MoTa.ToLower().Contains(keyword));
            }

            // Filter by type
            if (!string.IsNullOrEmpty(loaiKM) && loaiKM != "Tất cả")
            {
                query = query.Where(km => km.LoaiKM == loaiKM);
            }

            // Filter by status
            if (!string.IsNullOrEmpty(trangThai))
            {
                switch (trangThai)
                {
                    case "Đang diễn ra":
                        query = query.Where(km => km.TrangThai == true &&
                                                 km.NgayBatDau <= today &&
                                                 km.NgayKetThuc >= today);
                        break;
                    case "Sắp diễn ra":
                        query = query.Where(km => km.TrangThai == true &&
                                                 km.NgayBatDau > today);
                        break;
                    case "Đã hết hạn":
                        query = query.Where(km => km.NgayKetThuc < today);
                        break;
                }
            }

            query = query.OrderByDescending(km => km.NgayBatDau);
            totalRecords = query.Count();

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }

        #endregion
    }
}
