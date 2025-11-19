using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý vaccine và gói vaccine
    /// </summary>
    public class VaccineController : Controller
    {
        // GET: Vaccine/Index
        public ActionResult Index(string search, string loaiBenh)
        {
            // TODO: Load vaccines
            ViewBag.Search = search;
            ViewBag.LoaiBenh = loaiBenh;
            return View();
        }

        // GET: Vaccine/Detail/VCxxx
        public ActionResult Detail(string id)
        {
            // TODO: Load vaccine detail
            return View();
        }

        // GET: Vaccine/ChiTiet/VCxxx
        public ActionResult ChiTiet(string id)
        {
            // TODO: Load vaccine detail
            return View();
        }
    }
}
