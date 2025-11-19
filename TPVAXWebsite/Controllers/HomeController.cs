using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Services;
using TPVAXWebsite.DAL.Repositories;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller chính cho trang chủ và các trang thông tin
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IVaccineService _vaccineService;

        public HomeController()
        {
            var context = new TPVAXDbContext();
            var unitOfWork = new UnitOfWork(context);
            _vaccineService = new VaccineService(unitOfWork);
        }

        // GET: Home/Index
        public ActionResult Index()
        {
            // Load featured vaccines, promotions
            var vaccines = _vaccineService.GetAllVaccines();
            var goiVaccines = _vaccineService.GetAllGoiVaccines();

            ViewBag.Vaccines = vaccines;
            ViewBag.GoiVaccines = goiVaccines;

            return View();
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

        // GET: Home/BenhTruyenNhiem
        public ActionResult BenhTruyenNhiem()
        {
            // Load danh sách bệnh truyền nhiễm
            using (var unitOfWork = new UnitOfWork(new TPVAXDbContext()))
            {
                var loaiBenhs = unitOfWork.Repository<Models.Domain.LoaiBenh>().GetAll();
                return View(loaiBenhs);
            }
        }

        // GET: Home/ToiNenTiemGi
        public ActionResult ToiNenTiemGi()
        {
            // Logic tư vấn vaccine theo độ tuổi, nhóm đối tượng
            return View();
        }
    }
}
