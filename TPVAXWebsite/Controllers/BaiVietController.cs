using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý Bài viết / Kiến thức tiêm chủng
    /// </summary>
    public class BaiVietController : Controller
    {
        private readonly BaiVietService _service = new BaiVietService();

        // GET: BaiViet/Index
        public ActionResult Index(string category = null, string tag = null)
        {
            var baiViets = _service.LayTatCa();
            
            // Lọc theo danh mục nếu có
            if (!string.IsNullOrEmpty(category))
            {
                baiViets = baiViets.Where(b => b.DanhMuc == category).ToList();
            }
            
            // Lọc theo tag nếu có
            if (!string.IsNullOrEmpty(tag))
            {
                baiViets = baiViets.Where(b => !string.IsNullOrEmpty(b.Tag) && b.Tag.Contains(tag)).ToList();
            }

            ViewBag.Category = category;
            ViewBag.Tag = tag;
            
            return View(baiViets);
        }

        // GET: BaiViet/Detail/{id}
        public ActionResult Detail(int id)
        {
            var baiViet = _service.LayChiTiet(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }

            // Lấy bài viết liên quan
            var lienQuan = _service.LayTatCa()
                .Where(b => b.Id != id)
                .OrderByDescending(b => b.NgayDang)
                .Take(5)
                .ToList();

            ViewBag.LienQuan = lienQuan;

            return View(baiViet);
        }
    }
}
