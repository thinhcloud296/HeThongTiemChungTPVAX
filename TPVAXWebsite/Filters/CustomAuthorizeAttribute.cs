using System.Web;
using System.Web.Mvc;

namespace TPVAXWebsite.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var maKH = httpContext.Session["MaKH"];
            return maKH != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/Login?returnUrl=" + 
                HttpUtility.UrlEncode(filterContext.HttpContext.Request.RawUrl));
        }
    }
}
