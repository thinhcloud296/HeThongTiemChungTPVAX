using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý trang Mẹ và Bé
    /// </summary>
    public class MeVaBeController : Controller
    {
        private readonly BaiVietService _service = new BaiVietService();

        public ActionResult Index()
        {
            var ds = _service.LayTheoDanhMuc("Mẹ và bé");
            return View(ds);
        }

        public ActionResult ChiTiet(int id)
        {
            var bv = _service.LayChiTiet(id);
            if (bv == null) return HttpNotFound();

            // Lấy 5 bài viết bất kỳ trong chuyên mục "Mẹ và bé"
            var lienQuan = _service.LayTheoDanhMuc("Mẹ và bé")
                                   .Where(x => x.Id != id)   // loại trừ bài hiện tại
                                   .OrderByDescending(x => x.NgayDang)
                                   .Take(5)
                                   .ToList();

            ViewBag.LienQuan = lienQuan;

            return View(bv);
        }

    }

}
