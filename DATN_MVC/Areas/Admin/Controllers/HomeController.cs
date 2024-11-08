using DATN_MVC.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DATN_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
