using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Services;
using TPVAXWebsite.Filters;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý giỏ hàng
    /// </summary>
    [CustomAuthorize]
    public class GioHangController : Controller
    {
        private readonly IGioHangService _gioHangService;

        public GioHangController()
        {
            var context = new TPVAXDbContext();
            var unitOfWork = new UnitOfWork(context);
            _gioHangService = new GioHangService(unitOfWork);
        }

        // GET: GioHang/Index
        public ActionResult Index()
        {
            string maKH = Session["MaKH"]?.ToString();
            
            if (string.IsNullOrEmpty(maKH))
            {
                return RedirectToAction("Login", "Account");
            }

            var gioHangs = _gioHangService.GetGioHangByMaKH(maKH);
            var tongTien = _gioHangService.TinhTongTien(maKH);

            ViewBag.TongTien = tongTien;

            return View(gioHangs);
        }

        // POST: GioHang/ThemVaoGio
        [HttpPost]
        public JsonResult ThemVaoGio(string maSanPham, string loaiSanPham, int soLuong = 1)
        {
            string maKH = Session["MaKH"]?.ToString();

            if (string.IsNullOrEmpty(maKH))
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập" });
            }

            var success = _gioHangService.ThemVaoGio(maKH, maSanPham, loaiSanPham, soLuong);

            if (success)
            {
                var tongTien = _gioHangService.TinhTongTien(maKH);
                return Json(new { success = true, message = "Đã thêm vào giỏ hàng", tongTien = tongTien });
            }

            return Json(new { success = false, message = "Có lỗi xảy ra" });
        }

        // POST: GioHang/CapNhatSoLuong
        [HttpPost]
        public JsonResult CapNhatSoLuong(int maGH, int soLuong)
        {
            var success = _gioHangService.CapNhatSoLuong(maGH, soLuong);

            if (success)
            {
                string maKH = Session["MaKH"]?.ToString();
                var tongTien = _gioHangService.TinhTongTien(maKH);
                return Json(new { success = true, tongTien = tongTien });
            }

            return Json(new { success = false, message = "Có lỗi xảy ra" });
        }

        // POST: GioHang/Xoa
        [HttpPost]
        public JsonResult Xoa(int maGH)
        {
            var success = _gioHangService.XoaKhoiGio(maGH);

            if (success)
            {
                string maKH = Session["MaKH"]?.ToString();
                var tongTien = _gioHangService.TinhTongTien(maKH);
                return Json(new { success = true, tongTien = tongTien });
            }

            return Json(new { success = false, message = "Có lỗi xảy ra" });
        }

        // POST: GioHang/ApDungKhuyenMai
        [HttpPost]
        public JsonResult ApDungKhuyenMai(string maKM)
        {
            // TODO: Implement logic áp dụng khuyến mãi
            using (var unitOfWork = new UnitOfWork(new TPVAXDbContext()))
            {
                var khuyenMai = unitOfWork.Repository<Models.Domain.KhuyenMai>().GetById(maKM);

                if (khuyenMai != null && khuyenMai.TrangThai == true)
                {
                    Session["MaKM"] = maKM;
                    return Json(new { success = true, message = "Áp dụng khuyến mãi thành công" });
                }

                return Json(new { success = false, message = "Mã khuyến mãi không hợp lệ" });
            }
        }
    }
}
