using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý Khuyến mãi
    /// </summary>
    public class KhuyenMaiController : Controller
    {
        private readonly KhuyenMaiService _khuyenMaiService;

        public KhuyenMaiController()
        {
            _khuyenMaiService = new KhuyenMaiService();
        }

        // GET: KhuyenMai/Index
        public ActionResult Index(string search, string loaiKM, string trangThai, int page = 1)
        {
            try
            {
                int pageSize = 12; // 12 khuyến mãi/trang
                int totalRecords;

                var khuyenMais = _khuyenMaiService.SearchAndPaginate(
                    search,
                    loaiKM,
                    trangThai,
                    page,
                    pageSize,
                    out totalRecords
                );

                // Pagination data
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                ViewBag.TotalCount = totalRecords;
                ViewBag.PageSize = pageSize;

                // Filter data
                ViewBag.Search = search;
                ViewBag.LoaiKM = loaiKM;
                ViewBag.TrangThai = trangThai;

                // Stats
                ViewBag.TotalPromotions = _khuyenMaiService.GetCount();
                ViewBag.ActivePromotions = _khuyenMaiService.GetActiveCount();

                // Get active promotions for highlight
                ViewBag.ActiveList = _khuyenMaiService.GetActivePromotions().Take(3).ToList();
                ViewBag.UpcomingList = _khuyenMaiService.GetUpcomingPromotions().Take(3).ToList();

                return View(khuyenMais);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message;
                return View();
            }
        }

        // GET: KhuyenMai/Detail/{id}
        public ActionResult Detail(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index");
                }

                var khuyenMai = _khuyenMaiService.GetDetailViewModel(id);

                if (khuyenMai == null)
                {
                    ViewBag.Error = "Không tìm thấy khuyến mãi với mã: " + id;
                    return View("Error");
                }

                // Get related promotions
                ViewBag.RelatedPromotions = _khuyenMaiService.GetFeaturedPromotions(4)
                    .Where(km => km.MaKM != id)
                    .Take(3)
                    .ToList();

                return View(khuyenMai);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message;
                return View("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _khuyenMaiService?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
