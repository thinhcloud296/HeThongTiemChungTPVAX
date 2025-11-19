using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý giỏ hàng
    /// </summary>
    public class GioHangController : Controller
    {
        // GET: GioHang/Index
        public ActionResult Index()
        {
            // TODO: Load cart items
            return View();
        }

        // GET: GioHang/Cart
        public ActionResult Cart()
        {
            // TODO: Load cart items
            return View();
        }
    }
}
