using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.DAL.Repositories;
using TPVAXWebsite.Filters;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý tất cả chức năng Admin
    /// </summary>
    [CustomAuthorize(Roles = "Admin,QuanLy")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController()
        {
            _unitOfWork = new UnitOfWork(new TPVAXDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Dashboard

        // GET: Admin/Index
        public ActionResult Index()
        {
            var viewModel = new AdminDashboardViewModel
            {
                TongSoVaccine = _unitOfWork.Repository<Vaccine>().GetAll().Count(),
                TongSoKhachHang = _unitOfWork.Repository<KhachHang>().GetAll().Count(),
                TongSoLichHen = _unitOfWork.Repository<LichTiem>().GetAll().Count(),
                TongDoanhThu = _unitOfWork.Repository<HoaDon>()
                    .GetAll()
                    .Where(h => h.TrangThai == true )
                    .Sum(h => (decimal?)h.TongTien) ?? 0,
                LichHenHomNay = _unitOfWork.Repository<LichTiem>()
                    .GetAll()
                    .Where(l => l.NgayHenTiem.Date == DateTime.Today)
                    .Count(),
                VaccineSapHet = _unitOfWork.Repository<Vaccine>()
                    .GetAll()
                    .Where(v => v.SoLuongTon < 10)
                    .Count()
            };

            return View(viewModel);
        }

        #endregion

        #region Vaccines Management

        // GET: Admin/Vaccines
        public ActionResult Vaccines()
        {
            var vaccines = _unitOfWork.Repository<Models.Domain.Vaccine>()
                .GetAll()
               // .OrderByDescending(v => v.NgayNhap)
                .ToList();

            return View(vaccines);
        }

        // POST: Admin/CreateVaccine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateVaccine(Vaccine vaccine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vaccine.MaVC = Guid.NewGuid().ToString();
                 //   vaccine.NgayLap = DateTime.Now;
                    _unitOfWork.Repository<Vaccine>().Add(vaccine);
                    _unitOfWork.SaveChanges();

                    return Json(new { success = true, message = "Thêm vắc xin thành công!" });
                }

                return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // POST: Admin/UpdateVaccine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateVaccine(Vaccine vaccine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Repository<Vaccine>().Update(vaccine);
                    _unitOfWork.SaveChanges();

                    return Json(new { success = true, message = "Cập nhật vắc xin thành công!" });
                }

                return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // POST: Admin/DeleteVaccine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteVaccine(string id)
        {
            try
            {
                var vaccine = _unitOfWork.Repository<Vaccine>().GetById(id);
                if (vaccine != null)
                {
                    _unitOfWork.Repository<Vaccine>().Remove(vaccine);
                    _unitOfWork.SaveChanges();

                    return Json(new { success = true, message = "Xóa vắc xin thành công!" });
                }

                return Json(new { success = false, message = "Không tìm thấy vắc xin!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        #endregion

        #region Customers Management

        // GET: Admin/Customers
        public ActionResult Customers()
        {
            var customers = _unitOfWork.Repository<KhachHang>()
                .GetAll()
                .OrderByDescending(k => k.NgaySinh)
                .ToList();

            return View(customers);
        }

        // GET: Admin/CustomerDetail/{id}
        public ActionResult CustomerDetail(string id)
        {
            var customer = _unitOfWork.Repository<KhachHang>().GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        #endregion

        #region Appointments Management

        // GET: Admin/Appointments
        public ActionResult Appointments()
        {
            var appointments = _unitOfWork.Repository<LichTiem>()
                .GetAll()
                .OrderByDescending(l => l.NgayHenTiem)
                .ToList();

            return View(appointments);
        }

        // POST: Admin/UpdateAppointmentStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateAppointmentStatus(int id, string status)
        {
            try
            {
                var appointment = _unitOfWork.Repository<LichTiem>().GetById(id);
                if (appointment != null)
                {
                    // Sửa: Chuyển string sang bool
                    appointment.TrangThai = (status == "Đã tiêm" || status == "Hoàn thành" || status == "true");
                    _unitOfWork.Repository<LichTiem>().Update(appointment);
                    _unitOfWork.SaveChanges();
                    return Json(new { success = true, message = "Cập nhật trạng thái thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy lịch hẹn!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        #endregion

        #region Vaccine Packages Management

        // GET: Admin/GoiVaccine
        public ActionResult GoiVaccine()
        {
            var goiVaccines = _unitOfWork.Repository<GoiVaccine>()
                .GetAll()
                .OrderBy(g => g.TenGoi)
                .ToList();

            return View(goiVaccines);
        }

        // POST: Admin/CreateGoiVaccine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateGoiVaccine(GoiVaccine goiVaccine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    goiVaccine.MaGoi = Guid.NewGuid().ToString();
                    _unitOfWork.Repository<GoiVaccine>().Add(goiVaccine);
                    _unitOfWork.SaveChanges();

                    return Json(new { success = true, message = "Thêm gói vắc xin thành công!" });
                }

                return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        #endregion

        #region Suppliers Management

        // GET: Admin/NhaCungCap
        public ActionResult NhaCungCap()
        {
            var suppliers = _unitOfWork.Repository<NhaCungCap>()
                .GetAll()
                .OrderBy(n => n.TenNCC)
                .ToList();

            return View(suppliers);
        }

        // POST: Admin/CreateNhaCungCap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateNhaCungCap(NhaCungCap nhaCungCap)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    nhaCungCap.MaNCC = Guid.NewGuid().ToString();
                    _unitOfWork.Repository<NhaCungCap>().Add(nhaCungCap);
                    _unitOfWork.SaveChanges();

                    return Json(new { success = true, message = "Thêm nhà cung cấp thành công!" });
                }

                return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        #endregion

        #region Staff Management

        // GET: Admin/NhanVien
        public ActionResult NhanVien()
        {
            var staff = _unitOfWork.Repository<NhanVien>()
                .GetAll()
                .OrderBy(n => n.HoTen)
                .ToList();

            return View(staff);
        }

        // POST: Admin/CreateNhanVien
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateNhanVien(NhanVien nhanVien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    nhanVien.MaNV = Guid.NewGuid().ToString();
                    _unitOfWork.Repository<NhanVien>().Add(nhanVien);
                    _unitOfWork.SaveChanges();

                    return Json(new { success = true, message = "Thêm nhân viên thành công!" });
                }

                return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        #endregion

        #region Import Management

        // GET: Admin/PhieuNhap
        public ActionResult PhieuNhap()
        {
            var phieuNhaps = _unitOfWork.Repository<PhieuNhapVaccine>()
                .GetAll()
                .OrderByDescending(p => p.NgayLap)
                .ToList();

            return View(phieuNhaps);
        }

        // POST: Admin/CreatePhieuNhap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreatePhieuNhap(PhieuNhapVaccine phieuNhap)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    phieuNhap.MaPN = Guid.NewGuid().ToString();
                    phieuNhap.NgayLap = DateTime.Now;
                    _unitOfWork.Repository<PhieuNhapVaccine>().Add(phieuNhap);
                    _unitOfWork.SaveChanges();

                    return Json(new { success = true, message = "Tạo phiếu nhập thành công!" });
                }

                return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        #endregion

        #region Calendar

        // GET: Admin/Calendar
        public ActionResult Calendar()
        {
            var appointments = _unitOfWork.Repository<LichTiem>()
                .GetAll()
                .Where(l => l.NgayHenTiem >= DateTime.Today)
                .OrderBy(l => l.NgayHenTiem)
                .ToList();

            return View(appointments);
        }

        #endregion

        #region Reports

        // GET: Admin/Reports
        public ActionResult Reports()
        {
            var viewModel = new AdminReportsViewModel
            {
                DoanhThuThang = CalculateMonthlyRevenue(),
                SoLuongTiemThang = CalculateMonthlyVaccinations(),
                TopVaccines = GetTopVaccines(10),
                DoanhThuTheoNgay = GetDailyRevenue(30)
            };

            return View(viewModel);
        }

        private decimal CalculateMonthlyRevenue()
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return _unitOfWork.Repository<HoaDon>()
                .GetAll()
                .Where(h => h.NgayLap >= startOfMonth && h.TrangThai == true)
                .Sum(h => (decimal?)h.TongTien) ?? 0;
        }

        private int CalculateMonthlyVaccinations()
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return _unitOfWork.Repository<LichTiem>()
                .GetAll()
                .Where(l => l.NgayHenTiem >= startOfMonth && l.TrangThai == true)
                .Count();
        }

        private object GetTopVaccines(int count)
        {
            // Implementation for top vaccines
            return new { };
        }

        private object GetDailyRevenue(int days)
        {
            // Implementation for daily revenue
            return new { };
        }

        #endregion

        #region Profile

        // GET: Admin/Profile
        public ActionResult Profile()
        {
            var userId = Session["UserId"]?.ToString();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var taiKhoan = _unitOfWork.Repository<TaiKhoan>().GetById(userId);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }

            return View(taiKhoan);
        }

        // POST: Admin/UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(NhanVien model)  // ĐỔI THÀNH NhanVien
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var nhanVien = _unitOfWork.Repository<NhanVien>()
                        .GetById(model.MaNV);   // Dùng MaNV, không phải MaTK

                    if (nhanVien != null)
                    {
                        nhanVien.HoTen = model.HoTen;
                        nhanVien.Email = model.Email;
                        nhanVien.SoDT = model.SoDT;        // Đúng tên cột trong DB
                                                           // nhanVien.DiaChi = model.DiaChi; // thêm nếu cần
                                                           // nhanVien.GioiTinh = model.GioiTinh;

                        _unitOfWork.Repository<NhanVien>().Update(nhanVien);
                        _unitOfWork.SaveChanges();

                        TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
                        return RedirectToAction("Profile");
                    }
                }

                TempData["ErrorMessage"] = "Cập nhật thất bại!";
                return View("Profile", model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
                return View("Profile", model);
            }
        }

        #endregion

        #region Invoice Details

        // GET: Admin/InvoiceDetails/{id}
        public ActionResult InvoiceDetails(string id)
        {
            var hoaDon = _unitOfWork.Repository<HoaDon>().GetById(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            var chiTiet = _unitOfWork.Repository<ChiTietHoaDon>()
                .GetAll()
                .Where(c => c.MaHD == id)
                .ToList();

            var viewModel = new InvoiceDetailsViewModel
            {
                HoaDon = hoaDon,
                ChiTietHoaDon = chiTiet
            };

            return View(viewModel);
        }

        #endregion
    }
}
