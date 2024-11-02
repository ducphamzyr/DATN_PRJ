using DATN_API;
using DATN_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DATN_MVC.Controllers
{
    public class SanPhamController : Controller
    {
        HttpClient _client;
        AppDbContext context;
        public SanPhamController()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _client = new HttpClient(handler);

        }

        public IActionResult Index()
        {
            var requetURL = $@"https://localhost:7168/api/SanPham/get-all";
            var reponse = _client.GetStringAsync(requetURL).Result;
            var data = JsonConvert.DeserializeObject<List<SanPham>>(reponse);
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPham sp)
        {
            var requetURL = $@"https://localhost:7168/api/SanPham/create";
            var reponse = _client.PostAsJsonAsync(requetURL, sp).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var requetURL = $@"https://localhost:7168/api/SanPham/get-by-id?id={id}";
            var reponse = _client.GetStringAsync(requetURL).Result;
            var data = JsonConvert.DeserializeObject<SanPham>(reponse);
            return View(data);
        }
        public IActionResult Edit(Guid id)
        {
            var requetURL = $@"https://localhost:7168/api/SanPham/get-by-id/?id={id}";
            var reponse = _client.GetStringAsync(requetURL).Result;
            var data = JsonConvert.DeserializeObject<SanPham>(reponse);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(SanPham sp)
        {
            var requetURL = $@"https://localhost:7168/api/SanPham/update";
            var reponse = _client.PutAsJsonAsync(requetURL, sp).Result;
            return RedirectToAction("Index");
        }
        public IActionResult Delete(Guid id)
        {
            var requetURL = $@"https://localhost:7168/api/SanPham/delete?id={id}";
            var reponse = _client.DeleteAsync(requetURL).Result;
            return RedirectToAction("Index");
        }


    }
}
