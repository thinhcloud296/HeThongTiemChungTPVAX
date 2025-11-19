using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller chính cho trang chủ và các trang thông tin
    /// </summary>
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            // TODO: Load featured vaccines, promotions
            return View();
        }

        // GET: Home/About
        public ActionResult About()
        {
            return View();
        }

        // GET: Home/Contact
        public ActionResult Contact()
        {
            return View();
        }
    }
}
