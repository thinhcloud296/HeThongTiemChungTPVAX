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

        // GET: Vaccine/Index
        public ActionResult Index(string search, string loaiBenh)
        {
            try
            {
                // Load danh sách vaccine từ database
                var vaccines = _vaccineService.SearchAndFilter(search, null, loaiBenh);

                // Truyền dữ liệu sang View
                ViewBag.Search = search;
                ViewBag.LoaiBenh = loaiBenh;
                ViewBag.TotalCount = vaccines.Count();

                return View(vaccines);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi tải danh sách vaccine: " + ex.Message;
                return View();
            }
        }

        // GET: Vaccine/Detail/VCxxx
        public ActionResult Detail(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index");

                var vaccine = _vaccineService.GetById(id);
                if (vaccine == null)
                {
                    ViewBag.Error = "Không tìm thấy vaccine";
                    return View();
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
                return View();
            }
        }

        // GET: Vaccine/ChiTiet/VCxxx
        public ActionResult ChiTiet(string id)
        {
            // Redirect to Detail action
            return RedirectToAction("Detail", new { id });
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
