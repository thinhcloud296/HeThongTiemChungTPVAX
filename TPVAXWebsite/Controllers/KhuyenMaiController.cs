using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý Khuyến mãi
    /// </summary>
    public class KhuyenMaiController : Controller
    {
        // GET: KhuyenMai/Index
        public ActionResult Index()
        {
            // TODO: Load danh sách khuyến mãi
            return View();
        }

        // GET: KhuyenMai/Detail/{id}
        public ActionResult Detail(string id)
        {
            // TODO: Load chi tiết khuyến mãi
            return View();
        }
    }
}
