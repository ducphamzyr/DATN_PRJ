using DATN_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DATN_MVC.Controllers
{
    public class GioHangController : Controller
    {
        private readonly HttpClient _httpClient;

        public GioHangController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7255");
        }

        // Hiển thị danh sách giỏ hàng
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("GioHang/get-all");
            if (!response.IsSuccessStatusCode) return View("Error");

            var jsonData = await response.Content.ReadAsStringAsync();
            var gioHangs = JsonConvert.DeserializeObject<List<GioHang>>(jsonData);

            return View(gioHangs);
        }

        // Hiển thị chi tiết giỏ hàng
        public async Task<IActionResult> Details(long id)
        {
            var response = await _httpClient.GetAsync($"GioHang/get-by-id/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonData = await response.Content.ReadAsStringAsync();
            var gioHang = JsonConvert.DeserializeObject<GioHang>(jsonData);

            return View(gioHang);
        }

        // Trang thêm mới giỏ hàng
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(gioHang), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("GioHang/create", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(gioHang);
        }

        // Trang cập nhật giỏ hàng
        public async Task<IActionResult> Edit(long id)
        {
            var response = await _httpClient.GetAsync($"GioHang/get-by-id/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonData = await response.Content.ReadAsStringAsync();
            var gioHang = JsonConvert.DeserializeObject<GioHang>(jsonData);

            return View(gioHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, GioHang gioHang)
        {
            if (id != gioHang.GioHangID) return BadRequest();

            if (ModelState.IsValid)
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(gioHang), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"GioHang/update/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(gioHang);
        }

        // Xóa giỏ hàng
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _httpClient.GetAsync($"GioHang/get-by-id/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonData = await response.Content.ReadAsStringAsync();
            var gioHang = JsonConvert.DeserializeObject<GioHang>(jsonData);

            return View(gioHang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var response = await _httpClient.DeleteAsync($"GioHang/delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error");
        }
    }
}
