using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller chính cho trang chủ và các trang thông tin
    /// </summary>
    public class HomeController : Controller
    {
        // TODO: Implement trang chủ với danh sách vaccine nổi bật, khuyến mãi
        public ActionResult Index()
        {
            // Load featured vaccines, promotions
            return View();
        }

        // TODO: Implement trang giới thiệu
        public ActionResult About()
        {
            return View();
        }

        // TODO: Implement trang liên hệ
        public ActionResult Contact()
        {
            return View();
        }

        // TODO: Implement trang thông tin bệnh truyền nhiễm
        public ActionResult BenhTruyenNhiem()
        {
            // Load danh sách bệnh truyền nhiễm
            return View();
        }

        // TODO: Implement trang tư vấn "Tôi nên tiêm gì?"
        public ActionResult ToiNenTiemGi()
        {
            // Logic tư vấn vaccine theo độ tuổi, nhóm đối tượng
            return View();
        }
    }
}
