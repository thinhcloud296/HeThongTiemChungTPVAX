using System.Web.Mvc;

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
            // TODO: Implement login logic với DAL + Services
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPost()
        {
            // TODO: Implement login logic với DAL + Services
            return View("Login");
        }

        // GET: Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            // TODO: Implement register logic với DAL + Services
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterPost()
        {
            // TODO: Implement register logic với DAL + Services
            return View("Register");
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            // TODO: Clear session với Services
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Dashboard
        public ActionResult Dashboard()
        {
            // TODO: Check login and load data với DAL + Services
            return View();
        }
    }
}
