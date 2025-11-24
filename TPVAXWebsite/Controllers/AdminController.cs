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
        // PAGES: Customers, Appointments, etc.
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

        // GET: Admin/InvoiceDetails
        public ActionResult InvoiceDetails(string id)
        {
            // TODO: Load chi tiết hóa đơn
            return View();
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

        // ============================================================================
        // KHUYẾN MÃI CRUD - View Actions
        // ============================================================================

        /// <summary>
        /// GET: Admin/KhuyenMai - Trang quản lý khuyến mãi
        /// </summary>
        public ActionResult KhuyenMai()
        {
            return View();
        }

        // ============================================================================
        // KHUYẾN MÃI CRUD - API Methods
        // ============================================================================

        /// <summary>
        /// GET: Admin/GetKhuyenMaiList - Lấy danh sách khuyến mãi
        /// </summary>
        [HttpGet]
        public ActionResult GetKhuyenMaiList()
        {
            try
            {
                var khuyenMais = _unitOfWork.KhuyenMais.Query()
                    .ToList()
                    .Select(km => new
                    {
                        km.MaKM,
                        km.TenKM,
                        km.MoTa,
                        km.LoaiKM,
                        km.KieuGiam,
                        km.GiaTriGiam,
                        NgayBatDau = km.NgayBatDau.ToString("dd/MM/yyyy"),
                        NgayKetThuc = km.NgayKetThuc.ToString("dd/MM/yyyy"),
                        km.TrangThai,
                        // Xử lý đường dẫn hình ảnh
                        HinhAnh = string.IsNullOrEmpty(km.HinhAnh)
                            ? null
                            : (km.HinhAnh.StartsWith("/") || km.HinhAnh.StartsWith("http")
                                ? km.HinhAnh
                                : "/Content/images/khuyenmai/" + km.HinhAnh),
                        SoLuongSanPham = km.ChiTietKhuyenMai.Count
                    })
                    .OrderByDescending(km => km.NgayBatDau)
                    .ToList();

                return Json(new { success = true, data = khuyenMais }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error getting khuyến mãi list: " + ex.Message);
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message },
                    JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// POST: Admin/CreateKhuyenMai - Tạo khuyến mãi mới
        /// </summary>
        [HttpPost]
        public ActionResult CreateKhuyenMai(string TenKM, string MoTa, string LoaiKM, string KieuGiam, 
            decimal GiaTriGiam, string NgayBatDau, string NgayKetThuc, bool TrangThai, HttpPostedFileBase HinhAnh)
        {
            try
            {
                if (string.IsNullOrEmpty(TenKM))
                {
                    return Json(new { success = false, message = "Tên khuyến mãi không được để trống" });
                }

                // Generate MaKM
                var lastKM = _unitOfWork.KhuyenMais.Query()
                    .OrderByDescending(km => km.MaKM)
                    .FirstOrDefault();

                string newMaKM = GenerateKhuyenMaiMa(lastKM?.MaKM);

                // Parse dates
                DateTime batDau = DateTime.ParseExact(NgayBatDau, "yyyy-MM-dd", null);
                DateTime ketThuc = DateTime.ParseExact(NgayKetThuc, "yyyy-MM-dd", null);

                if (ketThuc < batDau)
                {
                    return Json(new { success = false, message = "Ngày kết thúc phải sau ngày bắt đầu" });
                }

                // Xử lý upload hình ảnh
                string imagePath = null;
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    if (HinhAnh.ContentLength > 5 * 1024 * 1024)
                    {
                        return Json(new { success = false, message = "Kích thước ảnh không được vượt quá 5MB" });
                    }

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = System.IO.Path.GetExtension(HinhAnh.FileName).ToLower();
                    if (!allowedExtensions.Contains(extension))
                    {
                        return Json(new { success = false, message = "Chỉ chấp nhận file ảnh: .jpg, .jpeg, .png, .gif" });
                    }

                    var fileName = newMaKM + extension;
                    var uploadPath = Server.MapPath("~/Content/images/khuyenmai/");
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    var filePath = System.IO.Path.Combine(uploadPath, fileName);
                    HinhAnh.SaveAs(filePath);
                    imagePath = "/Content/images/khuyenmai/" + fileName;
                }

                var khuyenMai = new KhuyenMai
                {
                    MaKM = newMaKM,
                    TenKM = TenKM,
                    MoTa = MoTa,
                    LoaiKM = LoaiKM,
                    KieuGiam = KieuGiam,
                    GiaTriGiam = GiaTriGiam,
                    NgayBatDau = batDau,
                    NgayKetThuc = ketThuc,
                    TrangThai = TrangThai,
                    HinhAnh = imagePath
                };

                _unitOfWork.KhuyenMais.Add(khuyenMai);
                _unitOfWork.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Thêm khuyến mãi thành công!",
                    data = new { MaKM = newMaKM }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error creating khuyến mãi: " + ex.Message);
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        /// <summary>
        /// GET: Admin/GetKhuyenMai - Lấy thông tin khuyến mãi để chỉnh sửa
        /// </summary>
        [HttpGet]
        public ActionResult GetKhuyenMai(string id)
        {
            try
            {
                var khuyenMai = _unitOfWork.KhuyenMais.Query()
                    .Where(km => km.MaKM == id)
                    .FirstOrDefault();

                if (khuyenMai == null)
                {
                    return Json(new { success = false, message = "Khuyến mãi không tồn tại" },
                        JsonRequestBehavior.AllowGet);
                }

                // Xử lý đường dẫn hình ảnh
                string hinhAnhPath = null;
                if (!string.IsNullOrEmpty(khuyenMai.HinhAnh))
                {
                    hinhAnhPath = khuyenMai.HinhAnh.StartsWith("/") || khuyenMai.HinhAnh.StartsWith("http")
                        ? khuyenMai.HinhAnh
                        : "/Content/images/khuyenmai/" + khuyenMai.HinhAnh;
                }

                var viewModel = new
                {
                    khuyenMai.MaKM,
                    khuyenMai.TenKM,
                    khuyenMai.MoTa,
                    khuyenMai.LoaiKM,
                    khuyenMai.KieuGiam,
                    khuyenMai.GiaTriGiam,
                    NgayBatDau = khuyenMai.NgayBatDau.ToString("yyyy-MM-dd"),
                    NgayKetThuc = khuyenMai.NgayKetThuc.ToString("yyyy-MM-dd"),
                    khuyenMai.TrangThai,
                    HinhAnh = hinhAnhPath
                };

                return Json(new { success = true, data = viewModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error getting khuyến mãi: " + ex.Message);
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message },
                    JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// POST: Admin/EditKhuyenMai - Cập nhật khuyến mãi
        /// </summary>
        [HttpPost]
        public ActionResult EditKhuyenMai(string MaKM, string TenKM, string MoTa, string LoaiKM, string KieuGiam,
            decimal GiaTriGiam, string NgayBatDau, string NgayKetThuc, bool TrangThai, HttpPostedFileBase HinhAnh)
        {
            try
            {
                if (string.IsNullOrEmpty(MaKM))
                {
                    return Json(new { success = false, message = "Mã khuyến mãi không hợp lệ" });
                }

                var khuyenMai = _unitOfWork.KhuyenMais.GetById(MaKM);
                if (khuyenMai == null)
                {
                    return Json(new { success = false, message = "Khuyến mãi không tồn tại" });
                }

                // Parse dates
                DateTime batDau = DateTime.ParseExact(NgayBatDau, "yyyy-MM-dd", null);
                DateTime ketThuc = DateTime.ParseExact(NgayKetThuc, "yyyy-MM-dd", null);

                if (ketThuc < batDau)
                {
                    return Json(new { success = false, message = "Ngày kết thúc phải sau ngày bắt đầu" });
                }

                // Xử lý upload hình ảnh mới
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    if (HinhAnh.ContentLength > 5 * 1024 * 1024)
                    {
                        return Json(new { success = false, message = "Kích thước ảnh không được vượt quá 5MB" });
                    }

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = System.IO.Path.GetExtension(HinhAnh.FileName).ToLower();
                    if (!allowedExtensions.Contains(extension))
                    {
                        return Json(new { success = false, message = "Chỉ chấp nhận file ảnh: .jpg, .jpeg, .png, .gif" });
                    }

                    // Xóa ảnh cũ nếu có
                    if (!string.IsNullOrEmpty(khuyenMai.HinhAnh))
                    {
                        var oldImagePath = Server.MapPath("~" + khuyenMai.HinhAnh);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var fileName = MaKM + extension;
                    var uploadPath = Server.MapPath("~/Content/images/khuyenmai/");
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    var filePath = System.IO.Path.Combine(uploadPath, fileName);
                    HinhAnh.SaveAs(filePath);
                    khuyenMai.HinhAnh = "/Content/images/khuyenmai/" + fileName;
                }

                khuyenMai.TenKM = TenKM;
                khuyenMai.MoTa = MoTa;
                khuyenMai.LoaiKM = LoaiKM;
                khuyenMai.KieuGiam = KieuGiam;
                khuyenMai.GiaTriGiam = GiaTriGiam;
                khuyenMai.NgayBatDau = batDau;
                khuyenMai.NgayKetThuc = ketThuc;
                khuyenMai.TrangThai = TrangThai;

                _unitOfWork.KhuyenMais.Update(khuyenMai);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Cập nhật khuyến mãi thành công!" });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error editing khuyến mãi: " + ex.Message);
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        /// <summary>
        /// POST: Admin/DeleteKhuyenMai - Xóa khuyến mãi
        /// </summary>
        [HttpPost]
        public ActionResult DeleteKhuyenMai(string id)
        {
            try
            {
                var khuyenMai = _unitOfWork.KhuyenMais.GetById(id);
                if (khuyenMai == null)
                {
                    return Json(new { success = false, message = "Khuyến mãi không tồn tại" });
                }

                // Kiểm tra xem có hóa đơn nào sử dụng khuyến mãi này không
                var hasHoaDon = _unitOfWork.HoaDons.Query()
                    .Any(hd => hd.MaKM == id);

                if (hasHoaDon)
                {
                    return Json(new { success = false, message = "Không thể xóa khuyến mãi đã được sử dụng trong hóa đơn" });
                }

                // Xóa chi tiết khuyến mãi trước
                var chiTiets = _unitOfWork.ChiTietKhuyenMais.Query()
                    .Where(ct => ct.MaKM == id)
                    .ToList();

                foreach (var ct in chiTiets)
                {
                    _unitOfWork.ChiTietKhuyenMais.Remove(ct);
                }

                // Xóa hình ảnh nếu có
                if (!string.IsNullOrEmpty(khuyenMai.HinhAnh))
                {
                    var imagePath = Server.MapPath("~" + khuyenMai.HinhAnh);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _unitOfWork.KhuyenMais.Remove(khuyenMai);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Xóa khuyến mãi thành công!" });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting khuyến mãi: " + ex.Message);
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // ============================================================================
        // KHUYẾN MÃI CRUD - Helper Methods
        // ============================================================================

        private string GenerateKhuyenMaiMa(string lastMaKM)
        {
            if (string.IsNullOrEmpty(lastMaKM))
            {
                return "KM00000001";
            }

            if (lastMaKM.Length >= 2 && int.TryParse(lastMaKM.Substring(2), out int number))
            {
                number++;
                return "KM" + number.ToString("D8");
            }

            return "KM00000001";
        }
    }
}