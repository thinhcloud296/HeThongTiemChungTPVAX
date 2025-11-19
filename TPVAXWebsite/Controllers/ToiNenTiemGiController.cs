using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý trang Tôi Nên Tiêm Gì - tư vấn vaccine
    /// </summary>
    public class ToiNenTiemGiController : Controller
    {
        // GET: ToiNenTiemGi/Index
        public ActionResult Index()
        {
            // TODO: Hiển thị form tư vấn vaccine theo độ tuổi và giới tính
            return View();
        }
    }
}
