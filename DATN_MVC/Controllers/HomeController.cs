using DATN_MVC.Controllers.Base;
using DATN_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DATN_MVC.Controllers
{
    public class HomeController : UserBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}