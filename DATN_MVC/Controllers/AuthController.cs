using DATN_MVC.Models;
using DATN_MVC.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

public class AuthController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        if (httpClientFactory == null)
            throw new ArgumentNullException(nameof(httpClientFactory));

        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        var baseUrl = _configuration["ApiSettings:BaseUrl"];
        if (string.IsNullOrEmpty(baseUrl))
            throw new InvalidOperationException("API Base URL is not configured");

        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    [HttpGet]
    public IActionResult Login()
    {
        // Nếu đã đăng nhập, chuyển hướng theo role
        if (HttpContext.Session.GetString("JWTToken") != null)
        {
            return RedirectBasedOnRole();
        }
        return View(new LoginModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var loginData = new
            {
                TenDangNhap = model.Username,
                MatKhau = model.Password
            };

            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();

                if (result.Success)
                {
                    // Lưu thông tin vào session
                    HttpContext.Session.SetString("JWTToken", result.Data.Token);
                    HttpContext.Session.SetString("UserRole", result.Data.TenPhanQuyen);
                    HttpContext.Session.SetString("UserName", result.Data.TenKhachHang);
                    HttpContext.Session.SetString("UserId", result.Data.Id.ToString());

                    return RedirectBasedOnRole();
                }

                ModelState.AddModelError("", result.Message);
            }
            else
            {
                ModelState.AddModelError("", "Đăng nhập không thành công");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Lỗi: {ex.Message}");
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    private IActionResult RedirectBasedOnRole()
    {
        var role = HttpContext.Session.GetString("UserRole");
        return role == "Admin"
            ? RedirectToAction("Index", "Home", new { area = "Admin" })
            : RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
