using Microsoft.AspNetCore.Mvc;
using DATN_MVC.Controllers.Base;
using DATN_MVC.Services;
using DATN_MVC.Models.PhanQuyen;

namespace DATN_MVC.Controllers
{
    public class PhanQuyenController : UserBaseController
    {
        private readonly IPhanQuyenService _phanQuyenService;
        private readonly ILogger<PhanQuyenController> _logger;

        public PhanQuyenController(IPhanQuyenService phanQuyenService,ILogger<PhanQuyenController> logger)
        {
            _phanQuyenService = phanQuyenService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var token = GetUserToken();
                _logger.LogInformation($"Token: {token}");
                var result = await _phanQuyenService.LayQuyenCuaToiAsync(token);

                if (result.Success)
                    return View(result.Data);

                TempData["error"] = result.Message;
                return View(null);
            }
            catch
            {
                TempData["error"] = "Lỗi hệ thống!";
                return View(null);
            }
        }

        public async Task<IActionResult> DanhSach()
        {
            try
            {
                var token = GetUserToken();
                var result = await _phanQuyenService.GetAllAsync(token);

                if (result.Success)
                    return View(result.Data);

                TempData["error"] = result.Message;
                return View(new List<PhanQuyenDTO>());
            }
            catch
            {
                TempData["error"] = "Lỗi hệ thống!";
                return View(new List<PhanQuyenDTO>());
            }
        }
    }
}