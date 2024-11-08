using DATN_MVC.Controllers.Base;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DATN_MVC.Areas.Admin.Attributes;

namespace DATN_MVC.Areas.Admin.Controllers.Base
{
    [Area("Admin")]
    [AdminAreaAuthorization]
    public abstract class AdminBaseController : Controller
    {
        protected string GetUserToken()
        {
            return HttpContext.Session.GetString("JWTToken");
        }

        protected int GetUserId()
        {
            return int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
        }
    }
}
