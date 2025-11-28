using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý Gói Vắc xin
    /// </summary>
    public class GoiVaccineController : Controller
    {
        private readonly GoiVaccineService _goiVaccineService;

        public GoiVaccineController()
        {
            _goiVaccineService = new GoiVaccineService();
        }

        // GET: GoiVaccine/Index
        public ActionResult Index(string search, string doiTuong, int page = 1)
        {
            try
            {
                int pageSize = 9; // 9 gói/trang
                int totalRecords;

                var goiVaccines = _goiVaccineService.SearchAndPaginate(
                    search, 
                    doiTuong, 
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
                ViewBag.DoiTuong = doiTuong;

                // Stats
                ViewBag.TotalPackages = _goiVaccineService.GetCount();
                ViewBag.ActivePackages = _goiVaccineService.GetActiveCount();

                return View(goiVaccines);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message;
                return View();
            }
        }

        // GET: GoiVaccine/Detail/{id}
        public ActionResult Detail(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index");
                }

                var goiVaccine = _goiVaccineService.GetDetailViewModelNew(id);
                
                if (goiVaccine == null)
                {
                    ViewBag.Error = "Không tìm thấy gói vaccine với mã: " + id;
                    return RedirectToAction("Index");
                }

                // Get related packages
                ViewBag.RelatedPackages = _goiVaccineService.GetPopularPackages(4)
                    .Where(g => g.MaGoi != id)
                    .Take(3)
                    .ToList();

                return View(goiVaccine);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _goiVaccineService?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
