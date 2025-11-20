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

                var vaccine = _vaccineService.GetById(id);
                if (vaccine == null)
                {
                    ViewBag.Error = "Không tìm thấy vaccine";
                    return RedirectToAction("Index", "VaccinePhongBenh");
                }

                // Tạo ViewModel
                var viewModel = new VaccineDetailViewModel
                {
                    Vaccine = new VaccineDetailViewModel.VaccineInfo
                    {
                        MaVC = vaccine.MaVC,
                        TenVaccine = vaccine.TenVC,
                        GiaBan = vaccine.GiaBan,
                        SoLuongTon = vaccine.SoLuongTon,
                        SoMuiToiDa = vaccine.SoMuiToiDa,
                        SoThangCho = vaccine.SoThangCho,
                        MoTa = vaccine.MoTa,
                        HinhAnh = vaccine.HinhAnh,
                        TenLoaiVaccine = vaccine.LoaiVaccine?.TenLoai
                    },
                    CacBenhPhong = _vaccineService.GetDiseasesByVaccine(id)
                        .Select(b => b.TenBenh)
                        .ToList(),
                    VaccinesLienQuan = _vaccineService.GetRelatedVaccines(id, 4)
                        .ToList()
                };

                return View(viewModel);
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
