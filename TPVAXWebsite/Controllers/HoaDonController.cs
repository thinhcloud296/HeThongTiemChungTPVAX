using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý hóa đơn và thanh toán
    /// </summary>
    public class HoaDonController : Controller
    {
        // GET: HoaDon/Index
        public ActionResult Index()
        {
            // TODO: Load danh sách hóa đơn
            return View();
        }

        // GET: HoaDon/Checkout
        public ActionResult Checkout()
        {
            // TODO: Hiển thị trang thanh toán
            return View();
        }
    }
}
