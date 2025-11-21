using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Models.ViewModels;
using TPVAXWebsite.Services;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý vaccine và gói vaccine
    /// </summary>
    public class VaccineController : Controller
    {
        private readonly VaccineService _vaccineService;

        public VaccineController()
        {
            _vaccineService = new VaccineService();
        }

        // GET: Vaccine/VC00000001 (Chi tiết vaccine)
        public ActionResult Index(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    // Nếu không có id, chuyển về trang danh sách tất cả vaccine
                    return RedirectToAction("Index", "VaccinePhongBenh");
                }

                var vaccineModel = _vaccineService.GetVaccineDetail(id);
                if (vaccineModel == null)
                {
                    ViewBag.Error = "Không tìm thấy vaccine";
                    return RedirectToAction("Index", "VaccinePhongBenh");
                }
                return View(vaccineModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi tải thông tin vaccine: " + ex.Message;
                return RedirectToAction("Index", "VaccinePhongBenh");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vaccineService?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
