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
        public ActionResult Index()
        {
            try
            {
                // Load tất cả vaccines để hiển thị trên trang chủ
                var vaccines = _vaccineService.GetAll().ToList();
                
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
