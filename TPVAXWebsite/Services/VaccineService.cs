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
        /// Lấy full thông tin vaccine trừ số lượng
        /// </summary>

        public VaccineDetailViewModel GetVaccineDetail(string maVC)
        {
            if (string.IsNullOrEmpty(maVC)) return null;

            // BƯỚC 1: Lấy dữ liệu thô (Raw Data) từ Database
            // Ta chưa map thẳng vào ViewModel ngay mà lấy các List string về trước để xử lý nối chuỗi
            var rawData = _unitOfWork.Vaccines.Query()
                .Where(v => v.MaVC == maVC)
                .Select(v => new
                {
                    // Lấy thông tin cơ bản
                    v.MaVC,
                    v.TenVC,    // Lưu ý: Trong Entity tên là TenVC, ViewModel là TenVaccine
                    v.GiaBan,
                    v.SoLuong,  // Tồn kho tổng
                    v.SoMuiToiDa,
                    v.SoThangCho,
                    v.MoTa,
                    v.HinhAnh,
                    v.MaLoai,   // Lấy để tìm vaccine liên quan
                    v.LoaiVaccine.TenLoai,

                    // SỬA ĐỔI: Lấy 1 giá trị Nước SX từ lần nhập mới nhất
                    NuocSanXuat = v.ChiTietPhieuNhap
                       .OrderByDescending(ct => ct.PhieuNhapVaccine.NgayLap) // Sắp xếp ngày nhập mới nhất
                       .Select(ct => ct.NuocSanXuat)
                       .FirstOrDefault(), // Chỉ lấy 1 dòng đầu tiên

                    // SỬA ĐỔI: Lấy 1 Tên Nhà Cung Cấp từ lần nhập mới nhất
                    NhaCungCap = v.ChiTietPhieuNhap
                      .OrderByDescending(ct => ct.PhieuNhapVaccine.NgayLap)
                      .Select(ct => ct.PhieuNhapVaccine.NhaCungCap.TenNCC)
                      .FirstOrDefault(),

                    // Lấy danh sách Bệnh phòng ngừa
                    ListBenh = v.VaccinePhongBenh
                                .Select(vp => vp.LoaiBenh.TenBenh)
                                .ToList()
                })
                .FirstOrDefault();

            if (rawData == null) return null;

            // BƯỚC 2: Xử lý nối chuỗi (Concatenation) trong bộ nhớ (In-Memory)
            // EF6 không hỗ trợ string.Join trực tiếp trong SQL, nên ta làm ở đây
            string strNuocSX = rawData.NuocSanXuat ?? "Đang cập nhật";

            string strTenNCC = rawData.NhaCungCap ?? "Đang cập nhật";

            // BƯỚC 3: Lấy danh sách Vaccine liên quan (Cùng loại)
            // Chỉ lấy những trường cần thiết để hiển thị (Projection)
            var relatedVaccines = _unitOfWork.Vaccines.Query()
                .Where(v => v.MaLoai == rawData.MaLoai && v.MaVC != maVC)
                .OrderBy(v => v.TenVC)
                .Take(4)
                .ToList(); // Lấy List<Domain.Vaccine> theo yêu cầu ViewModel của bạn

            // BƯỚC 4: Đổ dữ liệu vào ViewModel
            var model = new VaccineDetailViewModel
            {
                Vaccine = new VaccineDetailViewModel.VaccineInfo
                {
                    MaVC = rawData.MaVC,
                    TenVaccine = rawData.TenVC, // Map đúng tên cột
                    GiaBan = rawData.GiaBan,   
                    SoMuiToiDa = rawData.SoMuiToiDa,
                    SoThangCho = rawData.SoThangCho,

                    // Hai cột này đã được xử lý đầy đủ
                    NuocSanXuat = strNuocSX,
                    TenNCC = strTenNCC,

                    MoTa = rawData.MoTa,
                    HinhAnh = rawData.HinhAnh,
                    TenLoaiVaccine = rawData.TenLoai
                },
                CacBenhPhong = rawData.ListBenh,
                VaccinesLienQuan = relatedVaccines
            };

            return model;
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
            return vaccine != null && vaccine.SoLuong > 0;
        }

        /// <summary>
        /// Kiểm tra số lượng vaccine
        /// </summary>
        public bool CheckStock(string maVC, int soLuong)
        {
            var vaccine = _unitOfWork.Vaccines.GetById(maVC);
            return vaccine != null && vaccine.SoLuong >= soLuong;
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

                vaccine.SoLuong += soLuongThayDoi;

                if (vaccine.SoLuong < 0)
                    vaccine.SoLuong = 0;

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
                .Where(v => v.SoLuong < threshold)
                .ToList();
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
        #region Recommendations

        /// <summary>
        /// Gợi ý vaccine theo loại vaccine, loại bệnh và từ khóa
        /// </summary>
        public IEnumerable<VaccineRecommendationViewModel> GetRecommendations(string maLoaiVaccine, string maLoaiBenh, string keyword)
        {
            var query = _unitOfWork.Vaccines.Query()
                .Include(v => v.LoaiVaccine)
                .Include(v => v.VaccinePhongBenh.Select(vp => vp.LoaiBenh));

            // Lọc theo loại vaccine (ví dụ: Trẻ em, Người lớn, v.v.)
            if (!string.IsNullOrEmpty(maLoaiVaccine))
            {
                query = query.Where(v => v.MaLoai == maLoaiVaccine);
            }

            // Lọc theo loại bệnh phòng ngừa
            if (!string.IsNullOrEmpty(maLoaiBenh))
            {
                query = query.Where(v => v.VaccinePhongBenh.Any(vp => vp.MaLoaiBenh == maLoaiBenh));
            }

            // Tìm kiếm theo từ khóa
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower().Trim();
                query = query.Where(v => v.TenVC.ToLower().Contains(keyword) || 
                                        v.MoTa.ToLower().Contains(keyword));
            }

            return query.Select(v => new VaccineRecommendationViewModel
            {
                MaVC = v.MaVC,
                TenVC = v.TenVC,
                MoTa = v.MoTa,
                GiaBan = v.GiaBan,
                HinhAnh = v.HinhAnh
            }).ToList();
        }

        /// <summary>
        /// Lấy chi tiết vaccine theo mã (cho trang Detail)
        /// </summary>
        public VaccineRecommendationViewModel GetDetail(string maVC)
        {
            var vaccine = _unitOfWork.Vaccines.GetById(maVC);
            if (vaccine == null) return null;

            return new VaccineRecommendationViewModel
            {
                MaVC = vaccine.MaVC,
                TenVC = vaccine.TenVC,
                MoTa = vaccine.MoTa,
                GiaBan = vaccine.GiaBan,
                HinhAnh = vaccine.HinhAnh
            };
        }

        #endregion

    }
}
