using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý trang Thông tin Vaccine theo Đối tượng
    /// </summary>
    public class TheoDoiTuongController : Controller
    {
        private readonly BaiVietService _service = new BaiVietService();

        public ActionResult Index()
        {
            var ds = _service.LayTheoDanhMuc("Theo đối tượng");
            return View(ds);
        }

        public ActionResult ChiTiet(int id)
        {
            var bv = _service.LayChiTiet(id);
            if (bv == null) return HttpNotFound();

            // Lấy 5 bài viết khác trong chuyên mục "Theo đối tượng"
            var lienQuan = _service.LayTheoDanhMuc("Theo đối tượng")
                                   .Where(x => x.Id != id)
                                   .OrderByDescending(x => x.NgayDang)
                                   .Take(5)
                                   .ToList();

            ViewBag.LienQuan = lienQuan;

            return View(bv);
        }

    }

}
