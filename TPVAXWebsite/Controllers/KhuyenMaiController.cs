using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.DAL.Repositories;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý Khuyến mãi
    /// </summary>
    public class KhuyenMaiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public KhuyenMaiController()
        {
            _unitOfWork = new UnitOfWork(new TPVAXDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork?.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: KhuyenMai/Index
        public ActionResult Index()
        {
            var khuyenMais = _unitOfWork.Repository<KhuyenMai>()
                .GetAll()
                .Where(k => k.NgayKetThuc >= DateTime.Now)
                .OrderBy(k => k.NgayBatDau)
                .ToList();

            return View(khuyenMais);
        }

        // GET: KhuyenMai/Detail/{id}
        public ActionResult Detail(string id)
        {
            var khuyenMai = _unitOfWork.Repository<KhuyenMai>().GetById(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }

            var chiTiet = _unitOfWork.Repository<ChiTietKhuyenMai>()
                .GetAll()
                .Where(c => c.MaKhuyenMai == id)
                .ToList();

            var viewModel = new KhuyenMaiDetailViewModel
            {
                KhuyenMai = khuyenMai,
                ChiTietKhuyenMai = chiTiet
            };

            return View(viewModel);
        }

        // POST: KhuyenMai/ApDung
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ApDung(string maKhuyenMai)
        {
            try
            {
                var khuyenMai = _unitOfWork.Repository<KhuyenMai>().GetById(maKhuyenMai);
                if (khuyenMai == null)
                {
                    return Json(new { success = false, message = "Mã khuyến mãi không tồn tại!" });
                }

                // Check validity
                if (DateTime.Now < khuyenMai.NgayBatDau || DateTime.Now > khuyenMai.NgayKetThuc)
                {
                    return Json(new { success = false, message = "Mã khuyến mãi đã hết hạn hoặc chưa có hiệu lực!" });
                }

                // Apply discount logic here
                Session["AppliedKhuyenMai"] = maKhuyenMai;

                return Json(new { 
                    success = true, 
                    message = "Áp dụng khuyến mãi thành công!",
                    discount = khuyenMai.GiamGia
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }
    }
}
