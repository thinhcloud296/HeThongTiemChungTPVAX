using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý trang Bệnh truyền nhiễm
    /// </summary>
    public class BenhTruyenNhiemController : Controller
    {
        private readonly BaiVietService _service = new BaiVietService();

        public ActionResult Index()
        {
            var ds = _service.LayTheoDanhMuc("Bệnh truyền nhiễm");
            return View(ds);
        }

        public ActionResult ChiTiet(int id)
        {
            var bv = _service.LayChiTiet(id);
            if (bv == null) return HttpNotFound();

            var lienQuan = _service.LayTheoDanhMuc("Bệnh truyền nhiễm")
                                   .Where(x => x.Id != id)
                                   .OrderByDescending(x => x.NgayDang)
                                   .Take(5)
                                   .ToList();

            ViewBag.LienQuan = lienQuan;

            return View(bv);
        }

    }
}
