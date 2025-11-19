using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý thông tin Bệnh truyền nhiễm
    /// </summary>
    public class BenhTruyenNhiemController : Controller
    {
        // GET: BenhTruyenNhiem/Index
        public ActionResult Index(string filter = null)
        {
            // TODO: Load danh sách bệnh truyền nhiễm
            return View();
        }
    }
}
