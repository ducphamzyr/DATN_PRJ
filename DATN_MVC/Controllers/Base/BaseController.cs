using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DATN_MVC.Controllers.Base
{
    // Controllers/Base/BaseController.cs
    public abstract class BaseController : Controller
    {
        protected string GetUserToken()
        {
            return HttpContext.Session.GetString("JWTToken");
        }

        protected bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }

        // Thêm public vào trước override
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(GetUserToken()) &&
                context.ActionDescriptor.DisplayName?.Contains("Auth") == false)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
