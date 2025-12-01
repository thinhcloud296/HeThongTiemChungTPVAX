using System.Web;
using System.Web.Mvc;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Common
{
    /// <summary>
    /// Filter kiểm tra quyền Admin (Nhân viên)
    /// Chỉ cho phép nhân viên đã đăng nhập truy cập
    /// </summary>
    public class AuthorizeAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            
            // Kiểm tra đã đăng nhập chưa
            if (session["User"] == null || session["NV"] == null)
            {
                // Chưa đăng nhập hoặc không phải nhân viên -> redirect về Login
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "Login" }
                    });
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }

    /// <summary>
    /// Filter kiểm tra quyền Khách hàng
    /// Chỉ cho phép khách hàng đã đăng nhập truy cập
    /// </summary>
    public class AuthorizeCustomerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            
            // Kiểm tra đã đăng nhập chưa
            if (session["User"] == null || session["KH"] == null)
            {
                // Chưa đăng nhập hoặc không phải khách hàng -> redirect về Login
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "Login" }
                    });
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
