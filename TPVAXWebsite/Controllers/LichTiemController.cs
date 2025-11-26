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

        // Đặt lịch mới
        public ActionResult DatLich(string maVC)
        {
            ViewBag.MaVC = maVC;
            return View();
        }

        [HttpPost]
        public ActionResult DatLich(LichTiem model)
        {
            if (ModelState.IsValid)
            {
                model.MaLT = "LT" + DateTime.Now.Ticks.ToString();
                model.TrangThai = "Chưa tiêm";
                _context.LichTiems.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Account");
            }
            return View(model);
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
