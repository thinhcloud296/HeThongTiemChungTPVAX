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
    /// Service xử lý nghiệp vụ liên quan đến Gói Vaccine
    /// </summary>
    public class GoiVaccineService : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public GoiVaccineService()
        {
            _unitOfWork = new UnitOfWork(new TPVAXDbContext());
        }

        public GoiVaccineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Query Operations

        /// <summary>
        /// Lấy tất cả gói vaccine
        /// </summary>
        public IEnumerable<GoiVaccine> GetAll()
        {
            return _unitOfWork.GoiVaccines.GetAll()
                .OrderBy(g => g.TenGoi);
        }

        /// <summary>
        /// Lấy gói vaccine theo mã
        /// </summary>
        public GoiVaccine GetById(string maGoi)
        {
            if (string.IsNullOrEmpty(maGoi))
                return null;

            return _unitOfWork.GoiVaccines.Query()
                .Include(g => g.ChiTietGoiVaccine.Select(ct => ct.Vaccine))
                .Include(g => g.ChiTietGoiVaccine.Select(ct => ct.Vaccine.LoaiVaccine))
                .FirstOrDefault(g => g.MaGoi == maGoi);
        }

        /// <summary>
        /// Lấy gói vaccine đang hoạt động
        /// </summary>
        public IEnumerable<GoiVaccine> GetActivePackages()
        {
            return _unitOfWork.GoiVaccines.Query()
                .Where(g => g.TrangThai == "Hoạt động" || g.TrangThai == "Đang bán")
                .OrderBy(g => g.TenGoi)
                .ToList();
        }

        /// <summary>
        /// Lấy gói vaccine theo đối tượng áp dụng
        /// </summary>
        public IEnumerable<GoiVaccine> GetByTarget(string doiTuong)
        {
            if (string.IsNullOrEmpty(doiTuong))
                return GetAll();

            return _unitOfWork.GoiVaccines.Query()
                .Where(g => g.DoiTuongApDung.Contains(doiTuong))
                .OrderBy(g => g.TenGoi)
                .ToList();
        }

        /// <summary>
        /// Tìm kiếm gói vaccine theo tên
        /// </summary>
        public IEnumerable<GoiVaccine> SearchByName(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return GetAll();

            keyword = keyword.ToLower().Trim();
            return _unitOfWork.GoiVaccines.Query()
                .Where(g => g.TenGoi.ToLower().Contains(keyword) || 
                            g.MoTa.ToLower().Contains(keyword))
                .OrderBy(g => g.TenGoi)
                .ToList();
        }

        /// <summary>
        /// Lấy chi tiết gói vaccine với thông tin đầy đủ
        /// </summary>
        public GoiVaccineViewModel GetDetailViewModel(string maGoi)
        {
            var goiVaccine = GetById(maGoi);
            if (goiVaccine == null)
                return null;

            var viewModel = new GoiVaccineViewModel
            {
                MaGoi = goiVaccine.MaGoi,
                TenGoi = goiVaccine.TenGoi,
                MoTa = goiVaccine.MoTa,
                DoiTuongApDung = goiVaccine.DoiTuongApDung,
                GiaGoi = goiVaccine.GiaGoi,
                TrangThai = goiVaccine.TrangThai,
                ChiTietGoiVaccine = goiVaccine.ChiTietGoiVaccine.Select(ct => new ChiTietGoiVaccineViewModel
                {
                    MaVC = ct.MaVC,
                    TenVC = ct.Vaccine?.TenVC,
                    TenLoaiVC = ct.Vaccine?.LoaiVaccine?.TenLoai,
                    SoMui = ct.SoMui ?? 1,
                    GhiChu = ct.GhiChu
                }).ToList()
            };

            // Tính tổng giá trị vaccine trong gói
            viewModel.TongGiaTriVaccine = viewModel.ChiTietGoiVaccine.Sum(ct =>
            {
                var vaccine = _unitOfWork.Vaccines.GetById(ct.MaVC);
                return vaccine != null ? vaccine.GiaBan * ct.SoMui : 0;
            });

            // Tính tiết kiệm
            viewModel.TietKiem = viewModel.TongGiaTriVaccine - viewModel.GiaGoi;

            return viewModel;
        }

        /// <summary>
        /// Lấy gói vaccine phổ biến (có nhiều lượt đặt)
        /// </summary>
        public IEnumerable<GoiVaccine> GetPopularPackages(int count = 6)
        {
            // TODO: Implement dựa vào số lượng đơn hàng
            return _unitOfWork.GoiVaccines.Query()
                .Where(g => g.TrangThai == "Hoạt động" || g.TrangThai == "Đang bán")
                .OrderBy(g => g.GiaGoi) // Tạm thời sort theo giá
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Lấy vaccine trong gói
        /// </summary>
        public IEnumerable<Vaccine> GetVaccinesInPackage(string maGoi)
        {
            return _unitOfWork.ChiTietGoiVaccines.Query()
                .Where(ct => ct.MaGoi == maGoi)
                .Include(ct => ct.Vaccine)
                .Select(ct => ct.Vaccine)
                .ToList();
        }

        /// <summary>
        /// Kiểm tra gói vaccine có tồn tại không
        /// </summary>
        public bool Exists(string maGoi)
        {
            return _unitOfWork.GoiVaccines.Query()
                .Any(g => g.MaGoi == maGoi);
        }

        /// <summary>
        /// Lấy số lượng gói vaccine
        /// </summary>
        public int GetCount()
        {
            return _unitOfWork.GoiVaccines.Query().Count();
        }

        /// <summary>
        /// Lấy số lượng gói vaccine đang hoạt động
        /// </summary>
        public int GetActiveCount()
        {
            return _unitOfWork.GoiVaccines.Query()
                .Count(g => g.TrangThai == "Đang áp dụng" || g.TrangThai == "Hoạt động" || g.TrangThai == "Đang bán");
        }

        #endregion

        #region Pagination

        /// <summary>
        /// Lấy gói vaccine có phân trang
        /// </summary>
        public IEnumerable<GoiVaccine> GetPaged(int page, int pageSize, out int totalRecords)
        {
            var query = _unitOfWork.GoiVaccines.Query()
                .OrderBy(g => g.TenGoi);

            totalRecords = query.Count();

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Tìm kiếm và phân trang
        /// </summary>
        public IEnumerable<GoiVaccine> SearchAndPaginate(string keyword, string doiTuong, int page, int pageSize, out int totalRecords)
        {
            var query = _unitOfWork.GoiVaccines.Query();

            // Filter by keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower().Trim();
                query = query.Where(g => g.TenGoi.ToLower().Contains(keyword) ||
                                        g.MoTa.ToLower().Contains(keyword));
            }

            // Filter by target
            if (!string.IsNullOrEmpty(doiTuong) && doiTuong != "Tất cả")
            {
                query = query.Where(g => g.DoiTuongApDung.Contains(doiTuong));
            }

            // Only active packages (bỏ filter để lấy tất cả hoặc cho phép nhiều trạng thái)
            // query = query.Where(g => !string.IsNullOrEmpty(g.TrangThai));

            query = query.OrderBy(g => g.TenGoi);
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
