using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý lịch tiêm chủng
    /// </summary>
    public class LichTiemController : Controller
    {
        // GET: LichTiem/Index
        public ActionResult Index()
        {
            // TODO: Hiển thị danh sách lịch tiêm
            return View();
        }

        // GET: LichTiem/DatLich
        public ActionResult DatLich(string maVC)
        {
            // TODO: Hiển thị form đặt lịch
            return View();
        }
    }
}
