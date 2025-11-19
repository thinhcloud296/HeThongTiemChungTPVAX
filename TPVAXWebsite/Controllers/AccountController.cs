using System.Web.Mvc;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller xử lý đăng ký, đăng nhập, quản lý tài khoản khách hàng
    /// </summary>
    public class AccountController : Controller
    {
        // GET: Account/Login
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            // TODO: Clear session
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Dashboard
        public ActionResult Dashboard()
        {
            // TODO: Check login and load data
            return View();
        }
    }
}
