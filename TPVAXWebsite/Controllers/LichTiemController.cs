using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý lịch tiêm chủng
    /// </summary>
    public class LichTiemController : Controller
    {
        private readonly UnitOfWork _uow = new UnitOfWork();
        // GET: LichTiem/Index
        public ActionResult Index()
        {
            // TODO: Hiển thị danh sách lịch tiêm
            return View();
        }

        // GET: Hien thi form dat lich
        public ActionResult DatLich(string vaccineId)
        {
            // 1. Kiểm tra đăng nhập
            // Giả sử bạn lưu MaKH trong Session khi đăng nhập
            var currentMaKH = Session["MaKH"] as string;
            if (string.IsNullOrEmpty(currentMaKH))
            {
                // Lưu lại trang hiện tại để login xong quay lại
                return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.PathAndQuery });
            }

            // 2. Lấy thông tin Vaccine (để hiện tên và giá)
            var vaccine = _uow.Vaccines.GetById(vaccineId);
            if (vaccine == null) return HttpNotFound();

            // 3. Lấy danh sách Hồ sơ tiêm chủng liên kết với Khách hàng này
            // Logic: Join bảng LienKetHoSo -> HoSoTiemChung
            var listHoSo = _uow.LienKetHoSos.Query()
                .Where(lk => lk.MaKH == currentMaKH)
                .Select(lk => new
                {
                    lk.MaHSTC,
                    // Hiện tên kèm vai trò cho dễ chọn. VD: "Nguyễn Văn A (Con trai)"
                    HienThi = lk.HoSoTiemChung.HoTen + " (" + lk.VaiTro + ")"
                })
                .ToList();

            // 4. Khởi tạo ViewModel
            var model = new DatLichTiemViewModel
            {
                MaVC = vaccineId,
                TenVaccine = vaccine.TenVC,
                GiaBan = vaccine.GiaBan, // Cần thêm property này vào VM nếu muốn hiện giá
                HinhAnh = vaccine.HinhAnh,
                NgayHenTiem = DateTime.Now.AddDays(1), // Mặc định ngày mai

                // Tạo DropdownList
                DanhSachHoSo = new SelectList(listHoSo, "MaHSTC", "HienThi")
            };

            return View(model);
        }

        // POST: Xu ly luu database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatLich(DatLichTiemViewModel model)
        {
            // Lấy lại MaKH để validation bảo mật (tránh hack form)
            var currentMaKH = Session["MaKH"] as string;
            if (string.IsNullOrEmpty(currentMaKH)) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Tạo mã Lịch tiêm tự động (Ví dụ: LT + Timestamp)
                    // Bạn nên có hàm sinh mã riêng, đây là ví dụ đơn giản
                    string newMaLT = "LT" + DateTime.Now.ToString("ddHHmmss");

                    // 2. Tạo Entity LichTiem
                    var lichTiem = new LichTiem
                    {
                        MaLT = newMaLT,
                        NgayHenTiem = model.NgayHenTiem,
                        NgayTiemThucTe = null, // Chưa tiêm
                        SoMui = 1, // Mặc định là mũi 1, hoặc logic phức tạp hơn
                        TrangThai = "Chưa tiêm", // Theo DB của bạn
                        GhiChu = model.GhiChu,
                        MaHSTC = model.MaHSTC,
                        MaVC = model.MaVC,
                        MaNV = null // Chưa có nhân viên phụ trách
                    };

                    _uow.LichTiems.Add(lichTiem);
                    _uow.SaveChanges();

                    // 3. Thông báo và chuyển hướng
                    TempData["SuccessMessage"] = "Đặt lịch thành công!";
                    return RedirectToAction("LichSuDatLich"); // Chuyển sang trang lịch sử
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
                }
            }

            // Nếu lỗi (validation false hoặc try-catch), phải load lại DropdownList
            // Vì HTTP là stateless, nó không nhớ cái list cũ
            var listHoSo = _uow.LienKetHoSos.Query()
                .Where(lk => lk.MaKH == currentMaKH)
                .Select(lk => new { lk.MaHSTC, HienThi = lk.HoSoTiemChung.HoTen + " (" + lk.VaiTro + ")" })
                .ToList();
            model.DanhSachHoSo = new SelectList(listHoSo, "MaHSTC", "HienThi");

            return View(model);
        }
    }
}

