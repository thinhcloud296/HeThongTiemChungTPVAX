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
        public ActionResult DoiLichNgay(string MaLT, DateTime NgayHenTiem)
        {
            var lich = _context.LichTiems.Find(MaLT);
            if (lich == null) return HttpNotFound();

            lich.NgayHenTiem = NgayHenTiem;
            _context.SaveChanges();
            return new HttpStatusCodeResult(200);
        }

        // Hủy lịch (chỉ đổi trạng thái, không xóa)
        [HttpPost]
        public ActionResult HuyLich(string id)
        {
            var lich = _context.LichTiems.Find(id);
            if (lich == null) return HttpNotFound();

            lich.TrangThai = "Đã hủy";
            _context.SaveChanges();
            return new HttpStatusCodeResult(200);
        }
    }
}
