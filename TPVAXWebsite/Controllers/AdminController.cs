using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
        private const string VACCINE_IMAGE_PATH = "~/Content/Images/vaccines/";
        private const int MAX_IMAGE_SIZE = 5 * 1024 * 1024; // 5MB

        public AdminController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // ============================================================================
        // DASHBOARD
        // ============================================================================

        // GET: Admin/Index
        public ActionResult Index()
        {
            // TODO: Load dashboard data
            return View();
        }

        // ============================================================================
        // VACCINE CRUD - READ (danh sách)
        // ============================================================================

        /// <summary>
        /// GET: Admin/Vaccines - Hiển thị danh sách vaccine
        /// </summary>
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
                System.Diagnostics.Debug.WriteLine("Error loading vaccines: " + ex.Message);
                return View(new List<AdminVaccineViewModel>());
            }
        }

        // ============================================================================
        // VACCINE CRUD - API Methods
        // ============================================================================

        /// <summary>
        /// POST: Admin/CreateVaccine - Tạo vaccine mới
        /// </summary>
        [HttpPost]
        public ActionResult CreateVaccine(AdminVaccineCreateEditViewModel model)
        {
            try
            {
                // Debug
                System.Diagnostics.Debug.WriteLine("CreateVaccine called");
                System.Diagnostics.Debug.WriteLine("ModelState.IsValid: " + ModelState.IsValid);
                
                // Validate ModelState
                if (!ModelState.IsValid)
                {
                    var errors = GetModelStateErrors();
                    System.Diagnostics.Debug.WriteLine("ModelState errors: " + string.Join(", ", errors));
                    return Json(new
                    {
                        success = false,
                        message = "Dữ liệu không hợp lệ",
                        errors = errors
                    });
                }

                // Parse SelectedLoaiBenhIds from JSON string
                List<string> selectedBenhs = new List<string>();
                if (!string.IsNullOrEmpty(Request.Form["SelectedLoaiBenhIds"]))
                {
                    try
                    {
                        var jsonStr = Request.Form["SelectedLoaiBenhIds"];
                        selectedBenhs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(jsonStr) ?? new List<string>();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error parsing benehs: " + ex.Message);
                    }
                }

                // Validate bệnh phòng ngừa
                if (selectedBenhs == null || selectedBenhs.Count == 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Vui lòng chọn ít nhất một bệnh phòng ngừa"
                    });
                }

                // Xử lý upload hình ảnh
                string imagePath = null;
                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    var validateImageResult = ValidateImage(model.ImageFile);
                    if (!validateImageResult.Item1)
                    {
                        return Json(new { success = false, message = validateImageResult.Item2 });
                    }

                    imagePath = SaveVaccineImage(model.ImageFile);
                }

                // Generate MaVC
                var lastVaccine = _unitOfWork.Vaccines.Query()
                    .OrderByDescending(v => v.MaVC)
                    .FirstOrDefault();
                
                string newMaVC = GenerateVaccineMaVC(lastVaccine?.MaVC);

                // Tạo entity Vaccine mới
                var vaccine = new Vaccine
                {
                    MaVC = newMaVC,
                    TenVC = model.TenVC,
                    GiaBan = model.GiaBan,
                    SoLuong = model.SoLuong,
                    SoMuiToiDa = model.SoMuiToiDa,
                    SoThangCho = model.SoThangCho,
                    MaLoai = model.MaLoai,
                    MoTa = model.MoTa,
                    HinhAnh = imagePath
                };

                // Insert vào database
                _unitOfWork.Vaccines.Add(vaccine);
                _unitOfWork.SaveChanges();

                // Tạo VaccinePhongBenh cho mỗi bệnh được chọn
                foreach (var maBenhId in selectedBenhs)
                {
                    var vaccinePhongBenh = new VaccinePhongBenh
                    {
                        MaVC = newMaVC,
                        MaLoaiBenh = maBenhId
                    };
                    _unitOfWork.VaccinePhongBenhs.Add(vaccinePhongBenh);
                }

                _unitOfWork.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Thêm vaccine thành công!",
                    data = new { MaVC = newMaVC }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error creating vaccine: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("Stack trace: " + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
                return Json(new
                {
                    success = false,
                    message = "Lỗi hệ thống: " + ex.Message,
                    innerException = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        /// <summary>
        /// GET: Admin/GetVaccine - Lấy thông tin vaccine để chỉnh sửa
        /// </summary>
        [HttpGet]
        public ActionResult GetVaccine(string id)
        {
            try
            {
                var vaccine = _unitOfWork.Vaccines.GetById(id);
                if (vaccine == null)
                {
                    return Json(new { success = false, message = "Vaccine không tồn tại" },
                        JsonRequestBehavior.AllowGet);
                }

                // Lấy danh sách bệnh của vaccine
                var selectedLoaiBenhIds = _unitOfWork.VaccinePhongBenhs.Query()
                    .Where(vpb => vpb.MaVC == id)
                    .Select(vpb => vpb.MaLoaiBenh)
                    .ToList();

                var viewModel = new AdminVaccineCreateEditViewModel
                {
                    MaVC = vaccine.MaVC,
                    TenVC = vaccine.TenVC,
                    GiaBan = vaccine.GiaBan,
                    SoLuong = vaccine.SoLuong,
                    SoMuiToiDa = vaccine.SoMuiToiDa,
                    SoThangCho = vaccine.SoThangCho,
                    MaLoai = vaccine.MaLoai,
                    TenLoai = vaccine.LoaiVaccine?.TenLoai,
                    MoTa = vaccine.MoTa,
                    HinhAnh = vaccine.HinhAnh,
                    HinhAnhCu = vaccine.HinhAnh,
                    SelectedLoaiBenhIds = selectedLoaiBenhIds
                };

                return Json(new { success = true, data = viewModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error getting vaccine: " + ex.Message);
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message },
                    JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// POST: Admin/EditVaccine - Cập nhật vaccine
        /// </summary>
        [HttpPost]
        public ActionResult EditVaccine(AdminVaccineCreateEditViewModel model)
        {
            try
            {
                // Parse SelectedLoaiBenhIds from JSON string
                List<string> selectedBenhs = new List<string>();
                if (!string.IsNullOrEmpty(Request.Form["SelectedLoaiBenhIds"]))
                {
                    try
                    {
                        var jsonStr = Request.Form["SelectedLoaiBenhIds"];
                        selectedBenhs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(jsonStr) ?? new List<string>();
                        model.SelectedLoaiBenhIds = selectedBenhs;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error parsing benhs in EditVaccine: " + ex.Message);
                    }
                }

                if (string.IsNullOrEmpty(model.MaVC))
                {
                    return Json(new { success = false, message = "Mã vaccine không hợp lệ" });
                }

                // Validate bệnh phòng ngừa
                if (model.SelectedLoaiBenhIds == null || model.SelectedLoaiBenhIds.Count == 0)
                {
                    return Json(new { success = false, message = "Vui lòng chọn ít nhất một bệnh phòng ngừa" });
                }

                var vaccine = _unitOfWork.Vaccines.GetById(model.MaVC);
                if (vaccine == null)
                {
                    return Json(new { success = false, message = "Vaccine không tồn tại" });
                }

                // Xử lý upload hình ảnh mới
                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    var validateImageResult = ValidateImage(model.ImageFile);
                    if (!validateImageResult.Item1)
                    {
                        return Json(new { success = false, message = validateImageResult.Item2 });
                    }

                    // Xóa ảnh cũ
                    if (!string.IsNullOrEmpty(vaccine.HinhAnh))
                    {
                        DeleteVaccineImage(vaccine.HinhAnh);
                    }

                    vaccine.HinhAnh = SaveVaccineImage(model.ImageFile);
                }

                // Update các field
                vaccine.TenVC = model.TenVC;
                vaccine.GiaBan = model.GiaBan;
                vaccine.SoLuong = model.SoLuong;
                vaccine.SoMuiToiDa = model.SoMuiToiDa;
                vaccine.SoThangCho = model.SoThangCho;
                vaccine.MaLoai = model.MaLoai;
                vaccine.MoTa = model.MoTa;

                _unitOfWork.Vaccines.Update(vaccine);

                // Xóa tất cả VaccinePhongBenh cũ
                var oldVaccinePhongBenhs = _unitOfWork.VaccinePhongBenhs.Query()
                    .Where(vpb => vpb.MaVC == model.MaVC)
                    .ToList();

                foreach (var vpb in oldVaccinePhongBenhs)
                {
                    _unitOfWork.VaccinePhongBenhs.Remove(vpb);
                }

                // Thêm VaccinePhongBenh mới
                foreach (var maBenhId in model.SelectedLoaiBenhIds)
                {
                    var vaccinePhongBenh = new VaccinePhongBenh
                    {
                        MaVC = model.MaVC,
                        MaLoaiBenh = maBenhId
                    };
                    _unitOfWork.VaccinePhongBenhs.Add(vaccinePhongBenh);
                }

                _unitOfWork.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Cập nhật vaccine thành công!"
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error editing vaccine: " + ex.Message);
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        /// <summary>
        /// POST: Admin/DeleteVaccine - Xóa vaccine
        /// </summary>
        [HttpPost]
        public ActionResult DeleteVaccine(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Mã vaccine không hợp lệ",
                        errorType = "InvalidId"
                    });
                }

                var vaccine = _unitOfWork.Vaccines.GetById(id);
                if (vaccine == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Vaccine không tồn tại",
                        errorType = "NotFound"
                    });
                }

                // Kiểm tra ràng buộc: Có trong LichTiem?
                var hasAppointments = _unitOfWork.LichTiems.Any(lt => lt.MaVC == id);
                if (hasAppointments)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Không thể xóa vaccine này vì đã có lịch tiêm liên quan",
                        errorType = "CannotDelete"
                    });
                }

                // Kiểm tra ràng buộc: Có trong ChiTietGoiVaccine?
                var hasGoiVaccine = _unitOfWork.ChiTietGoiVaccines.Any(ctgv => ctgv.MaVC == id);
                if (hasGoiVaccine)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Không thể xóa vaccine này vì đã có trong gói vaccine",
                        errorType = "CannotDelete"
                    });
                }

                // Xóa VaccinePhongBenh liên quan
                var vaccinePhongBenhs = _unitOfWork.VaccinePhongBenhs.Query()
                    .Where(vpb => vpb.MaVC == id)
                    .ToList();

                foreach (var vpb in vaccinePhongBenhs)
                {
                    _unitOfWork.VaccinePhongBenhs.Remove(vpb);
                }

                // Xóa ảnh
                if (!string.IsNullOrEmpty(vaccine.HinhAnh))
                {
                    DeleteVaccineImage(vaccine.HinhAnh);
                }

                // Xóa vaccine
                _unitOfWork.Vaccines.Remove(vaccine);
                _unitOfWork.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Xóa vaccine thành công!"
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting vaccine: " + ex.Message);
                return Json(new
                {
                    success = false,
                    message = "Lỗi hệ thống: " + ex.Message,
                    errorType = "Error"
                });
            }
        }

        /// <summary>
        /// <summary>
        /// GET: Admin/GetLoaiVaccineList - Lấy danh sách loại vaccine
        /// </summary>
        [HttpGet]
        public JsonResult GetLoaiVaccineList()
        {
            try
            {
                // Lấy tất cả loại vaccine
                var loaiVaccines = _unitOfWork.LoaiVaccines.GetAll()
                    .OrderBy(lv => lv.TenLoai)
                    .Select(lv => new 
                    { 
                        id = lv.MaLoai, 
                        text = lv.TenLoai 
                    })
                    .ToList();

                // Nếu không có dữ liệu, trả về mảng trống nhưng success = true
                return Json(
                    new { success = true, data = loaiVaccines }, 
                    JsonRequestBehavior.AllowGet
                );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetLoaiVaccineList Error: {ex.Message}");
                return Json(
                    new { success = false, message = "Lỗi khi tải danh sách loại vaccine: " + ex.Message }, 
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        /// <summary>
        /// GET: Admin/GetLoaiBenhList - Lấy danh sách loại bệnh
        /// </summary>
        [HttpGet]
        public JsonResult GetLoaiBenhList()
        {
            try
            {
                // Lấy tất cả loại bệnh
                var loaiBenhs = _unitOfWork.LoaiBenhs.GetAll()
                    .OrderBy(lb => lb.TenBenh)
                    .Select(lb => new 
                    { 
                        id = lb.MaLoaiBenh, 
                        text = lb.TenBenh 
                    })
                    .ToList();

                // Nếu không có dữ liệu, trả về mảng trống nhưng success = true
                return Json(
                    new { success = true, data = loaiBenhs }, 
                    JsonRequestBehavior.AllowGet
                );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetLoaiBenhList Error: {ex.Message}");
                return Json(
                    new { success = false, message = "Lỗi khi tải danh sách loại bệnh: " + ex.Message }, 
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        // ============================================================================
        // VACCINE CRUD - Helper methods
        // ============================================================================

        /// <summary>
        /// Validate hình ảnh upload
        /// </summary>
        private Tuple<bool, string> ValidateImage(HttpPostedFileBase imageFile)
        {
            if (imageFile == null)
                return new Tuple<bool, string>(false, "File hình ảnh không hợp lệ");

            // Kiểm tra extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return new Tuple<bool, string>(false, "Chỉ cho phép file ảnh: .jpg, .jpeg, .png, .gif");
            }

            // Kiểm tra kích thước
            if (imageFile.ContentLength > MAX_IMAGE_SIZE)
            {
                return new Tuple<bool, string>(false, "Kích thước ảnh không được vượt quá 5MB");
            }

            return new Tuple<bool, string>(true, "OK");
        }

        /// <summary>
        /// Lưu hình ảnh vaccine
        /// </summary>
        private string SaveVaccineImage(HttpPostedFileBase imageFile)
        {
            try
            {
                // Tạo tên file duy nhất
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string folderPath = Server.MapPath(VACCINE_IMAGE_PATH);

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Lưu file
                string filePath = Path.Combine(folderPath, fileName);
                imageFile.SaveAs(filePath);

                return fileName;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error saving image: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Xóa hình ảnh vaccine
        /// </summary>
        private void DeleteVaccineImage(string imageName)
        {
            try
            {
                if (string.IsNullOrEmpty(imageName))
                    return;

                string filePath = Server.MapPath(VACCINE_IMAGE_PATH + imageName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting image: " + ex.Message);
                // Không throw để không ảnh hưởng đến logic chính
            }
        }

        /// <summary>
        /// Generate MaVC mới (VC00000001, VC00000002, ...)
        /// </summary>
        private string GenerateVaccineMaVC(string lastMaVC)
        {
            if (string.IsNullOrEmpty(lastMaVC))
                return "VC00000001";

            // Extract số từ mã cuối cùng
            string numberPart = lastMaVC.Substring(2); // Lấy phần số (bỏ "VC")
            
            if (int.TryParse(numberPart, out int number))
            {
                number++;
                return "VC" + number.ToString().PadLeft(8, '0');
            }

            return "VC00000001";
        }

        /// <summary>
        /// Lấy lỗi từ ModelState
        /// </summary>
        private Dictionary<string, string> GetModelStateErrors()
        {
            var errors = new Dictionary<string, string>();

            foreach (var state in ModelState.Values)
            {
                foreach (var error in state.Errors)
                {
                    errors.Add(state.ToString(), error.ErrorMessage);
                }
            }

            return errors;
        }

        // ============================================================================
        // PAGES: Customers, Appointments, GoiVaccine, etc.
        // ============================================================================

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

        // ============================================================================
        // NHA CUNG CẤP CRUD - API Methods
        // ============================================================================

        /// <summary>
        /// POST: Admin/CreateNhaCungCap - Tạo nhà cung cấp mới
        /// </summary>
        [HttpPost]
        public ActionResult CreateNhaCungCap(AdminNhaCungCapCreateEditViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Dữ liệu không hợp lệ",
                        errors = GetModelStateErrors()
                    });
                }

                // Generate MaNCC
                var lastNCC = _unitOfWork.NhaCungCaps.Query()
                    .OrderByDescending(n => n.MaNCC)
                    .FirstOrDefault();

                string newMaNCC = GenerateNhaCungCapMa(lastNCC?.MaNCC);

                // Tạo entity NhaCungCap mới
                var nhaCungCap = new NhaCungCap
                {
                    MaNCC = newMaNCC,
                    TenNCC = model.TenNCC,
                    DiaChi = model.DiaChi,
                    Email = model.Email,
                    SoDT = model.SoDT,
                    TenNganHang = model.TenNganHang,
                    SoTK = model.SoTK
                };

                _unitOfWork.NhaCungCaps.Add(nhaCungCap);
                _unitOfWork.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Thêm nhà cung cấp thành công!",
                    data = new { MaNCC = newMaNCC }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error creating nhà cung cấp: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("Stack trace: " + ex.StackTrace);
                return Json(new
                {
                    success = false,
                    message = "Lỗi hệ thống: " + ex.Message
                });
            }
        }

        /// <summary>
        /// GET: Admin/GetNhaCungCap - Lấy thông tin nhà cung cấp để chỉnh sửa
        /// </summary>
        [HttpGet]
        public ActionResult GetNhaCungCap(string id)
        {
            try
            {
                var nhaCungCap = _unitOfWork.NhaCungCaps.GetById(id);
                if (nhaCungCap == null)
                {
                    return Json(new { success = false, message = "Nhà cung cấp không tồn tại" },
                        JsonRequestBehavior.AllowGet);
                }

                var viewModel = new AdminNhaCungCapCreateEditViewModel
                {
                    MaNCC = nhaCungCap.MaNCC,
                    TenNCC = nhaCungCap.TenNCC,
                    DiaChi = nhaCungCap.DiaChi,
                    Email = nhaCungCap.Email,
                    SoDT = nhaCungCap.SoDT,
                    TenNganHang = nhaCungCap.TenNganHang,
                    SoTK = nhaCungCap.SoTK
                };

                return Json(new { success = true, data = viewModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error getting nhà cung cấp: " + ex.Message);
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message },
                    JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// POST: Admin/EditNhaCungCap - Cập nhật nhà cung cấp
        /// </summary>
        [HttpPost]
        public ActionResult EditNhaCungCap(AdminNhaCungCapCreateEditViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.MaNCC))
                {
                    return Json(new { success = false, message = "Mã nhà cung cấp không hợp lệ" });
                }

                var nhaCungCap = _unitOfWork.NhaCungCaps.GetById(model.MaNCC);
                if (nhaCungCap == null)
                {
                    return Json(new { success = false, message = "Nhà cung cấp không tồn tại" });
                }

                // Update các field
                nhaCungCap.TenNCC = model.TenNCC;
                nhaCungCap.DiaChi = model.DiaChi;
                nhaCungCap.Email = model.Email;
                nhaCungCap.SoDT = model.SoDT;
                nhaCungCap.TenNganHang = model.TenNganHang;
                nhaCungCap.SoTK = model.SoTK;

                _unitOfWork.NhaCungCaps.Update(nhaCungCap);
                _unitOfWork.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Cập nhật nhà cung cấp thành công!"
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error updating nhà cung cấp: " + ex.Message);
                return Json(new
                {
                    success = false,
                    message = "Lỗi hệ thống: " + ex.Message
                });
            }
        }

        /// <summary>
        /// POST: Admin/DeleteNhaCungCap - Xóa nhà cung cấp
        /// </summary>
        [HttpPost]
        public ActionResult DeleteNhaCungCap(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Mã nhà cung cấp không hợp lệ"
                    });
                }

                var nhaCungCap = _unitOfWork.NhaCungCaps.GetById(id);
                if (nhaCungCap == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Nhà cung cấp không tồn tại"
                    });
                }

                // Kiểm tra xem có PhieuNhap nào liên quan không
                var hasPhieuNhap = _unitOfWork.PhieuNhapVaccines.Query()
                    .Any(pn => pn.MaNCC == id);

                if (hasPhieuNhap)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Không thể xóa nhà cung cấp này vì đã có phiếu nhập liên quan"
                    });
                }

                _unitOfWork.NhaCungCaps.Remove(nhaCungCap);
                _unitOfWork.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Xóa nhà cung cấp thành công!"
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting nhà cung cấp: " + ex.Message);
                return Json(new
                {
                    success = false,
                    message = "Lỗi hệ thống: " + ex.Message
                });
            }
        }

        /// <summary>
        /// GET: Admin/GetNhaCungCapList - Lấy danh sách nhà cung cấp
        /// </summary>
        [HttpGet]
        public ActionResult GetNhaCungCapList()
        {
            try
            {
                var nhaCungCaps = _unitOfWork.NhaCungCaps.Query()
                    .OrderBy(n => n.TenNCC)
                    .Select(n => new
                    {
                        n.MaNCC,
                        n.TenNCC,
                        n.DiaChi,
                        n.Email,
                        n.SoDT,
                        n.TenNganHang,
                        n.SoTK
                    })
                    .ToList();

                return Json(new { success = true, data = nhaCungCaps }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error getting nhà cung cấp list: " + ex.Message);
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message },
                    JsonRequestBehavior.AllowGet);
            }
        }

        // ============================================================================
        // NHA CUNG CẤP CRUD - Helper methods
        // ============================================================================

        private string GenerateNhaCungCapMa(string lastMaNCC)
        {
            if (string.IsNullOrEmpty(lastMaNCC))
            {
                return "NCC0000001";
            }

            // Giả sử format: NCC + 7 digits (NCC0000001)
            if (lastMaNCC.Length >= 3 && int.TryParse(lastMaNCC.Substring(3), out int number))
            {
                number++;
                return "NCC" + number.ToString("D7");
            }

            return "NCC0000001";
        }

        // GET: Admin/Reports
        public ActionResult Reports()
        {
            // TODO: Hiển thị báo cáo thống kê
            return View();
        }
    }
}
