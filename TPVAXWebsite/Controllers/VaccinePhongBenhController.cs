using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý trang Vaccine Phòng Bệnh
    /// </summary>
    public class VaccinePhongBenhController : Controller
    {
        // GET: VaccinePhongBenh/Index
        public ActionResult Index()
        {
            // TODO: Load danh sách vaccine phòng bệnh theo từng loại bệnh
            return View();
        }
    }
}
