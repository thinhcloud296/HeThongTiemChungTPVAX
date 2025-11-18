using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý vaccine và gói vaccine
    /// </summary>
    public class VaccineController : Controller
    {
        private readonly IVaccineService _vaccineService;

        public VaccineController()
        {
            var context = new TPVAXDbContext();
            var unitOfWork = new UnitOfWork(context);
            _vaccineService = new VaccineService(unitOfWork);
        }

        // GET: Vaccine/Index
        public ActionResult Index(string search, string loaiBenh)
        {
            var vaccines = string.IsNullOrEmpty(search) 
                ? _vaccineService.GetAllVaccines() 
                : _vaccineService.SearchVaccines(search);

            if (!string.IsNullOrEmpty(loaiBenh))
            {
                vaccines = _vaccineService.GetVaccinesByLoaiBenh(loaiBenh);
            }

            ViewBag.Search = search;
            ViewBag.LoaiBenh = loaiBenh;

            return View(vaccines);
        }

        // GET: Vaccine/Details/VCxxx
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            var detail = _vaccineService.GetVaccineDetail(id);

            if (detail == null)
            {
                return HttpNotFound();
            }

            return View(detail);
        }

        // GET: Vaccine/TheoDoiTuong
        public ActionResult TheoDoiTuong(string doiTuong)
        {
            using (var unitOfWork = new UnitOfWork(new TPVAXDbContext()))
            {
                var loaiBenhs = unitOfWork.Repository<Models.Domain.LoaiBenh>()
                    .Find(lb => lb.NhomDoiTuong.Contains(doiTuong))
                    .ToList();

                ViewBag.DoiTuong = doiTuong;
                return View(loaiBenhs);
            }
        }

        // GET: Vaccine/GoiVaccine
        public ActionResult GoiVaccine()
        {
            var goiVaccines = _vaccineService.GetAllGoiVaccines();
            return View(goiVaccines);
        }

        // GET: Vaccine/GoiVaccineDetail/GOxxx
        public ActionResult GoiVaccineDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("GoiVaccine");
            }

            var goi = _vaccineService.GetGoiVaccineById(id);

            if (goi == null)
            {
                return HttpNotFound();
            }

            // Lấy chi tiết các vaccine trong gói
            using (var unitOfWork = new UnitOfWork(new TPVAXDbContext()))
            {
                var chiTiets = unitOfWork.Repository<Models.Domain.ChiTietGoiVaccine>()
                    .Find(ct => ct.MaGoi == id)
                    .ToList();

                ViewBag.ChiTietGoiVaccines = chiTiets;
            }

            return View(goi);
        }
    }
}
