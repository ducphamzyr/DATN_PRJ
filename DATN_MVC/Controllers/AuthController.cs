using Microsoft.AspNetCore.Mvc;
using DATN_MVC.Models.Auth;
using DATN_MVC.Services;
using System.Text.Json;
using DATN_MVC.Models;

namespace DATN_MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthController(ISessionService sessionService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _sessionService = sessionService;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
        }

        #region Login
        public IActionResult Login(string returnUrl = null)
        {
            if (_sessionService.IsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loginData = new
                    {
                        TenDangNhap = model.Username,
                        MatKhau = model.Password
                    };
                    Console.WriteLine("POST DATA");
                    var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginData);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("API OK RUI NE");
                        var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();

                        if (result.Success)
                        {
                            _sessionService.SaveLoginInfo(
                                result.Data.Token,
                                result.Data.TenKhachHang,
                                result.Data.TenPhanQuyen
                            );

                            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            return RedirectToAction("Index", "Home");
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
            }
            return View(model);
        }
        #endregion

        #region Register
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
                        TenDangNhap = model.Username,
                        MatKhau = model.Password,
                        TenKhachHang = model.FullName,
                        Email = model.Email,
                        SoDienThoai = model.PhoneNumber,
                        DiaChi = model.Address
                    };

                    var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerData);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();

                        if (result.Success)
                        {
                            _sessionService.SaveLoginInfo(
                                result.Data.Token,
                                result.Data.TenKhachHang,
                                result.Data.TenPhanQuyen
                            );

                            TempData["SuccessMessage"] = "Đăng ký thành công!";
                            return RedirectToAction("Index", "Home");
                        }
                        ModelState.AddModelError("", result.Message);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                }
            }
            return View(model);
        }
        #endregion

        #region Logout
        [HttpPost]
        public IActionResult Logout()
        {
            _sessionService.ClearLoginInfo();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}