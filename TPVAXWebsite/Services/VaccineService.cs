using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Services
{
    /// <summary>
    /// Service xử lý nghiệp vụ liên quan đến Vaccine
    /// </summary>
    public class VaccineService : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public VaccineService()
        {
            _unitOfWork = new UnitOfWork(new TPVAXDbContext());
        }

        public VaccineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region CRUD Operations

        /// <summary>
        /// Lấy tất cả vaccine
        /// </summary>
        public IEnumerable<Vaccine> GetAll()
        {
            return _unitOfWork.Vaccines.GetAll();
        }

        /// <summary>
        /// Lấy vaccine theo mã
        /// </summary>
        public Vaccine GetById(string maVC)
        {
            if (string.IsNullOrEmpty(maVC))
                return null;

            return _unitOfWork.Vaccines.Query()
                .Include(v => v.LoaiVaccine)
                .Include(v => v.VaccinePhongBenh.Select(vp => vp.LoaiBenh))
                .FirstOrDefault(v => v.MaVC == maVC);
        }

        /// <summary>
        /// Thêm vaccine mới
        /// </summary>
        public bool Add(Vaccine vaccine)
        {
            try
            {
                if (vaccine == null)
                    return false;

                // Kiểm tra mã vaccine đã tồn tại
                if (_unitOfWork.Vaccines.Any(v => v.MaVC == vaccine.MaVC))
                    return false;

                _unitOfWork.Vaccines.Add(vaccine);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Cập nhật vaccine
        /// </summary>
        public bool Update(Vaccine vaccine)
        {
            try
            {
                if (vaccine == null)
                    return false;

                var existing = _unitOfWork.Vaccines.GetById(vaccine.MaVC);
                if (existing == null)
                    return false;

                _unitOfWork.Vaccines.Update(vaccine);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa vaccine
        /// </summary>
        public bool Delete(string maVC)
        {
            try
            {
                if (string.IsNullOrEmpty(maVC))
                    return false;

                var vaccine = _unitOfWork.Vaccines.GetById(maVC);
                if (vaccine == null)
                    return false;

                _unitOfWork.Vaccines.Remove(vaccine);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Search & Filter

        /// <summary>
        /// Tìm kiếm vaccine theo tên
        /// </summary>
        public IEnumerable<Vaccine> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return GetAll();

            keyword = keyword.ToLower().Trim();
            return _unitOfWork.Vaccines.Query()
                .Include(v => v.LoaiVaccine)
                .Where(v => v.TenVC.ToLower().Contains(keyword))
                .ToList();
        }

        /// <summary>
        /// Lọc vaccine theo loại
        /// </summary>
        public IEnumerable<Vaccine> GetByLoaiVaccine(string maLoai)
        {
            if (string.IsNullOrEmpty(maLoai))
                return GetAll();

            return _unitOfWork.Vaccines.Query()
                .Include(v => v.LoaiVaccine)
                .Where(v => v.MaLoai == maLoai)
                .ToList();
        }

        /// <summary>
        /// Lọc vaccine theo loại bệnh
        /// </summary>
        public IEnumerable<Vaccine> GetByLoaiBenh(string maLoaiBenh)
        {
            if (string.IsNullOrEmpty(maLoaiBenh))
                return GetAll();

            return _unitOfWork.Vaccines.Query()
                .Include(v => v.LoaiVaccine)
                .Include(v => v.VaccinePhongBenh.Select(vp => vp.LoaiBenh))
                .Where(v => v.VaccinePhongBenh.Any(vp => vp.MaLoaiBenh == maLoaiBenh))
                .ToList();
        }

        /// <summary>
        /// Tìm kiếm và lọc vaccine
        /// </summary>
        public IEnumerable<Vaccine> SearchAndFilter(string keyword, string maLoai, string maLoaiBenh)
        {
            var query = _unitOfWork.Vaccines.Query()
                .Include(v => v.LoaiVaccine)
                .Include(v => v.VaccinePhongBenh.Select(vp => vp.LoaiBenh));

            // Tìm kiếm theo từ khóa
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower().Trim();
                query = query.Where(v => v.TenVC.ToLower().Contains(keyword));
            }

            // Lọc theo loại vaccine
            if (!string.IsNullOrEmpty(maLoai))
            {
                query = query.Where(v => v.MaLoai == maLoai);
            }

            // Lọc theo loại bệnh
            if (!string.IsNullOrEmpty(maLoaiBenh))
            {
                query = query.Where(v => v.VaccinePhongBenh.Any(vp => vp.MaLoaiBenh == maLoaiBenh));
            }

            return query.ToList();
        }

        #endregion

        #region Business Logic

        /// <summary>
        /// Kiểm tra vaccine còn hàng
        /// </summary>
        public bool IsAvailable(string maVC)
        {
            var vaccine = _unitOfWork.Vaccines.GetById(maVC);
            return vaccine != null && vaccine.SoLuongTon > 0;
        }

        /// <summary>
        /// Kiểm tra số lượng vaccine
        /// </summary>
        public bool CheckStock(string maVC, int soLuong)
        {
            var vaccine = _unitOfWork.Vaccines.GetById(maVC);
            return vaccine != null && vaccine.SoLuongTon >= soLuong;
        }

        /// <summary>
        /// Cập nhật số lượng tồn kho
        /// </summary>
        public bool UpdateStock(string maVC, int soLuongThayDoi)
        {
            try
            {
                var vaccine = _unitOfWork.Vaccines.GetById(maVC);
                if (vaccine == null)
                    return false;

                vaccine.SoLuongTon += soLuongThayDoi;

                if (vaccine.SoLuongTon < 0)
                    vaccine.SoLuongTon = 0;

                _unitOfWork.Vaccines.Update(vaccine);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy vaccine liên quan (cùng loại)
        /// </summary>
        public IEnumerable<Vaccine> GetRelatedVaccines(string maVC, int take = 4)
        {
            var vaccine = _unitOfWork.Vaccines.GetById(maVC);
            if (vaccine == null || string.IsNullOrEmpty(vaccine.MaLoai))
                return Enumerable.Empty<Vaccine>();

            return _unitOfWork.Vaccines.Query()
                .Include(v => v.LoaiVaccine)
                .Where(v => v.MaLoai == vaccine.MaLoai && v.MaVC != maVC)
                .Take(take)
                .ToList();
        }

        /// <summary>
        /// Lấy danh sách loại bệnh mà vaccine phòng được
        /// </summary>
        public IEnumerable<LoaiBenh> GetDiseasesByVaccine(string maVC)
        {
            var vaccinePhongBenhs = _unitOfWork.VaccinePhongBenhs.Query()
                .Include(vp => vp.LoaiBenh)
                .Where(vp => vp.MaVC == maVC)
                .ToList();

            return vaccinePhongBenhs.Select(vp => vp.LoaiBenh).ToList();
        }

        /// <summary>
        /// Lấy vaccine sắp hết hàng (tồn kho < 10)
        /// </summary>
        public IEnumerable<Vaccine> GetLowStockVaccines(int threshold = 10)
        {
            return _unitOfWork.Vaccines.Query()
                .Include(v => v.LoaiVaccine)
                .Where(v => v.SoLuongTon < threshold)
                .ToList();
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
