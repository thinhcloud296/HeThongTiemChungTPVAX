using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Services;
using TPVAXWebsite.Models.ViewModels;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Filters;
using TPVAXWebsite.DAL.Repositories;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý lịch tiêm chủng
    /// </summary>
    [CustomAuthorize]
    public class LichTiemController : Controller
    {
        private readonly ILichTiemService _lichTiemService;

        public LichTiemController()
        {
            var context = new TPVAXDbContext();
            var unitOfWork = new UnitOfWork(context);
            _lichTiemService = new LichTiemService(unitOfWork);
        }

        // GET: LichTiem/LichSuTiemChung
        public ActionResult LichSuTiemChung()
        {
            string maKH = Session["MaKH"]?.ToString();

            if (string.IsNullOrEmpty(maKH))
            {
                return RedirectToAction("Login", "Account");
            }

            var lichTiems = _lichTiemService.GetLichTiemByMaKH(maKH);
            return View(lichTiems);
        }

        // GET: LichTiem/DatLich
        [HttpGet]
        public ActionResult DatLich(string maVC)
        {
            string maKH = Session["MaKH"]?.ToString();

            if (string.IsNullOrEmpty(maKH))
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách hồ sơ để chọn
            var hoSos = _lichTiemService.GetHoSoTiemChungByMaKH(maKH);
            ViewBag.HoSos = hoSos;

            // Nếu có mã vaccine, load thông tin vaccine
            if (!string.IsNullOrEmpty(maVC))
            {
                using (var unitOfWork = new UnitOfWork(new TPVAXDbContext()))
                {
                    var vaccine = unitOfWork.Repository<Vaccine>().GetById(maVC);
                    ViewBag.Vaccine = vaccine;
                }
            }

            return View();
        }

        // POST: LichTiem/DatLich
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatLich(DatLichTiemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string maKH = Session["MaKH"]?.ToString();
                var hoSos = _lichTiemService.GetHoSoTiemChungByMaKH(maKH);
                ViewBag.HoSos = hoSos;
                return View(model);
            }

            var success = _lichTiemService.DatLichTiem(model);

            if (success)
            {
                TempData["SuccessMessage"] = "Đặt lịch tiêm thành công!";
                return RedirectToAction("LichSuTiemChung");
            }

            ModelState.AddModelError("", "Có lỗi xảy ra khi đặt lịch");
            return View(model);
        }

        // POST: LichTiem/HuyLich
        [HttpPost]
        public JsonResult HuyLich(string maLT)
        {
            var success = _lichTiemService.HuyLichTiem(maLT);

            if (success)
            {
                return Json(new { success = true, message = "Hủy lịch thành công" });
            }

            return Json(new { success = false, message = "Không thể hủy lịch này" });
        }

        // GET: LichTiem/QuanLyHoSo
        public ActionResult QuanLyHoSo()
        {
            string maKH = Session["MaKH"]?.ToString();

            if (string.IsNullOrEmpty(maKH))
            {
                return RedirectToAction("Login", "Account");
            }

            var hoSos = _lichTiemService.GetHoSoTiemChungByMaKH(maKH);
            return View(hoSos);
        }

        // GET: LichTiem/ThemHoSo
        [HttpGet]
        public ActionResult ThemHoSo()
        {
            return View();
        }

        // POST: LichTiem/ThemHoSo
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ThemHoSo")]
        public ActionResult ThemHoSoPost(HoSoTiemChung hoSo, string vaiTro)
        {
            if (!ModelState.IsValid)
            {
                return View(hoSo);
            }

            string maKH = Session["MaKH"]?.ToString();

            if (string.IsNullOrEmpty(maKH))
            {
                return RedirectToAction("Login", "Account");
            }

            var success = _lichTiemService.ThemHoSoNguoiThan(maKH, hoSo, vaiTro);

            if (success)
            {
                TempData["SuccessMessage"] = "Thêm hồ sơ thành công!";
                return RedirectToAction("QuanLyHoSo");
            }

            ModelState.AddModelError("", "Số CCCD đã tồn tại trong hệ thống");
            return View(hoSo);
        }
    }
}
