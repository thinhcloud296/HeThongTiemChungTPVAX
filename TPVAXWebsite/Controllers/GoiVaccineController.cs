using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý Gói Vắc xin
    /// </summary>
    public class GoiVaccineController : Controller
    {
        // GET: GoiVaccine/Index
        public ActionResult Index()
        {
            // TODO: Load danh sách gói vaccine
            return View();
        }

        // GET: GoiVaccine/Detail/{id}
        public ActionResult Detail(string id)
        {
            // TODO: Load chi tiết gói vaccine
            return View();
        }
    }
}
