using DATN_MVC.Models;
using DATN_MVC.Models.Auth;
using DATN_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

public class AuthController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ISessionService _sessionService;

    public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ISessionService sessionService)
    {
        if (httpClientFactory == null)
            throw new ArgumentNullException(nameof(httpClientFactory));

        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _sessionService = sessionService;

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
        if (ModelState.IsValid)
        {
            try
            {
                var loginData = new
                {
                    TenDangNhap = model.TenDangNhap,
                    MatKhau = model.MatKhau     
                };

                var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginData);

                // Parse response content
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
                else
                {
                    // Add API error message to ModelState
                    ModelState.AddModelError("", result.Message);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi hệ thống: {ex.Message}");
            }
        }
        return View(model);
    }
    [HttpGet]
    public IActionResult Register()
    {
        if (_sessionService.IsLoggedIn())
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var registerData = new
                {
                    TenDangNhap = model.TenDangNhap,
                    MatKhau = model.MatKhau,
                    XacNhanMatKhau = model.XacNhanMatKhau,
                    TenKhachHang = model.TenKhachHang,
                    Email = model.Email,
                    SoDienThoai = model.SoDienThoai,
                    DiaChi = model.DiaChi
                };

                var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerData);

                // Parse response content
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
                else
                {
                    // Add API error message to ModelState
                    ModelState.AddModelError("", result.Message);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi hệ thống: {ex.Message}");
            }
        }
        return View(model);
    }
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/reset-password", new { Email = model.Email });
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();

                if (result.Success)
                {
                    TempData["SuccessMessage"] = "Mật khẩu đã được reset thành: 123456";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", result.Message ?? "Không thể reset mật khẩu");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
            }
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
