using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Services;
using TPVAXWebsite.Filters;
using TPVAXWebsite.DAL.Repositories;
namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý hóa đơn và thanh toán
    /// </summary>
    [CustomAuthorize]
    public class HoaDonController : Controller
    {
        private readonly IHoaDonService _hoaDonService;

        public HoaDonController()
        {
            var context = new TPVAXDbContext();
            var unitOfWork = new UnitOfWork(context);
            var gioHangService = new GioHangService(unitOfWork);
            _hoaDonService = new HoaDonService(unitOfWork, gioHangService);
        }

        // GET: HoaDon/DanhSach
        public ActionResult DanhSach()
        {
            string maKH = Session["MaKH"]?.ToString();

            if (string.IsNullOrEmpty(maKH))
            {
                return RedirectToAction("Login", "Account");
            }

            var hoaDons = _hoaDonService.GetHoaDonsByMaKH(maKH);
            return View(hoaDons);
        }

        // GET: HoaDon/ChiTiet/HDxxx
        public ActionResult ChiTiet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("DanhSach");
            }

            var hoaDon = _hoaDonService.GetHoaDonDetail(id);

            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            return View(hoaDon);
        }

        // GET: HoaDon/ThanhToan
        [HttpGet]
        public ActionResult ThanhToan()
        {
            string maKH = Session["MaKH"]?.ToString();

            if (string.IsNullOrEmpty(maKH))
            {
                return RedirectToAction("Login", "Account");
            }

            // Load giỏ hàng để hiển thị
            using (var unitOfWork = new UnitOfWork(new TPVAXDbContext()))
            {
                var gioHangService = new GioHangService(unitOfWork);
                var gioHangs = gioHangService.GetGioHangByMaKH(maKH);
                var tongTien = gioHangService.TinhTongTien(maKH);

                ViewBag.GioHangs = gioHangs;
                ViewBag.TongTien = tongTien;
            }

            return View();
        }

        // POST: HoaDon/ThanhToan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThanhToan(string phuongThucThanhToan)
        {
            string maKH = Session["MaKH"]?.ToString();
            string maKM = Session["MaKM"]?.ToString();

            if (string.IsNullOrEmpty(maKH))
            {
                return RedirectToAction("Login", "Account");
            }

            var maHD = _hoaDonService.TaoHoaDonTuGioHang(maKH, maKM);

            if (!string.IsNullOrEmpty(maHD))
            {
                // Clear mã khuyến mãi
                Session["MaKM"] = null;

                TempData["SuccessMessage"] = "Đặt hàng thành công!";
                return RedirectToAction("ChiTiet", new { id = maHD });
            }

            TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo hóa đơn";
            return RedirectToAction("ThanhToan");
        }

        // POST: HoaDon/HuyHoaDon
        [HttpPost]
        public JsonResult HuyHoaDon(string maHD)
        {
            var success = _hoaDonService.HuyHoaDon(maHD);

            if (success)
            {
                return Json(new { success = true, message = "Hủy hóa đơn thành công" });
            }

            return Json(new { success = false, message = "Không thể hủy hóa đơn này" });
        }

        // GET: HoaDon/InHoaDon/HDxxx
        public ActionResult InHoaDon(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("DanhSach");
            }

            var hoaDon = _hoaDonService.GetHoaDonDetail(id);

            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            // TODO: Generate PDF
            // Tạm thời return view in
            return View("ChiTiet", hoaDon);
        }
    }
}
