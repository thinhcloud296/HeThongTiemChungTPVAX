using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý danh sách vaccine theo loại bệnh với tìm kiếm, lọc, phân trang
    /// </summary>
    public class VaccinePhongBenhController : Controller
    {
        private readonly VaccineService _vaccineService;

        public VaccinePhongBenhController()
        {
            _vaccineService = new VaccineService();
        }

        // GET: VaccinePhongBenh/Index
        public ActionResult Index(string search, string loaiBenh, int page = 1)
        {
            try
            {
                int pageSize = 12; // Hiển thị 12 vaccine mỗi trang
                
                // Load danh sách vaccine từ database
                var allVaccines = _vaccineService.SearchAndFilter(search, null, loaiBenh).ToList();
                
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

                // Truyền dữ liệu sang View
                ViewBag.Search = search;
                ViewBag.LoaiBenh = loaiBenh;
                ViewBag.TotalCount = totalVaccines;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.PageSize = pageSize;

                return View(vaccines);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi tải danh sách vaccine: " + ex.Message;
                return View();
            }
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
