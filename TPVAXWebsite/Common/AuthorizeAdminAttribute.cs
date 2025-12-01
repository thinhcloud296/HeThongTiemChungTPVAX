using System.Linq;
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
                // Lưu URL hiện tại để redirect sau khi đăng nhập
                var returnUrl = filterContext.HttpContext.Request.Url?.PathAndQuery;
                
                // Chưa đăng nhập hoặc không phải nhân viên -> redirect về Login
                filterContext.HttpContext.Session["ReturnUrl"] = returnUrl;
                filterContext.HttpContext.Response.StatusCode = 401;
                
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
                // Lưu URL hiện tại để redirect sau khi đăng nhập
                var returnUrl = filterContext.HttpContext.Request.Url?.PathAndQuery;
                filterContext.HttpContext.Session["ReturnUrl"] = returnUrl;
                
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

    /// <summary>
    /// Filter kiểm tra quyền theo chức vụ cụ thể
    /// Ví dụ: [AuthorizeRole(1)] chỉ cho Quản lý truy cập
    /// ChucVu: 1=Quản lý, 2=Tiếp nhận, 3=Kho, 4=Y tế, 5=Thu ngân
    /// </summary>
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        private readonly int[] _allowedRoles;

        public AuthorizeRoleAttribute(params int[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            
            // Kiểm tra đã đăng nhập với tài khoản nhân viên chưa
            if (session["User"] == null || session["NV"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "Login" }
                    });
                return;
            }

            // Kiểm tra chức vụ
            var chucVu = session["ChucVu"] as int?;
            if (chucVu == null || (_allowedRoles.Length > 0 && !_allowedRoles.Contains(chucVu.Value)))
            {
                // Không có quyền truy cập -> hiển thị trang 403 hoặc redirect
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/AccessDenied.cshtml"
                };
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
