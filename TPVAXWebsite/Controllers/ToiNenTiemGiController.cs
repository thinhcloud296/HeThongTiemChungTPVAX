using System;
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

        public ToiNenTiemGiController()
        {
            _vaccineService = new VaccineService();
        }

        /// <summary>
        /// Hiển thị form tư vấn và danh sách vaccine gợi ý
        /// </summary>
        /// <param name="ageGroup">Nhóm tuổi (ví dụ: "6-12 tháng")</param>
        /// <param name="gender">Giới tính (Nam/Nữ)</param>
        /// <returns>View danh sách vaccine phù hợp</returns>
        public ActionResult Index(string ageGroup, string gender)
        {
            try
            {
                // Lưu lại lựa chọn để hiển thị lại trên form
                ViewBag.AgeGroup = ageGroup;
                ViewBag.Gender = gender;

                // Gọi service để lấy danh sách vaccine gợi ý
                var recommendedVaccines = _vaccineService.GetRecommendations(ageGroup, gender);

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
            }
            base.Dispose(disposing);
        }
    }
}
