using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.DAL.Repositories;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý thông tin Bệnh truyền nhiễm
    /// </summary>
    public class BenhTruyenNhiemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BenhTruyenNhiemController()
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

        // GET: BenhTruyenNhiem/Index
        public ActionResult Index(string filter = null)
        {
            var loaiBenh = _unitOfWork.Repository<LoaiBenh>()
                .GetAll()
                .OrderBy(l => l.TenLoaiBenh)
                .ToList();

            // Apply filter if exists
            if (!string.IsNullOrEmpty(filter))
            {
                loaiBenh = loaiBenh
                    .Where(l => l.TenLoaiBenh.ToLower().Contains(filter.ToLower()))
                    .ToList();
            }

            return View(loaiBenh);
        }

        // GET: BenhTruyenNhiem/Detail/{id}
        public ActionResult Detail(string id)
        {
            var loaiBenh = _unitOfWork.Repository<LoaiBenh>().GetById(id);
            if (loaiBenh == null)
            {
                return HttpNotFound();
            }

            // Get vaccines that prevent this disease
            var vaccinePhongBenh = _unitOfWork.Repository<VaccinePhongBenh>()
                .GetAll()
                .Where(v => v.MaLoaiBenh == id)
                .ToList();

            var viewModel = new BenhTruyenNhiemDetailViewModel
            {
                LoaiBenh = loaiBenh,
                DanhSachVaccinePhongBenh = vaccinePhongBenh
            };

            return View(viewModel);
        }

        // POST: BenhTruyenNhiem/Search
        [HttpPost]
        public JsonResult Search(string keyword)
        {
            try
            {
                var result = _unitOfWork.Repository<LoaiBenh>()
                    .GetAll()
                    .Where(l => l.TenLoaiBenh.Contains(keyword) || l.MoTa.Contains(keyword))
                    .Select(l => new
                    {
                        l.MaLoaiBenh,
                        l.TenLoaiBenh,
                        l.MoTa
                    })
                    .ToList();

                return Json(new { success = true, data = result });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
