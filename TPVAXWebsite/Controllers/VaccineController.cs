using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý vaccine và gói vaccine
    /// </summary>
    public class VaccineController : Controller
    {
        // TODO: Inject services

        // TODO: Danh sách vaccine với filter và search
        public ActionResult Index(string search, string loaiBenh, string loaiVaccine)
        {
            // Load danh sách vaccine
            // Filter theo: tên, loại bệnh, loại vaccine
            // Pagination
            return View();
        }

        // TODO: Chi tiết vaccine
        public ActionResult Details(string id)
        {
            // Load thông tin vaccine
            // Load loại vaccine
            // Load các bệnh phòng chống
            // Load các gói vaccine có chứa vaccine này
            return View();
        }

        // TODO: Danh sách vaccine theo đối tượng (trẻ em, người lớn, phụ nữ mang thai...)
        public ActionResult TheoDoiTuong(string doiTuong)
        {
            // Load danh sách bệnh theo nhóm đối tượng
            // Load vaccine tương ứng
            return View();
        }

        // TODO: Danh sách gói vaccine
        public ActionResult GoiVaccine()
        {
            // Load danh sách gói vaccine đang áp dụng
            return View();
        }

        // TODO: Chi tiết gói vaccine
        public ActionResult GoiVaccineDetail(string id)
        {
            // Load thông tin gói vaccine
            // Load chi tiết các vaccine trong gói
            // Hiển thị phác đồ tiêm
            return View();
        }
    }
}
