using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;
using System.Data.Entity; 

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý tất cả chức năng Admin
    /// </summary>
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: Admin/Index
        public ActionResult Index()
        {
            // TODO: Load dashboard data
            return View();
        }

        // GET: Admin/Vaccines
        public ActionResult Vaccines()
        {
            try
            {
                // Lấy tất cả vaccine từ database
                var vaccines = _unitOfWork.Vaccines.Query()
                    .Include(v => v.LoaiVaccine)
                    .ToList();

                // Lấy danh sách phòng bệnh cho mỗi vaccine
                var vaccinePhongBenhs = _unitOfWork.VaccinePhongBenhs.Query()
                    .Include(vb => vb.LoaiBenh)
                    .ToList();

                // Chuyển đổi sang ViewModel
                var viewModels = vaccines.Select(v => new AdminVaccineViewModel
                {
                    MaVC = v.MaVC,
                    TenVC = v.TenVC,
                    GiaBan = v.GiaBan,
                    SoLuongTon = v.SoLuong,
                    SoMuiToiDa = v.SoMuiToiDa,
                    SoThangCho = v.SoThangCho,
                    MaLoai = v.MaLoai,
                    TenLoai = v.LoaiVaccine?.TenLoai ?? "Chưa phân loại",
                    MoTa = v.MoTa,
                    HinhAnh = v.HinhAnh
                }).ToList();

                return View(viewModels);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                System.Diagnostics.Debug.WriteLine("Error loading vaccines: " + ex.Message);
                return View(new List<AdminVaccineViewModel>());
            }
        }

        // GET: Admin/Customers
        public ActionResult Customers()
        {
            try
            {
                var customers = _unitOfWork.KhachHangs.GetAll().ToList();
                var viewModels = customers.Select(k => new AdminCustomerViewModel
                {
                    MaKH = k.MaKH,
                    HoTen = k.HoTen,
                    CCCD = k.CCCD,
                    NgaySinh = k.NgaySinh,
                    GioiTinh = k.GioiTinh,
                    DiaChi = k.DiaChi,
                    SoDT = k.SoDT,
                    Email = k.Email,
                    MaTK = k.MaTK
                }).ToList();

                return View(viewModels);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading customers: " + ex.Message);
                return View(new List<AdminCustomerViewModel>());
            }
        }

        // GET: Admin/Appointments
        public ActionResult Appointments()
        {
            try
            {
                var appointments = _unitOfWork.LichTiems.Query()
                    .Include(lt => lt.HoSoTiemChung)
                    .Include(lt => lt.Vaccine)
                    .Include(lt => lt.NhanVien)
                    .ToList();

                var viewModels = appointments.Select(lt => new AdminAppointmentViewModel
                {
                    MaLT = lt.MaLT,
                    MaHSTC = lt.MaHSTC,
                    TenNguoiTiem = lt.HoSoTiemChung?.HoTen ?? "Chưa xác định",
                    TenVaccine = lt.Vaccine?.TenVC ?? "Chưa xác định",
                    NgayHenTiem = lt.NgayHenTiem,
                    NgayTiemThucTe = lt.NgayTiemThucTe,
                    SoMui = lt.SoMui,
                    TrangThai = lt.TrangThai,
                    GhiChu = lt.GhiChu,
                    MaNV = lt.MaNV,
                    TenNhanVien = lt.NhanVien?.HoTen ?? "Chưa phân công"
                }).OrderByDescending(a => a.NgayHenTiem).ToList();

                return View(viewModels);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading appointments: " + ex.Message);
                return View(new List<AdminAppointmentViewModel>());
            }
        }

        // GET: Admin/GoiVaccine
        public ActionResult GoiVaccine()
        {
            try
            {
                var goiVaccines = _unitOfWork.GoiVaccines.GetAll().ToList();
                var viewModels = goiVaccines.Select(g => new AdminGoiVaccineViewModel
                {
                    MaGoi = g.MaGoi,
                    TenGoi = g.TenGoi,
                    MoTa = g.MoTa,
                    DoiTuongApDung = g.DoiTuongApDung,
                    GiaGoi = g.GiaGoi,
                    TrangThai = g.TrangThai,
                    HinhAnh = g.HinhAnh
                }).ToList();

                return View(viewModels);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading vaccine packages: " + ex.Message);
                return View(new List<AdminGoiVaccineViewModel>());
            }
        }

        // GET: Admin/InvoiceDetails
        public ActionResult InvoiceDetails(string id)
        {
            // TODO: Load chi tiết hóa đơn
            return View();
        }

        // GET: Admin/NhaCungCap
        public ActionResult NhaCungCap()
        {
            try
            {
                var suppliers = _unitOfWork.NhaCungCaps.GetAll().ToList();
                var viewModels = suppliers.Select(s => new AdminNhaCungCapViewModel
                {
                    MaNCC = s.MaNCC,
                    TenNCC = s.TenNCC,
                    DiaChi = s.DiaChi,
                    Email = s.Email,
                    SoDT = s.SoDT,
                    TenNganHang = s.TenNganHang,
                    SoTK = s.SoTK
                }).ToList();

                return View(viewModels);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading suppliers: " + ex.Message);
                return View(new List<AdminNhaCungCapViewModel>());
            }
        }

        // GET: Admin/NhanVien
        public ActionResult NhanVien()
        {
            try
            {
                var employees = _unitOfWork.NhanViens.GetAll().ToList();
                var viewModels = employees.Select(e => new AdminNhanVienViewModel
                {
                    MaNV = e.MaNV,
                    HoTen = e.HoTen,
                    GioiTinh = e.GioiTinh,
                    NgaySinh = e.NgaySinh,
                    CCCD = e.CCCD,
                    NgayVaoLam = e.NgayVaoLam,
                    SoDT = e.SoDT,
                    DiaChi = e.DiaChi,
                    Email = e.Email,
                    ChucVu = e.ChucVu,
                    TrangThai = e.TrangThai
                }).ToList();

                return View(viewModels);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading employees: " + ex.Message);
                return View(new List<AdminNhanVienViewModel>());
            }
        }

        // GET: Admin/Profile
        public new ActionResult Profile()
        {
            // TODO: Hiển thị profile admin
            return View();
        }

        // GET: Admin/Reports
        public ActionResult Reports()
        {
            // TODO: Hiển thị báo cáo thống kê
            return View();
        }
    }
}
