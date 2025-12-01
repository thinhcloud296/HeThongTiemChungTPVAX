using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;
using TPVAXWebsite.Common;
using System.Data.Entity;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý tất cả chức năng Admin
    /// Yêu cầu đăng nhập với tài khoản Nhân viên
    /// </summary>
    [AuthorizeAdmin]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string VACCINE_IMAGE_PATH = "~/Content/Images/vaccines/";
        private const string KHUYENMAI_IMAGE_PATH = "~/Content/images/khuyenmai/";
        private const int MAX_IMAGE_SIZE = 5 * 1024 * 1024; // 5MB
        private static readonly string[] ALLOWED_IMAGE_EXTENSIONS = { ".jpg", ".jpeg", ".png", ".gif" };

        public AdminController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // ============================================================================
        // DASHBOARD
        // ============================================================================

        // GET: Admin/TestDatabase - Test connection và hiển thị dữ liệu thô
        public ActionResult TestDatabase()
        {
            try
            {
                var result = new System.Text.StringBuilder();
                result.AppendLine("=== DATABASE CONNECTION TEST ===\n");
                
                // Test connection
                result.AppendLine($"Connection String: {_unitOfWork.GetType().Name}");
                result.AppendLine($"Context Type: {System.Configuration.ConfigurationManager.ConnectionStrings["TPVAXConnection"]?.ConnectionString}\n");
                
                // Count tất cả bảng
                var vaccineCount = _unitOfWork.Vaccines.Query().Count();
                result.AppendLine($"Vaccine Count: {vaccineCount}");
                
                var customerCount = _unitOfWork.KhachHangs.Query().Count();
                result.AppendLine($"KhachHang Count: {customerCount}");
                
                var appointmentCount = _unitOfWork.LichTiems.Query().Count();
                result.AppendLine($"LichTiem Count: {appointmentCount}");
                
                var invoiceCount = _unitOfWork.HoaDons.Query().Count();
                result.AppendLine($"HoaDon Count: {invoiceCount}\n");
                
                // Lấy sample data
                if (vaccineCount > 0)
                {
                    result.AppendLine("Sample Vaccines:");
                    var vaccines = _unitOfWork.Vaccines.Query().Take(5).ToList();
                    foreach (var v in vaccines)
                    {
                        result.AppendLine($"  - {v.MaVC}: {v.TenVC}, Số lượng: {v.SoLuong}, Giá: {v.GiaBan:N0}đ");
                    }
                    result.AppendLine();
                }
                
                if (customerCount > 0)
                {
                    result.AppendLine("Sample Khách hàng:");
                    var customers = _unitOfWork.KhachHangs.Query().Take(5).ToList();
                    foreach (var k in customers)
                    {
                        result.AppendLine($"  - {k.MaKH}: {k.HoTen}, SĐT: {k.SoDT}");
                    }
                    result.AppendLine();
                }
                
                if (appointmentCount > 0)
                {
                    result.AppendLine("Sample Lịch tiêm:");
                    var appointments = _unitOfWork.LichTiems.Query()
                        .OrderByDescending(lt => lt.NgayHenTiem)
                        .Take(5)
                        .ToList();
                    foreach (var lt in appointments)
                    {
                        result.AppendLine($"  - {lt.MaLT}: Ngày hẹn: {lt.NgayHenTiem:dd/MM/yyyy HH:mm}, Trạng thái: {lt.TrangThai}");
                    }
                    result.AppendLine();
                }
                
                if (invoiceCount > 0)
                {
                    result.AppendLine("Sample Hóa đơn:");
                    var invoices = _unitOfWork.HoaDons.Query()
                        .OrderByDescending(hd => hd.NgayLap)
                        .Take(5)
                        .ToList();
                    foreach (var hd in invoices)
                    {
                        result.AppendLine($"  - {hd.MaHD}: Ngày: {hd.NgayLap:dd/MM/yyyy}, Tổng: {hd.TongTien:N0}đ, Trạng thái: {(hd.TrangThai == true ? "Đã thanh toán" : "Chưa thanh toán")}");
                    }
                    result.AppendLine();
                }
                
                result.AppendLine("=== TEST COMPLETED ===");
                
                return Content(result.ToString(), "text/plain");
            }
            catch (Exception ex)
            {
                var error = $"ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}";
                if (ex.InnerException != null)
                {
                    error += $"\n\nInner Exception:\n{ex.InnerException.Message}";
                }
                return Content(error, "text/plain");
            }
        }

        // GET: Admin/Index
        public ActionResult Index()
        {
            try
            {
                // Kiểm tra _unitOfWork có null không
                if (_unitOfWork == null)
                {
                    throw new Exception("UnitOfWork is not initialized");
                }
                
                // Test query trực tiếp
                var vaccineList = _unitOfWork.Vaccines.GetAll();
                
                // Tính toán thống kê cho dashboard - Sử dụng GetAll().Count() thay vì Query().Count()
                var totalVaccines = _unitOfWork.Vaccines.GetAll().Count();
                
                var totalCustomers = _unitOfWork.KhachHangs.GetAll().Count();
                
                // Lịch tiêm hôm nay
                var today = DateTime.Today;
                var allAppointments = _unitOfWork.LichTiems.Query()
                    .Include(lt => lt.HoSoTiemChung)
                    .ToList();
                
                var appointmentsToday = allAppointments
                    .Where(lt => lt.NgayHenTiem.Date == today)
                    .Count();
                
                // Doanh thu tháng này
                var currentMonth = DateTime.Now.Month;
                var currentYear = DateTime.Now.Year;
                var allInvoices = _unitOfWork.HoaDons.GetAll().ToList();
                
                var monthlyRevenue = allInvoices
                    .Where(hd => hd.NgayLap.Month == currentMonth && 
                                 hd.NgayLap.Year == currentYear &&
                                 hd.TrangThai == true)
                    .Sum(hd => (decimal?)hd.TongTien) ?? 0;
                
                // Vaccine sắp hết hàng (dưới 100)
                var lowStockVaccines = vaccineList
                    .Where(v => v.SoLuong < 100)
                    .OrderBy(v => v.SoLuong)
                    .Take(5)
                    .ToList(); // Không select, giữ nguyên kiểu Vaccine
                
                // Lịch tiêm gần đây - lấy tất cả lịch có ngày hẹn >= hiện tại
                var upcomingAppointments = allAppointments
                    .Where(lt => lt.NgayHenTiem >= DateTime.Now && 
                                 (lt.TrangThai == "Đã đặt" || lt.TrangThai == "Chưa tiêm"))
                    .OrderBy(lt => lt.NgayHenTiem)
                    .Take(5)
                    .ToList(); // Không select, giữ nguyên kiểu LichTiem
                
                // Doanh thu 6 tháng gần nhất
                var sixMonthsAgo = DateTime.Now.AddMonths(-5);
                var revenueByMonth = allInvoices
                    .Where(hd => hd.NgayLap >= sixMonthsAgo && hd.TrangThai == true)
                    .GroupBy(hd => new { hd.NgayLap.Year, hd.NgayLap.Month })
                    .Select(g => new
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        TotalRevenue = g.Sum(hd => hd.TongTien)
                    })
                    .OrderBy(r => r.Year).ThenBy(r => r.Month)
                    .Cast<object>() // Cast sang object để tránh lỗi serialization
                    .ToList();
                
                // Truyền dữ liệu qua ViewBag
                ViewBag.TotalVaccines = totalVaccines;
                ViewBag.TotalCustomers = totalCustomers;
                ViewBag.AppointmentsToday = appointmentsToday;
                ViewBag.MonthlyRevenue = monthlyRevenue;
                ViewBag.LowStockVaccines = lowStockVaccines;
                ViewBag.UpcomingAppointments = upcomingAppointments;
                ViewBag.RevenueByMonth = revenueByMonth;
                
                return View();
            }
            catch (Exception ex)
            {
                
                // Set default values nếu có lỗi
                ViewBag.TotalVaccines = 0;
                ViewBag.TotalCustomers = 0;
                ViewBag.AppointmentsToday = 0;
                ViewBag.MonthlyRevenue = 0;
                ViewBag.LowStockVaccines = new List<object>();
                ViewBag.UpcomingAppointments = new List<object>();
                ViewBag.RevenueByMonth = new List<object>();
                ViewBag.ErrorMessage = ex.Message;
                
                return View();
            }
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
                // Validate ModelState
                if (!ModelState.IsValid)
                {
                    var errors = GetModelStateErrors();
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
                        // Error parsing
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
                        // Error parsing
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
        /// Validate hình ảnh upload (dùng chung cho vaccine và khuyến mãi)
        /// </summary>
        private Tuple<bool, string> ValidateImage(HttpPostedFileBase imageFile)
        {
            if (imageFile == null)
                return new Tuple<bool, string>(false, "File hình ảnh không hợp lệ");

            // Kiểm tra extension
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

            if (!ALLOWED_IMAGE_EXTENSIONS.Contains(fileExtension))
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
        public ActionResult Reports(string reportType = "revenue", DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                // Mặc định lấy 6 tháng gần nhất
                var defaultEndDate = DateTime.Now.Date;
                var defaultStartDate = DateTime.Now.AddMonths(-5).Date;
                
                startDate = startDate ?? defaultStartDate;
                endDate = endDate ?? defaultEndDate;
                
                ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");
                ViewBag.ReportType = reportType;

                // Khởi tạo mặc định tất cả ViewBag với kiểu cụ thể
                ViewBag.VaccineRevenue = new List<VaccineRevenueReportItem>();
                ViewBag.RevenueByMonth = new List<RevenueByMonthItem>();
                ViewBag.InventoryData = new List<InventoryReportItem>();
                ViewBag.InventoryByCategory = new List<InventoryByCategoryItem>();
                ViewBag.VaccinationByVaccine = new List<VaccinationByVaccineItem>();
                ViewBag.VaccinationByMonth = new List<VaccinationByMonthItem>();
                ViewBag.AppointmentStatus = new List<AppointmentStatusItem>();
                ViewBag.AppointmentTrends = new List<VaccinationByMonthItem>();
                ViewBag.TotalRevenue = 0m;
                ViewBag.TotalOrders = 0;
                ViewBag.TotalInventoryValue = 0m;
                ViewBag.TotalVaccineTypes = 0;
                ViewBag.LowStockCount = 0;
                ViewBag.OutOfStockCount = 0;
                ViewBag.TotalVaccinations = 0;

                // ============ BÁO CÁO DOANH THU ============
                if (reportType == "revenue")
                {
                    // Doanh thu theo vaccine
                    var revenueByVaccine = _unitOfWork.ChiTietHoaDons.Query()
                        .Include(ct => ct.HoaDon)
                        .Where(ct => ct.HoaDon.TrangThai == true && 
                                     ct.LoaiSanPham == "VACCINE" &&
                                     ct.HoaDon.NgayLap >= startDate && 
                                     ct.HoaDon.NgayLap <= endDate)
                        .GroupBy(ct => ct.MaSanPham)
                        .Select(g => new
                        {
                            MaVC = g.Key,
                            SoLuotTiem = g.Sum(ct => ct.SoLuong),
                            TongDoanhThu = g.Sum(ct => ct.SoLuong * ct.DonGia)
                        })
                        .ToList();

                    var vaccineRevenue = revenueByVaccine.Select(r => new VaccineRevenueReportItem
                    {
                        MaVC = r.MaVC,
                        TenVC = _unitOfWork.Vaccines.GetById(r.MaVC)?.TenVC ?? "N/A",
                        SoLuotTiem = r.SoLuotTiem,
                        TongDoanhThu = r.TongDoanhThu
                    }).OrderByDescending(r => r.TongDoanhThu).ToList();

                    ViewBag.VaccineRevenue = vaccineRevenue;

                    // Doanh thu theo tháng
                    var revenueByMonth = _unitOfWork.HoaDons.Query()
                        .Where(hd => hd.TrangThai == true && 
                                     hd.NgayLap >= startDate && 
                                     hd.NgayLap <= endDate)
                        .ToList()
                        .GroupBy(hd => new { hd.NgayLap.Year, hd.NgayLap.Month })
                        .Select(g => new RevenueByMonthItem
                        {
                            Year = g.Key.Year,
                            Month = g.Key.Month,
                            TongDoanhThu = g.Sum(hd => hd.TongTien),
                            SoHoaDon = g.Count()
                        })
                        .OrderBy(r => r.Year).ThenBy(r => r.Month)
                        .ToList();

                    ViewBag.RevenueByMonth = revenueByMonth;

                    // Tổng doanh thu
                    ViewBag.TotalRevenue = vaccineRevenue.Sum(v => v.TongDoanhThu);
                    ViewBag.TotalOrders = revenueByMonth.Sum(r => r.SoHoaDon);
                }
                // ============ BÁO CÁO TỒN KHO ============
                else if (reportType == "inventory")
                {
                    // Vaccine tồn kho
                    var inventoryData = _unitOfWork.Vaccines.Query()
                        .Include(v => v.LoaiVaccine)
                        .ToList()
                        .Select(v => new InventoryReportItem
                        {
                            MaVC = v.MaVC,
                            TenVC = v.TenVC,
                            TenLoai = v.LoaiVaccine?.TenLoai ?? "Chưa phân loại",
                            SoLuongTon = v.SoLuong,
                            GiaBan = v.GiaBan,
                            GiaTriTonKho = v.SoLuong * v.GiaBan,
                            TrangThai = v.SoLuong == 0 ? "Hết hàng" : (v.SoLuong < 50 ? "Sắp hết" : "Còn hàng")
                        })
                        .OrderBy(v => v.SoLuongTon)
                        .ToList();

                    ViewBag.InventoryData = inventoryData;
                    ViewBag.TotalInventoryValue = inventoryData.Sum(v => v.GiaTriTonKho);
                    ViewBag.TotalVaccineTypes = inventoryData.Count;
                    ViewBag.LowStockCount = inventoryData.Count(v => v.TrangThai == "Sắp hết");
                    ViewBag.OutOfStockCount = inventoryData.Count(v => v.TrangThai == "Hết hàng");

                    // Thống kê theo loại vaccine
                    var inventoryByCategory = inventoryData
                        .GroupBy(v => v.TenLoai)
                        .Select(g => new InventoryByCategoryItem
                        {
                            TenLoai = g.Key,
                            SoLuong = g.Count(),
                            TongTonKho = g.Sum(v => v.SoLuongTon),
                            GiaTriTonKho = g.Sum(v => v.GiaTriTonKho)
                        })
                        .OrderByDescending(g => g.GiaTriTonKho)
                        .ToList();

                    ViewBag.InventoryByCategory = inventoryByCategory;
                }
                // ============ BÁO CÁO LƯỢT TIÊM ============
                else if (reportType == "vaccination")
                {
                    // Lượt tiêm theo vaccine
                    var vaccinationByVaccine = _unitOfWork.LichTiems.Query()
                        .Include(lt => lt.Vaccine)
                        .Where(lt => lt.TrangThai == "Đã tiêm" && 
                                     lt.NgayTiemThucTe >= startDate && 
                                     lt.NgayTiemThucTe <= endDate)
                        .ToList()
                        .GroupBy(lt => lt.MaVC)
                        .Select(g => new VaccinationByVaccineItem
                        {
                            MaVC = g.Key,
                            TenVC = g.First().Vaccine?.TenVC ?? "N/A",
                            SoLuotTiem = g.Count()
                        })
                        .OrderByDescending(v => v.SoLuotTiem)
                        .ToList();

                    ViewBag.VaccinationByVaccine = vaccinationByVaccine;

                    // Lượt tiêm theo tháng
                    var vaccinationByMonth = _unitOfWork.LichTiems.Query()
                        .Where(lt => lt.TrangThai == "Đã tiêm" && 
                                     lt.NgayTiemThucTe >= startDate && 
                                     lt.NgayTiemThucTe <= endDate)
                        .ToList()
                        .GroupBy(lt => new { lt.NgayTiemThucTe.Value.Year, lt.NgayTiemThucTe.Value.Month })
                        .Select(g => new VaccinationByMonthItem
                        {
                            Year = g.Key.Year,
                            Month = g.Key.Month,
                            SoLuotTiem = g.Count()
                        })
                        .OrderBy(r => r.Year).ThenBy(r => r.Month)
                        .ToList();

                    ViewBag.VaccinationByMonth = vaccinationByMonth;

                    // Thống kê trạng thái lịch tiêm
                    var appointmentStatus = _unitOfWork.LichTiems.Query()
                        .Where(lt => lt.NgayHenTiem >= startDate && lt.NgayHenTiem <= endDate)
                        .ToList()
                        .GroupBy(lt => lt.TrangThai)
                        .Select(g => new AppointmentStatusItem
                        {
                            TrangThai = g.Key,
                            SoLuong = g.Count()
                        })
                        .ToList();

                    ViewBag.AppointmentStatus = appointmentStatus;
                    ViewBag.TotalVaccinations = vaccinationByVaccine.Sum(v => v.SoLuotTiem);
                }

                // Xu hướng tiêm chủng 6 tháng gần nhất (dùng chung cho tất cả báo cáo)
                var appointmentTrends = _unitOfWork.LichTiems.Query()
                    .Where(lt => lt.NgayTiemThucTe >= startDate && lt.TrangThai == "Đã tiêm")
                    .ToList()
                    .GroupBy(lt => new { lt.NgayTiemThucTe.Value.Year, lt.NgayTiemThucTe.Value.Month })
                    .Select(g => new VaccinationByMonthItem
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        SoLuotTiem = g.Count()
                    })
                    .OrderBy(r => r.Year).ThenBy(r => r.Month)
                    .ToList();

                ViewBag.AppointmentTrends = appointmentTrends;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ReportType = reportType ?? "revenue";
                ViewBag.StartDate = DateTime.Now.AddMonths(-5).ToString("yyyy-MM-dd");
                ViewBag.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.VaccineRevenue = new List<VaccineRevenueReportItem>();
                ViewBag.AppointmentTrends = new List<VaccinationByMonthItem>();
                ViewBag.InventoryData = new List<InventoryReportItem>();
                ViewBag.InventoryByCategory = new List<InventoryByCategoryItem>();
                ViewBag.VaccinationByVaccine = new List<VaccinationByVaccineItem>();
                ViewBag.VaccinationByMonth = new List<VaccinationByMonthItem>();
                ViewBag.AppointmentStatus = new List<AppointmentStatusItem>();
                ViewBag.RevenueByMonth = new List<RevenueByMonthItem>();
                ViewBag.TotalRevenue = 0m;
                ViewBag.TotalOrders = 0;
                ViewBag.TotalInventoryValue = 0m;
                ViewBag.TotalVaccineTypes = 0;
                ViewBag.LowStockCount = 0;
                ViewBag.OutOfStockCount = 0;
                ViewBag.TotalVaccinations = 0;
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // API: Lấy dữ liệu báo cáo theo AJAX
        [HttpGet]
        public ActionResult GetReportData(string reportType, string startDate, string endDate)
        {
            try
            {
                DateTime start = DateTime.Parse(startDate);
                DateTime end = DateTime.Parse(endDate);

                if (reportType == "revenue")
                {
                    var revenueByVaccine = _unitOfWork.ChiTietHoaDons.Query()
                        .Include(ct => ct.HoaDon)
                        .Where(ct => ct.HoaDon.TrangThai == true && 
                                     ct.LoaiSanPham == "VACCINE" &&
                                     ct.HoaDon.NgayLap >= start && 
                                     ct.HoaDon.NgayLap <= end)
                        .GroupBy(ct => ct.MaSanPham)
                        .Select(g => new
                        {
                            MaVC = g.Key,
                            SoLuotTiem = g.Sum(ct => ct.SoLuong),
                            TongDoanhThu = g.Sum(ct => ct.SoLuong * ct.DonGia)
                        })
                        .ToList();

                    var vaccineRevenue = revenueByVaccine.Select(r => new
                    {
                        MaVC = r.MaVC,
                        TenVC = _unitOfWork.Vaccines.GetById(r.MaVC)?.TenVC ?? "N/A",
                        SoLuotTiem = r.SoLuotTiem,
                        TongDoanhThu = r.TongDoanhThu
                    }).OrderByDescending(r => r.TongDoanhThu).ToList();

                    var revenueByMonth = _unitOfWork.HoaDons.Query()
                        .Where(hd => hd.TrangThai == true && 
                                     hd.NgayLap >= start && 
                                     hd.NgayLap <= end)
                        .ToList()
                        .GroupBy(hd => new { hd.NgayLap.Year, hd.NgayLap.Month })
                        .Select(g => new
                        {
                            Year = g.Key.Year,
                            Month = g.Key.Month,
                            TongDoanhThu = g.Sum(hd => hd.TongTien),
                            SoHoaDon = g.Count()
                        })
                        .OrderBy(r => r.Year).ThenBy(r => r.Month)
                        .ToList();

                    return Json(new
                    {
                        success = true,
                        vaccineRevenue = vaccineRevenue,
                        revenueByMonth = revenueByMonth,
                        totalRevenue = vaccineRevenue.Sum(v => v.TongDoanhThu),
                        totalOrders = revenueByMonth.Sum(r => r.SoHoaDon)
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (reportType == "inventory")
                {
                    var inventoryData = _unitOfWork.Vaccines.Query()
                        .Include(v => v.LoaiVaccine)
                        .ToList()
                        .Select(v => new
                        {
                            MaVC = v.MaVC,
                            TenVC = v.TenVC,
                            TenLoai = v.LoaiVaccine?.TenLoai ?? "Chưa phân loại",
                            SoLuongTon = v.SoLuong,
                            GiaBan = v.GiaBan,
                            GiaTriTonKho = v.SoLuong * v.GiaBan,
                            TrangThai = v.SoLuong == 0 ? "Hết hàng" : (v.SoLuong < 50 ? "Sắp hết" : "Còn hàng")
                        })
                        .OrderBy(v => v.SoLuongTon)
                        .ToList();

                    var inventoryByCategory = inventoryData
                        .GroupBy(v => v.TenLoai)
                        .Select(g => new
                        {
                            TenLoai = g.Key,
                            SoLuong = g.Count(),
                            TongTonKho = g.Sum(v => v.SoLuongTon),
                            GiaTriTonKho = g.Sum(v => v.GiaTriTonKho)
                        })
                        .OrderByDescending(g => g.GiaTriTonKho)
                        .ToList();

                    return Json(new
                    {
                        success = true,
                        inventoryData = inventoryData,
                        inventoryByCategory = inventoryByCategory,
                        totalInventoryValue = inventoryData.Sum(v => v.GiaTriTonKho),
                        totalVaccineTypes = inventoryData.Count,
                        lowStockCount = inventoryData.Count(v => v.TrangThai == "Sắp hết"),
                        outOfStockCount = inventoryData.Count(v => v.TrangThai == "Hết hàng")
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (reportType == "vaccination")
                {
                    var vaccinationByVaccine = _unitOfWork.LichTiems.Query()
                        .Include(lt => lt.Vaccine)
                        .Where(lt => lt.TrangThai == "Đã tiêm" && 
                                     lt.NgayTiemThucTe >= start && 
                                     lt.NgayTiemThucTe <= end)
                        .ToList()
                        .GroupBy(lt => lt.MaVC)
                        .Select(g => new
                        {
                            MaVC = g.Key,
                            TenVC = g.First().Vaccine?.TenVC ?? "N/A",
                            SoLuotTiem = g.Count()
                        })
                        .OrderByDescending(v => v.SoLuotTiem)
                        .ToList();

                    var vaccinationByMonth = _unitOfWork.LichTiems.Query()
                        .Where(lt => lt.TrangThai == "Đã tiêm" && 
                                     lt.NgayTiemThucTe >= start && 
                                     lt.NgayTiemThucTe <= end)
                        .ToList()
                        .GroupBy(lt => new { lt.NgayTiemThucTe.Value.Year, lt.NgayTiemThucTe.Value.Month })
                        .Select(g => new
                        {
                            Year = g.Key.Year,
                            Month = g.Key.Month,
                            SoLuotTiem = g.Count()
                        })
                        .OrderBy(r => r.Year).ThenBy(r => r.Month)
                        .ToList();

                    var appointmentStatus = _unitOfWork.LichTiems.Query()
                        .Where(lt => lt.NgayHenTiem >= start && lt.NgayHenTiem <= end)
                        .ToList()
                        .GroupBy(lt => lt.TrangThai)
                        .Select(g => new
                        {
                            TrangThai = g.Key,
                            SoLuong = g.Count()
                        })
                        .ToList();

                    return Json(new
                    {
                        success = true,
                        vaccinationByVaccine = vaccinationByVaccine,
                        vaccinationByMonth = vaccinationByMonth,
                        appointmentStatus = appointmentStatus,
                        totalVaccinations = vaccinationByVaccine.Sum(v => v.SoLuotTiem)
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false, message = "Loại báo cáo không hợp lệ" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // API: Export báo cáo ra Excel
        [HttpGet]
        public ActionResult ExportReport(string reportType, string startDate, string endDate)
        {
            try
            {
                DateTime start = DateTime.Parse(startDate);
                DateTime end = DateTime.Parse(endDate);

                var csv = new System.Text.StringBuilder();
                csv.AppendLine("Báo cáo " + (reportType == "revenue" ? "Doanh thu" : reportType == "inventory" ? "Tồn kho" : "Lượt tiêm"));
                csv.AppendLine("Từ ngày: " + start.ToString("dd/MM/yyyy") + " - Đến ngày: " + end.ToString("dd/MM/yyyy"));
                csv.AppendLine("");

                if (reportType == "revenue")
                {
                    csv.AppendLine("Mã Vắc xin,Tên Vắc xin,Số lượt tiêm,Tổng doanh thu (VNĐ)");
                    
                    var revenueByVaccine = _unitOfWork.ChiTietHoaDons.Query()
                        .Include(ct => ct.HoaDon)
                        .Where(ct => ct.HoaDon.TrangThai == true && 
                                     ct.LoaiSanPham == "VACCINE" &&
                                     ct.HoaDon.NgayLap >= start && 
                                     ct.HoaDon.NgayLap <= end)
                        .GroupBy(ct => ct.MaSanPham)
                        .Select(g => new
                        {
                            MaVC = g.Key,
                            SoLuotTiem = g.Sum(ct => ct.SoLuong),
                            TongDoanhThu = g.Sum(ct => ct.SoLuong * ct.DonGia)
                        })
                        .ToList();

                    foreach (var item in revenueByVaccine)
                    {
                        var tenVC = _unitOfWork.Vaccines.GetById(item.MaVC)?.TenVC ?? "N/A";
                        csv.AppendLine($"{item.MaVC},{tenVC},{item.SoLuotTiem},{item.TongDoanhThu:N0}");
                    }

                    csv.AppendLine("");
                    csv.AppendLine($"Tổng cộng,,{revenueByVaccine.Sum(r => r.SoLuotTiem)},{revenueByVaccine.Sum(r => r.TongDoanhThu):N0}");
                }
                else if (reportType == "inventory")
                {
                    csv.AppendLine("Mã Vắc xin,Tên Vắc xin,Loại,Số lượng tồn,Giá bán,Giá trị tồn kho,Trạng thái");
                    
                    var inventoryData = _unitOfWork.Vaccines.Query()
                        .Include(v => v.LoaiVaccine)
                        .ToList();

                    foreach (var v in inventoryData)
                    {
                        var trangThai = v.SoLuong == 0 ? "Hết hàng" : (v.SoLuong < 50 ? "Sắp hết" : "Còn hàng");
                        csv.AppendLine($"{v.MaVC},{v.TenVC},{v.LoaiVaccine?.TenLoai ?? "Chưa phân loại"},{v.SoLuong},{v.GiaBan:N0},{v.SoLuong * v.GiaBan:N0},{trangThai}");
                    }

                    csv.AppendLine("");
                    csv.AppendLine($"Tổng giá trị tồn kho,,,,,{inventoryData.Sum(v => v.SoLuong * v.GiaBan):N0},");
                }
                else if (reportType == "vaccination")
                {
                    csv.AppendLine("Mã Vắc xin,Tên Vắc xin,Số lượt tiêm");
                    
                    var vaccinationByVaccine = _unitOfWork.LichTiems.Query()
                        .Include(lt => lt.Vaccine)
                        .Where(lt => lt.TrangThai == "Đã tiêm" && 
                                     lt.NgayTiemThucTe >= start && 
                                     lt.NgayTiemThucTe <= end)
                        .ToList()
                        .GroupBy(lt => lt.MaVC)
                        .Select(g => new
                        {
                            MaVC = g.Key,
                            TenVC = g.First().Vaccine?.TenVC ?? "N/A",
                            SoLuotTiem = g.Count()
                        })
                        .OrderByDescending(v => v.SoLuotTiem)
                        .ToList();

                    foreach (var item in vaccinationByVaccine)
                    {
                        csv.AppendLine($"{item.MaVC},{item.TenVC},{item.SoLuotTiem}");
                    }

                    csv.AppendLine("");
                    csv.AppendLine($"Tổng cộng,,{vaccinationByVaccine.Sum(v => v.SoLuotTiem)}");
                }

                var bytes = System.Text.Encoding.UTF8.GetBytes(csv.ToString());
                var bom = new byte[] { 0xEF, 0xBB, 0xBF };
                var result = bom.Concat(bytes).ToArray();

                return File(result, "text/csv", $"BaoCao_{reportType}_{DateTime.Now:yyyyMMdd}.csv");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // ============================================================================
        // GÓI VACCINE CRUD
        // ============================================================================

        /// <summary>
        /// GET: Admin/GoiVaccine - Trang quản lý gói vaccine
        /// </summary>
        public ActionResult GoiVaccine()
        {
            return View(new List<AdminGoiVaccineViewModel>());
        }

        /// <summary>
        /// GET: Admin/GetGoiVaccineList - Lấy danh sách gói vaccine
        /// </summary>
        [HttpGet]
        public ActionResult GetGoiVaccineList()
        {
            try
            {
                var goiList = _unitOfWork.GoiVaccines.Query()
                    .Include(g => g.ChiTietGoiVaccine)
                    .ToList()
                    .Select(g => new
                    {
                        MaGoi = g.MaGoi,
                        TenGoi = g.TenGoi,
                        MoTa = g.MoTa,
                        DoiTuongApDung = g.DoiTuongApDung,
                        GiaGoi = g.GiaGoi,
                        TrangThai = g.TrangThai,
                        HinhAnh = !string.IsNullOrEmpty(g.HinhAnh) ? (g.HinhAnh.StartsWith("/") ? g.HinhAnh : "/Content/images/goivaccine/" + g.HinhAnh) : "",
                        SoLuongVaccine = g.ChiTietGoiVaccine?.Count ?? 0
                    })
                    .ToList();

                return Json(new { success = true, data = goiList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// GET: Admin/GetGoiVaccine - Lấy thông tin gói vaccine
        /// </summary>
        [HttpGet]
        public ActionResult GetGoiVaccine(string id)
        {
            try
            {
                var goi = _unitOfWork.GoiVaccines.Query()
                    .Include(g => g.ChiTietGoiVaccine.Select(ct => ct.Vaccine))
                    .FirstOrDefault(g => g.MaGoi == id);

                if (goi == null)
                {
                    return Json(new { success = false, message = "Gói vaccine không tồn tại" }, JsonRequestBehavior.AllowGet);
                }

                var chiTietList = new List<object>();
                if (goi.ChiTietGoiVaccine != null)
                {
                    chiTietList = goi.ChiTietGoiVaccine.Select(ct => new
                    {
                        MaCTGoi = ct.MaCTGoi,
                        MaVC = ct.MaVC,
                        TenVC = ct.Vaccine?.TenVC ?? "N/A",
                        GiaVC = ct.Vaccine?.GiaBan ?? 0,
                        SoMui = ct.SoMui ?? 1,
                        GhiChu = ct.GhiChu
                    }).Cast<object>().ToList();
                }

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        MaGoi = goi.MaGoi,
                        TenGoi = goi.TenGoi,
                        MoTa = goi.MoTa,
                        DoiTuongApDung = goi.DoiTuongApDung,
                        GiaGoi = goi.GiaGoi,
                        TrangThai = goi.TrangThai,
                        HinhAnh = !string.IsNullOrEmpty(goi.HinhAnh) ? (goi.HinhAnh.StartsWith("/") ? goi.HinhAnh : "/Content/images/goivaccine/" + goi.HinhAnh) : "",
                        ChiTietList = chiTietList
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// POST: Admin/CreateGoiVaccine - Tạo gói vaccine mới
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGoiVaccine(string TenGoi, string MoTa, string DoiTuongApDung, decimal GiaGoi, string TrangThai, HttpPostedFileBase HinhAnh)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TenGoi))
                {
                    return Json(new { success = false, message = "Tên gói không được để trống" });
                }

                // Generate MaGoi
                var lastGoi = _unitOfWork.GoiVaccines.Query()
                    .OrderByDescending(g => g.MaGoi)
                    .FirstOrDefault();

                string newMaGoi = "GOI0000001";
                if (lastGoi != null && !string.IsNullOrEmpty(lastGoi.MaGoi))
                {
                    string numberPart = lastGoi.MaGoi.Substring(3);
                    if (int.TryParse(numberPart, out int number))
                    {
                        newMaGoi = "GOI" + (number + 1).ToString().PadLeft(7, '0');
                    }
                }

                // Xử lý upload hình ảnh
                string imagePath = null;
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    var validateResult = ValidateImage(HinhAnh);
                    if (!validateResult.Item1)
                    {
                        return Json(new { success = false, message = validateResult.Item2 });
                    }

                    string fileName = newMaGoi + Path.GetExtension(HinhAnh.FileName);
                    string folderPath = Server.MapPath("~/Content/images/goivaccine/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    HinhAnh.SaveAs(Path.Combine(folderPath, fileName));
                    imagePath = "/Content/images/goivaccine/" + fileName;
                }

                var goiVaccine = new GoiVaccine
                {
                    MaGoi = newMaGoi,
                    TenGoi = TenGoi,
                    MoTa = MoTa,
                    DoiTuongApDung = DoiTuongApDung,
                    GiaGoi = GiaGoi,
                    TrangThai = TrangThai ?? "Đang áp dụng",
                    HinhAnh = imagePath
                };

                _unitOfWork.GoiVaccines.Add(goiVaccine);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Tạo gói vaccine thành công!", data = new { MaGoi = newMaGoi } });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        /// <summary>
        /// POST: Admin/EditGoiVaccine - Cập nhật gói vaccine
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGoiVaccine(string MaGoi, string TenGoi, string MoTa, string DoiTuongApDung, decimal GiaGoi, string TrangThai, HttpPostedFileBase HinhAnh)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(MaGoi))
                {
                    return Json(new { success = false, message = "Mã gói không hợp lệ" });
                }

                var goi = _unitOfWork.GoiVaccines.GetById(MaGoi);
                if (goi == null)
                {
                    return Json(new { success = false, message = "Gói vaccine không tồn tại" });
                }

                // Xử lý upload hình ảnh mới
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    var validateResult = ValidateImage(HinhAnh);
                    if (!validateResult.Item1)
                    {
                        return Json(new { success = false, message = validateResult.Item2 });
                    }

                    string fileName = MaGoi + Path.GetExtension(HinhAnh.FileName);
                    string folderPath = Server.MapPath("~/Content/images/goivaccine/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    HinhAnh.SaveAs(Path.Combine(folderPath, fileName));
                    goi.HinhAnh = "/Content/images/goivaccine/" + fileName;
                }

                goi.TenGoi = TenGoi;
                goi.MoTa = MoTa;
                goi.DoiTuongApDung = DoiTuongApDung;
                goi.GiaGoi = GiaGoi;
                goi.TrangThai = TrangThai;

                _unitOfWork.GoiVaccines.Update(goi);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Cập nhật gói vaccine thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        /// <summary>
        /// POST: Admin/DeleteGoiVaccine - Xóa gói vaccine
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGoiVaccine(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return Json(new { success = false, message = "Mã gói không hợp lệ" });
                }

                var goi = _unitOfWork.GoiVaccines.GetById(id);
                if (goi == null)
                {
                    return Json(new { success = false, message = "Gói vaccine không tồn tại" });
                }

                // Xóa chi tiết gói trước
                var chiTietList = _unitOfWork.ChiTietGoiVaccines.Query()
                    .Where(ct => ct.MaGoi == id)
                    .ToList();

                foreach (var ct in chiTietList)
                {
                    _unitOfWork.ChiTietGoiVaccines.Remove(ct);
                }

                _unitOfWork.GoiVaccines.Remove(goi);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Xóa gói vaccine thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        /// <summary>
        /// GET: Admin/GetVaccineListForGoi - Lấy danh sách vaccine để thêm vào gói
        /// </summary>
        [HttpGet]
        public ActionResult GetVaccineListForGoi()
        {
            try
            {
                var vaccines = _unitOfWork.Vaccines.GetAll()
                    .Select(v => new { MaVC = v.MaVC, TenVC = v.TenVC, GiaBan = v.GiaBan })
                    .OrderBy(v => v.TenVC)
                    .ToList();

                return Json(new { success = true, data = vaccines }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// POST: Admin/AddVaccineToGoi - Thêm vaccine vào gói
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVaccineToGoi(string MaGoi, string MaVC, int SoMui, string GhiChu)
        {
            try
            {
                // Kiểm tra gói tồn tại
                var goi = _unitOfWork.GoiVaccines.GetById(MaGoi);
                if (goi == null)
                {
                    return Json(new { success = false, message = "Gói vaccine không tồn tại" });
                }

                // Kiểm tra vaccine tồn tại
                var vaccine = _unitOfWork.Vaccines.GetById(MaVC);
                if (vaccine == null)
                {
                    return Json(new { success = false, message = "Vaccine không tồn tại" });
                }

                // Kiểm tra vaccine đã có trong gói chưa
                var existing = _unitOfWork.ChiTietGoiVaccines.Query()
                    .FirstOrDefault(ct => ct.MaGoi == MaGoi && ct.MaVC == MaVC);
                if (existing != null)
                {
                    return Json(new { success = false, message = "Vaccine này đã có trong gói" });
                }

                // Generate MaCTGoi
                var lastCT = _unitOfWork.ChiTietGoiVaccines.Query()
                    .OrderByDescending(ct => ct.MaCTGoi)
                    .FirstOrDefault();

                string newMaCTGoi = "CTG0000001";
                if (lastCT != null && !string.IsNullOrEmpty(lastCT.MaCTGoi))
                {
                    string numberPart = lastCT.MaCTGoi.Substring(3);
                    if (int.TryParse(numberPart, out int number))
                    {
                        newMaCTGoi = "CTG" + (number + 1).ToString().PadLeft(7, '0');
                    }
                }

                var chiTiet = new ChiTietGoiVaccine
                {
                    MaCTGoi = newMaCTGoi,
                    MaGoi = MaGoi,
                    MaVC = MaVC,
                    SoMui = SoMui,
                    GhiChu = GhiChu
                };

                _unitOfWork.ChiTietGoiVaccines.Add(chiTiet);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Thêm vaccine vào gói thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        /// <summary>
        /// POST: Admin/RemoveVaccineFromGoi - Xóa vaccine khỏi gói
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveVaccineFromGoi(string id)
        {
            try
            {
                var chiTiet = _unitOfWork.ChiTietGoiVaccines.GetById(id);
                if (chiTiet == null)
                {
                    return Json(new { success = false, message = "Chi tiết không tồn tại" });
                }

                _unitOfWork.ChiTietGoiVaccines.Remove(chiTiet);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Xóa vaccine khỏi gói thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
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
                if (_unitOfWork == null)
                {
                    return Json(new { success = false, message = "UnitOfWork is null" }, JsonRequestBehavior.AllowGet);
                }

                if (_unitOfWork.KhuyenMais == null)
                {
                    return Json(new { success = false, message = "KhuyenMais repository is null" }, JsonRequestBehavior.AllowGet);
                }

                // Lấy danh sách khuyến mãi 
                var khuyenMaiList = _unitOfWork.KhuyenMais.Query().ToList();
                
                if (khuyenMaiList == null)
                {
                    return Json(new { success = true, data = new List<object>() }, JsonRequestBehavior.AllowGet);
                }

                var result = new List<object>();
                
                foreach (var km in khuyenMaiList)
                {
                    if (km == null) continue;

                    // Xử lý đường dẫn hình ảnh
                    string hinhAnhPath = "";
                    if (!string.IsNullOrEmpty(km.HinhAnh))
                    {
                        // Nếu đường dẫn đã có dấu / hoặc http thì giữ nguyên
                        if (km.HinhAnh.StartsWith("/") || km.HinhAnh.StartsWith("http"))
                        {
                            hinhAnhPath = km.HinhAnh;
                        }
                        else
                        {
                            // Nếu chỉ là tên file, thêm path đầy đủ
                            hinhAnhPath = "/Content/images/khuyenmai/" + km.HinhAnh;
                        }
                    }

                    result.Add(new
                    {
                        MaKM = km.MaKM ?? "",
                        TenKM = km.TenKM ?? "",
                        MoTa = km.MoTa ?? "",
                        LoaiKM = km.LoaiKM ?? "",
                        KieuGiam = km.KieuGiam ?? "",
                        GiaTriGiam = km.GiaTriGiam,
                        NgayBatDau = km.NgayBatDau.ToString("dd/MM/yyyy"),
                        NgayKetThuc = km.NgayKetThuc.ToString("dd/MM/yyyy"),
                        TrangThai = km.TrangThai,
                        HinhAnh = hinhAnhPath,
                        SoLuongSanPham = 0
                    });
                }

                return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// POST: Admin/CreateKhuyenMai - Tạo khuyến mãi mới
        /// </summary>
        [HttpPost]
        public ActionResult CreateKhuyenMai(AdminKhuyenMaiCreateEditViewModel model)
        {
            try
            {
                // Validate ModelState
                if (!ModelState.IsValid)
                {
                    var errors = GetModelStateErrors();
                    return Json(new
                    {
                        success = false,
                        message = "Dữ liệu không hợp lệ",
                        errors = errors
                    });
                }

                // Validate ngày
                if (model.NgayKetThuc < model.NgayBatDau)
                {
                    return Json(new { success = false, message = "Ngày kết thúc phải sau ngày bắt đầu" });
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

                    imagePath = SaveKhuyenMaiImage(model.ImageFile, null); // null = generate new filename
                }

                // Generate MaKM
                var lastKM = _unitOfWork.KhuyenMais.Query()
                    .OrderByDescending(km => km.MaKM)
                    .FirstOrDefault();

                string newMaKM = GenerateKhuyenMaiMa(lastKM?.MaKM);

                // Tạo entity KhuyenMai mới
                var khuyenMai = new KhuyenMai
                {
                    MaKM = newMaKM,
                    TenKM = model.TenKM,
                    MoTa = model.MoTa,
                    LoaiKM = model.LoaiKM,
                    KieuGiam = model.KieuGiam,
                    GiaTriGiam = model.GiaTriGiam,
                    NgayBatDau = model.NgayBatDau,
                    NgayKetThuc = model.NgayKetThuc,
                    TrangThai = model.TrangThai,
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
                if (string.IsNullOrEmpty(id))
                {
                    return Json(new { success = false, message = "Mã khuyến mãi không hợp lệ" }, JsonRequestBehavior.AllowGet);
                }

                var khuyenMai = _unitOfWork.KhuyenMais.GetById(id);

                if (khuyenMai == null)
                {
                    return Json(new { success = false, message = "Khuyến mãi không tồn tại" }, JsonRequestBehavior.AllowGet);
                }

                var viewModel = new
                {
                    MaKM = khuyenMai.MaKM,
                    TenKM = khuyenMai.TenKM ?? "",
                    MoTa = khuyenMai.MoTa ?? "",
                    LoaiKM = khuyenMai.LoaiKM ?? "",
                    KieuGiam = khuyenMai.KieuGiam ?? "",
                    GiaTriGiam = khuyenMai.GiaTriGiam,
                    NgayBatDau = khuyenMai.NgayBatDau.ToString("yyyy-MM-dd"),
                    NgayKetThuc = khuyenMai.NgayKetThuc.ToString("yyyy-MM-dd"),
                    TrangThai = khuyenMai.TrangThai,
                    HinhAnh = khuyenMai.HinhAnh ?? ""
                };

                return Json(new { success = true, data = viewModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// POST: Admin/EditKhuyenMai - Cập nhật khuyến mãi
        /// </summary>
        [HttpPost]
        public ActionResult EditKhuyenMai(AdminKhuyenMaiCreateEditViewModel model)
        {
            try
            {
                // Validate MaKM
                if (string.IsNullOrEmpty(model.MaKM))
                {
                    return Json(new { success = false, message = "Mã khuyến mãi không hợp lệ" });
                }

                // Lấy khuyến mãi từ database
                var khuyenMai = _unitOfWork.KhuyenMais.GetById(model.MaKM);
                if (khuyenMai == null)
                {
                    return Json(new { success = false, message = "Khuyến mãi không tồn tại" });
                }

                // Validate ngày
                if (model.NgayKetThuc < model.NgayBatDau)
                {
                    return Json(new { success = false, message = "Ngày kết thúc phải sau ngày bắt đầu" });
                }

                // Xử lý upload hình ảnh mới
                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    var validateImageResult = ValidateImage(model.ImageFile);
                    if (!validateImageResult.Item1)
                    {
                        return Json(new { success = false, message = validateImageResult.Item2 });
                    }

                    // Xóa ảnh cũ nếu có
                    if (!string.IsNullOrEmpty(khuyenMai.HinhAnh))
                    {
                        DeleteKhuyenMaiImage(khuyenMai.HinhAnh);
                    }

                    // Lưu ảnh mới
                    khuyenMai.HinhAnh = SaveKhuyenMaiImage(model.ImageFile, model.MaKM);
                }

                // Cập nhật thông tin
                khuyenMai.TenKM = model.TenKM;
                khuyenMai.MoTa = model.MoTa;
                khuyenMai.LoaiKM = model.LoaiKM;
                khuyenMai.KieuGiam = model.KieuGiam;
                khuyenMai.GiaTriGiam = model.GiaTriGiam;
                khuyenMai.NgayBatDau = model.NgayBatDau;
                khuyenMai.NgayKetThuc = model.NgayKetThuc;
                khuyenMai.TrangThai = model.TrangThai;

                _unitOfWork.KhuyenMais.Update(khuyenMai);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Cập nhật khuyến mãi thành công!" });
            }
            catch (Exception ex)
            {
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
                if (string.IsNullOrEmpty(id))
                {
                    return Json(new { success = false, message = "Mã khuyến mãi không hợp lệ" });
                }

                var khuyenMai = _unitOfWork.KhuyenMais.GetById(id);
                if (khuyenMai == null)
                {
                    return Json(new { success = false, message = "Khuyến mãi không tồn tại" });
                }

                // Xóa hình ảnh nếu có
                if (!string.IsNullOrEmpty(khuyenMai.HinhAnh))
                {
                    DeleteKhuyenMaiImage(khuyenMai.HinhAnh);
                }

                // Xóa khuyến mãi
                _unitOfWork.KhuyenMais.Remove(khuyenMai);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Xóa khuyến mãi thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // ============================================================================
        // KHUYẾN MÃI CRUD - Helper Methods
        // ============================================================================

        /// <summary>
        /// Generate MaKM mới (KM00000001, KM00000002, ...)
        /// </summary>
        private string GenerateKhuyenMaiMa(string lastMaKM)
        {
            if (string.IsNullOrEmpty(lastMaKM))
            {
                return "KM00000001";
            }

            // Extract số từ mã cuối cùng
            string numberPart = lastMaKM.Substring(2); // Lấy phần số (bỏ "KM")
            
            if (int.TryParse(numberPart, out int number))
            {
                number++;
                return "KM" + number.ToString().PadLeft(8, '0');
            }

            return "KM00000001";
        }

        /// <summary>
        /// Lưu hình ảnh khuyến mãi
        /// </summary>
        /// <param name="imageFile">File upload</param>
        /// <param name="maKM">Mã khuyến mãi (null nếu tạo mới)</param>
        /// <returns>Đường dẫn tương đối của ảnh</returns>
        private string SaveKhuyenMaiImage(HttpPostedFileBase imageFile, string maKM)
        {
            try
            {
                // Tạo tên file: MaKM + extension
                string fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
                string fileName = (maKM ?? Guid.NewGuid().ToString()) + fileExtension;

                // Đường dẫn thư mục
                string folderPath = Server.MapPath(KHUYENMAI_IMAGE_PATH);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Lưu file
                string filePath = Path.Combine(folderPath, fileName);
                imageFile.SaveAs(filePath);

                // Trả về path tuyệt đối để lưu vào DB (để tương thích với data cũ)
                return "/Content/images/khuyenmai/" + fileName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Xóa hình ảnh khuyến mãi
        /// </summary>
        /// <param name="imagePath">Đường dẫn ảnh (có thể là tuyệt đối hoặc tương đối)</param>
        private void DeleteKhuyenMaiImage(string imagePath)
        {
            try
            {
                if (string.IsNullOrEmpty(imagePath))
                    return;

                // Xử lý path: nếu bắt đầu bằng / thì bỏ đi để dùng với Server.MapPath
                string relativePath = imagePath.StartsWith("/") ? "~" + imagePath : imagePath;
                string filePath = Server.MapPath(relativePath);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                // Error deleting image
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}