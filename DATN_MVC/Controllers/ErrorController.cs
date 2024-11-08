using Microsoft.AspNetCore.Mvc;

namespace DATN_MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("NotFound");
                case 403:
                    return View("Forbidden");
                default:
                    return View("Error");
            }
        }
    }
}
