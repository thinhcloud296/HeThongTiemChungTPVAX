using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.DAL.Repositories;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý Gói Vắc xin
    /// </summary>
    public class GoiVaccineController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GoiVaccineController()
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

        // GET: GoiVaccine/Index
        public ActionResult Index()
        {
            var goiVaccines = _unitOfWork.Repository<GoiVaccine>()
                .GetAll()
                .OrderBy(g => g.TenGoi)
                .ToList();

            return View(goiVaccines);
        }

        // GET: GoiVaccine/Detail/{id}
        public ActionResult Detail(string id)
        {
            var goiVaccine = _unitOfWork.Repository<GoiVaccine>().GetById(id);
            if (goiVaccine == null)
            {
                return HttpNotFound();
            }

            var chiTiet = _unitOfWork.Repository<ChiTietGoiVaccine>()
                .GetAll()
                .Where(c => c.MaGoi == id)
                .ToList();

            var viewModel = new GoiVaccineDetailViewModel
            {
                GoiVaccine = goiVaccine,
                ChiTietGoiVaccine = chiTiet
            };

            return View(viewModel);
        }

        // POST: GoiVaccine/DatGoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DatGoi(string maGoi, int soLuong = 1)
        {
            try
            {
                var goiVaccine = _unitOfWork.Repository<GoiVaccine>().GetById(maGoi);
                if (goiVaccine == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy gói vắc xin!" });
                }

                // Add to cart logic here
                // ...

                return Json(new { success = true, message = "Đã thêm gói vào giỏ hàng!" });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }
    }
}
