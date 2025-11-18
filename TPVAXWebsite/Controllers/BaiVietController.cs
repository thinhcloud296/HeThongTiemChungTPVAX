using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý Bài viết
    /// </summary>
    public class BaiVietController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaiVietController()
        {
            _unitOfWork = new UnitOfWork(new TPVAXDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork?.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: BaiViet/Index
        public ActionResult Index(string category = null, string tag = null)
        {
            // Placeholder for now - will implement when BaiViet entity is created
            var baiViets = new System.Collections.Generic.List<BaiVietViewModel>();

            return View(baiViets);
        }

        // GET: BaiViet/Detail/{slug}
        public ActionResult Detail(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return HttpNotFound();
            }

            // Placeholder for now
            var viewModel = new BaiVietDetailViewModel
            {
                BaiViet = new BaiVietViewModel
                {
                    TieuDe = "Bài viết chi tiết",
                    NoiDung = "Nội dung bài viết..."
                }
            };

            return View(viewModel);
        }

        // POST: BaiViet/Search
        [HttpPost]
        public JsonResult Search(string keyword)
        {
            // Implement search logic
            return Json(new { success = true, data = new { } });
        }
    }
}
