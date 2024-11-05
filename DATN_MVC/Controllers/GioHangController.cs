using DATN_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DATN_MVC.Controllers
{
    public class GioHangController : Controller
    {
        HttpClient _client;
        public GioHangController(HttpClient client)
        {
            _client = client;
        }
        public IActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                if (username != null)
                {

                    string requestUrl = $"https://localhost:7255/api/CartDetails/get-cartdetails?id={username}";

                    var response = _client.GetStringAsync(requestUrl).Result;

                    var allCartItem = JsonConvert.DeserializeObject<List<GioHangChiTiet>>(response);

                    return View(allCartItem);

                }
                else
                {
                    return View();
                }

            }
            //else
            {
                return RedirectToAction("Login", "Accounts");
            }
        }
    }
}
