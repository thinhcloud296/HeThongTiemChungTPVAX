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
        private readonly GoiVaccineService _goiVaccineService;

        public HomeController()
        {
            _vaccineService = new VaccineService();
            _goiVaccineService = new GoiVaccineService();
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

                // Lấy danh sách gói vaccine
                try 
                {
                    var goiVaccines = _goiVaccineService.GetAll()
                        .Where(g => g.TrangThai == "Đang áp dụng")
                        .ToList();
                    ViewBag.GoiVaccines = goiVaccines;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error loading packages: " + ex.Message);
                    ViewBag.GoiVaccines = new System.Collections.Generic.List<TPVAXWebsite.Models.Domain.GoiVaccine>();
                }

                // Lấy 4 vaccine ngẫu nhiên cho phần "Mùa này cần tiêm gì?"
                try
                {
                    if (allVaccines != null && allVaccines.Any())
                    {
                        var seasonalVaccines = allVaccines.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
                        ViewBag.SeasonalVaccines = seasonalVaccines;
                    }
                    else
                    {
                        ViewBag.SeasonalVaccines = new System.Collections.Generic.List<TPVAXWebsite.Models.Domain.Vaccine>();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error loading seasonal vaccines: " + ex.Message);
                    ViewBag.SeasonalVaccines = new System.Collections.Generic.List<TPVAXWebsite.Models.Domain.Vaccine>();
                }
                
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
                _goiVaccineService?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
