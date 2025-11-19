using System.Web.Mvc;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý tất cả chức năng Admin
    /// </summary>
    public class AdminController : Controller
    {
        // GET: Admin/Index
        public ActionResult Index()
        {
            // TODO: Load dashboard data
            return View();
        }

        // GET: Admin/Vaccines
        public ActionResult Vaccines()
        {
            // TODO: Load danh sách vaccine
            return View();
        }

        // GET: Admin/Customers
        public ActionResult Customers()
        {
            // TODO: Load danh sách khách hàng
            return View();
        }

        // GET: Admin/Appointments
        public ActionResult Appointments()
        {
            // TODO: Load danh sách lịch hẹn
            return View();
        }

        // GET: Admin/GoiVaccine
        public ActionResult GoiVaccine()
        {
            // TODO: Load danh sách gói vaccine
            return View();
        }

        // GET: Admin/InvoiceDetails
        public ActionResult InvoiceDetails(string id)
        {
            // TODO: Load chi tiết hóa đơn
            return View();
        }

        // GET: Admin/NhaCungCap
        public ActionResult NhaCungCap()
        {
            // TODO: Load danh sách nhà cung cấp
            return View();
        }

        // GET: Admin/NhanVien
        public ActionResult NhanVien()
        {
            // TODO: Load danh sách nhân viên
            return View();
        }

        // GET: Admin/Profile
        public ActionResult Profile()
        {
            // TODO: Hiển thị profile admin
            return View();
        }

        // GET: Admin/Reports
        public ActionResult Reports()
        {
            // TODO: Hiển thị báo cáo thống kê
            return View();
        }
    }
}
