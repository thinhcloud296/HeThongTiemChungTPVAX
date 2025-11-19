using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý Bài viết
    /// </summary>
    public class BaiVietController : Controller
    {
        // GET: BaiViet/Index
        public ActionResult Index(string category = null, string tag = null)
        {
            // TODO: Load bài viết
            return View();
        }

        // GET: BaiViet/Detail/{slug}
        public ActionResult Detail(string slug)
        {
            // TODO: Load chi tiết bài viết
            return View();
        }
    }
}
