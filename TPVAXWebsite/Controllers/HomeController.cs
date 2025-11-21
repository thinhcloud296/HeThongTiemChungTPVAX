using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller chính cho trang chủ và các trang thông tin
    /// </summary>
    public class HomeController : Controller
    {
        private readonly VaccineService _vaccineService;

        public HomeController()
        {
            _vaccineService = new VaccineService();
        }

        // GET: Home/Index
        public ActionResult Index(string search, int page = 1)
        {
            try
            {
                int pageSize = 9; // Hiển thị 9 vaccine mỗi trang
                
                // Load tất cả vaccines
                var allVaccines = string.IsNullOrEmpty(search) 
                    ? _vaccineService.GetAll().ToList()
                    : _vaccineService.Search(search).ToList();
                
                // Tính toán phân trang
                int totalVaccines = allVaccines.Count();
                int totalPages = (int)Math.Ceiling(totalVaccines / (double)pageSize);
                
                // Đảm bảo page hợp lệ
                if (page < 1) page = 1;
                if (page > totalPages && totalPages > 0) page = totalPages;
                
                // Lấy dữ liệu theo trang
                var vaccines = allVaccines
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                
                ViewBag.TotalVaccines = totalVaccines;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.PageSize = pageSize;
                ViewBag.Search = search;
                
                return View(vaccines); 
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi tải danh sách vaccine: " + ex.Message;
                return View();
            }
        }

        // GET: Home/About
        public ActionResult About()
        {
            return View();
        }

        // GET: Home/Contact
        public ActionResult Contact()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vaccineService?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
