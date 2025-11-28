using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Services;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý trang Tôi Nên Tiêm Gì - tư vấn vaccine theo độ tuổi và giới tính
    /// </summary>
    public class ToiNenTiemGiController : Controller
    {
        private readonly VaccineService _vaccineService;
        private readonly DAL.IUnitOfWork _unitOfWork;

        public ToiNenTiemGiController()
        {
            _vaccineService = new VaccineService();
            _unitOfWork = new DAL.UnitOfWork(new DAL.TPVAXDbContext());
        }

        /// <summary>
        /// Hiển thị form tư vấn và danh sách vaccine gợi ý
        /// </summary>
        /// <param name="ageGroup">Nhóm tuổi (ví dụ: "6-12 tháng")</param>
        /// <param name="gender">Giới tính (Nam/Nữ)</param>
        /// <returns>View danh sách vaccine phù hợp</returns>
        public ActionResult Index(string loaiVaccine, string loaiBenh, string search)
        {
            try
            {
                // Load danh sách loại vaccine từ database
                var loaiVaccines = _unitOfWork.LoaiVaccines.GetAll()
                    .OrderBy(lv => lv.TenLoai)
                    .ToList();

                // Load danh sách loại bệnh từ database
                var loaiBenhs = _unitOfWork.LoaiBenhs.GetAll()
                    .OrderBy(lb => lb.TenBenh)
                    .ToList();

                // Lưu lại lựa chọn để hiển thị lại trên form
                ViewBag.LoaiVaccine = loaiVaccine;
                ViewBag.LoaiBenh = loaiBenh;
                ViewBag.Search = search;
                ViewBag.LoaiVaccines = loaiVaccines;
                ViewBag.LoaiBenhs = loaiBenhs;

                // Gọi service để lấy danh sách vaccine gợi ý
                var recommendedVaccines = _vaccineService.GetRecommendations(loaiVaccine, loaiBenh, search);

                return View(recommendedVaccines);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Hiển thị chi tiết một vaccine cụ thể
        /// </summary>
        /// <param name="id">Mã vaccine</param>
        /// <returns>View chi tiết vaccine</returns>
        public ActionResult Detail(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index");
                }

                var vaccine = _vaccineService.GetVaccineDetail(id);


                if (vaccine == null)
                {
                    ViewBag.Error = "Không tìm thấy vaccine với mã: " + id;
                    return View("Error");
                }

                return View(vaccine);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message;
                return View("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vaccineService?.Dispose();
                _unitOfWork?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
