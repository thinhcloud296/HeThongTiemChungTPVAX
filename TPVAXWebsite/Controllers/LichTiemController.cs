using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Models.Domain;
using System.Data.Entity;
using TPVAXWebsite.DAL;

namespace TPVAXWebsite.Controllers
{
    public class LichTiemController : Controller
    {
        private TPVAXDbContext _context = new TPVAXDbContext();

        // Hiển thị danh sách lịch tiêm (toàn bộ hoặc theo khách hàng)
        public ActionResult Index()
        {
            var lichTiems = _context.LichTiems
                .Include(l => l.Vaccine)
                .Include(l => l.HoSoTiemChung)
                .ToList();

            return View(lichTiems);
        }

        // GET: LichTiem/DatLich
        public ActionResult DatLich(string maVC)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để đặt lịch tiêm.";
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrEmpty(maVC))
            {
                TempData["ErrorMessage"] = "Không tìm thấy thông tin văc xin.";
                return RedirectToAction("Index", "VaccinePhongBenh");
            }

            var vaccine = _context.Vaccines.Find(maVC);
            if (vaccine == null)
            {
                TempData["ErrorMessage"] = "Văc xin không tồn tại.";
                return RedirectToAction("Index", "VaccinePhongBenh");
            }

            // Lấy danh sách hồ sơ tiêm chủng của khách hàng
            var hoSosData = (from lk in _context.LienKetHoSos
                             join hs in _context.HoSoTiemChungs on lk.MaHSTC equals hs.MaHSTC
                             where lk.MaKH == kh.MaKH && hs.TrangThai == true
                             select new
                             {
                                 hs.MaHSTC,
                                 hs.HoTen,
                                 hs.NgaySinh,
                                 lk.VaiTro
                             }).ToList();

            var hoSos = hoSosData.Select(x => new SelectListItem
            {
                Value = x.MaHSTC,
                Text = $"{x.HoTen} - {x.NgaySinh:dd/MM/yyyy} ({x.VaiTro})"
            }).ToList();

            var model = new TPVAXWebsite.Models.ViewModels.DatLichTiemViewModel
            {
                MaVC = vaccine.MaVC,
                TenVaccine = vaccine.TenVC,
                GiaBan = vaccine.GiaBan,
                HinhAnh = vaccine.HinhAnh,
                DanhSachHoSo = hoSos,
                NgayHenTiem = DateTime.Now.AddDays(1).Date.AddHours(9) // Mặc định ngày mai 9h sáng
            };

            return View(model);
        }

        // POST: LichTiem/DatLich
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatLich(TPVAXWebsite.Models.ViewModels.DatLichTiemViewModel model)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập.";
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                // Reload danh sách hồ sơ nếu lỗi
                var hoSosData = (from lk in _context.LienKetHoSos
                                 join hs in _context.HoSoTiemChungs on lk.MaHSTC equals hs.MaHSTC
                                 where lk.MaKH == kh.MaKH && hs.TrangThai == true
                                 select new
                                 {
                                     hs.MaHSTC,
                                     hs.HoTen,
                                     hs.NgaySinh,
                                     lk.VaiTro
                                 }).ToList();

                model.DanhSachHoSo = hoSosData.Select(x => new SelectListItem
                {
                    Value = x.MaHSTC,
                    Text = $"{x.HoTen} - {x.NgaySinh:dd/MM/yyyy} ({x.VaiTro})"
                }).ToList();
                return View(model);
            }

            try
            {
                // Validate ngày hẹn phải là tương lai
                if (model.NgayHenTiem < DateTime.Now)
                {
                    ModelState.AddModelError("NgayHenTiem", "Ngày hẹn phải lớn hơn thời điểm hiện tại.");
                    return View(model);
                }

                // Kiểm tra hồ sơ có thuộc về khách hàng không
                var lienKet = _context.LienKetHoSos
                    .FirstOrDefault(lk => lk.MaKH == kh.MaKH && lk.MaHSTC == model.MaHSTC);
                if (lienKet == null)
                {
                    ModelState.AddModelError("MaHSTC", "Hồ sơ không hợp lệ.");
                    return View(model);
                }

                // Tạo mã lịch tiêm tự động
                string maLT;
                var lastLichTiem = _context.LichTiems
                    .OrderByDescending(lt => lt.MaLT)
                    .FirstOrDefault();

                if (lastLichTiem != null && lastLichTiem.MaLT.Length >= 10)
                {
                    int lastNumber = int.Parse(lastLichTiem.MaLT.Substring(2));
                    maLT = "LT" + (lastNumber + 1).ToString("D8");
                }
                else
                {
                    maLT = "LT00000001";
                }

                // Tạo lịch tiêm mới
                var lichTiem = new LichTiem
                {
                    MaLT = maLT,
                    MaHSTC = model.MaHSTC,
                    MaVC = model.MaVC,
                    NgayHenTiem = model.NgayHenTiem,
                    SoMui = 1, // Mặc định mũi 1
                    TrangThai = "Chưa tiêm",
                    GhiChu = model.GhiChu
                };

                _context.LichTiems.Add(lichTiem);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Đặt lịch tiêm thành công! Chúng tôi sẽ liên hệ xác nhận sớm.";
                return RedirectToAction("Dashboard", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
                // Reload danh sách hồ sơ
                var hoSosData = (from lk in _context.LienKetHoSos
                                 join hs in _context.HoSoTiemChungs on lk.MaHSTC equals hs.MaHSTC
                                 where lk.MaKH == kh.MaKH && hs.TrangThai == true
                                 select new
                                 {
                                     hs.MaHSTC,
                                     hs.HoTen,
                                     hs.NgaySinh,
                                     lk.VaiTro
                                 }).ToList();

                model.DanhSachHoSo = hoSosData.Select(x => new SelectListItem
                {
                    Value = x.MaHSTC,
                    Text = $"{x.HoTen} - {x.NgaySinh:dd/MM/yyyy} ({x.VaiTro})"
                }).ToList();
                return View(model);
            }
        }

        // Đổi lịch (cập nhật ngày hẹn)
        [HttpPost]
        public JsonResult DoiLichNgay(string MaLT, DateTime NgayHenTiem)
        {
            try
            {
                var kh = Session["KH"] as KhachHang;
                if (kh == null)
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập." });
                }

                var lich = _context.LichTiems.Find(MaLT);
                if (lich == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy lịch hẹn." });
                }

                // Kiểm tra quyền: chỉ cho phép đổi lịch của hồ sơ liên kết với tài khoản
                var lienKet = _context.LienKetHoSos
                    .FirstOrDefault(lk => lk.MaKH == kh.MaKH && lk.MaHSTC == lich.MaHSTC);

                if (lienKet == null)
                {
                    return Json(new { success = false, message = "Bạn không có quyền đổi lịch này." });
                }

                // Kiểm tra ngày hẹn mới phải trong tương lai
                if (NgayHenTiem < DateTime.Now)
                {
                    return Json(new { success = false, message = "Ngày hẹn mới phải sau thời điểm hiện tại." });
                }

                // Kiểm tra trạng thái có thể đổi
                if (lich.TrangThai != "Chưa tiêm")
                {
                    return Json(new { success = false, message = "Chỉ có thể đổi lịch hẹn đang chờ tiêm." });
                }

                lich.NgayHenTiem = NgayHenTiem;
                _context.SaveChanges();

                return Json(new { success = true, message = "Đổi lịch thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // Hủy lịch (chỉ đổi trạng thái, không xóa)
        [HttpPost]
        public JsonResult HuyLich(string id)
        {
            try
            {
                var kh = Session["KH"] as KhachHang;
                if (kh == null)
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập." });
                }

                var lich = _context.LichTiems.Find(id);
                if (lich == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy lịch hẹn." });
                }

                // Kiểm tra quyền
                var lienKet = _context.LienKetHoSos
                    .FirstOrDefault(lk => lk.MaKH == kh.MaKH && lk.MaHSTC == lich.MaHSTC);

                if (lienKet == null)
                {
                    return Json(new { success = false, message = "Bạn không có quyền hủy lịch này." });
                }

                // Kiểm tra trạng thái có thể hủy
                if (lich.TrangThai == "Đã tiêm")
                {
                    return Json(new { success = false, message = "Không thể hủy lịch đã tiêm." });
                }

                if (lich.TrangThai == "Đã hủy")
                {
                    return Json(new { success = false, message = "Lịch hẹn này đã được hủy trước đó." });
                }

                lich.TrangThai = "Đã hủy";
                _context.SaveChanges();

                return Json(new { success = true, message = "Hủy lịch thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
