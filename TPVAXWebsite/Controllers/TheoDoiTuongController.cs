using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý trang Thông tin Vaccine theo Đối tượng
    /// </summary>
    public class TheoDoiTuongController : Controller
    {
        // GET: TheoDoiTuong/Index
        public ActionResult Index()
        {
            // TODO: Load thông tin vaccine theo đối tượng (Nhi, Thanh thiếu niên, Du lịch, v.v)
            return View();
        }
    }
}
