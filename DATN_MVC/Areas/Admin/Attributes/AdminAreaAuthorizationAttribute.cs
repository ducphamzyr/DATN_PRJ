using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DATN_MVC.Areas.Admin.Attributes
{
    public class AdminAreaAuthorizationAttribute : TypeFilterAttribute
    {
        public AdminAreaAuthorizationAttribute() : base(typeof(AdminAreaAuthorizationFilter))
        {
        }
    }

    public class AdminAreaAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var role = context.HttpContext.Session.GetString("UserRole");
            var token = context.HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            if (role != "Admin")
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Auth", null);
                return;
            }
        }
    }
}
