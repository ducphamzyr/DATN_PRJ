using Microsoft.AspNetCore.Mvc;
using DATN_MVC.Controllers.Base;
using DATN_MVC.Services;
using DATN_MVC.Models;

namespace DATN_MVC.Controllers
{
    public class PhanQuyenController : UserBaseController
    {
        private readonly IPhanQuyenService _phanQuyenService;
        private readonly ILogger<PhanQuyenController> _logger;

        public PhanQuyenController(IPhanQuyenService phanQuyenService, ILogger<PhanQuyenController> logger)
        {
            _phanQuyenService = phanQuyenService;
            _logger = logger;
        }

        // GET: PhanQuyen
        public async Task<IActionResult> Index()
        {
            try
            {
                var token = GetUserToken();
                _logger.LogInformation("Getting current user role");

                var result = await _phanQuyenService.LayQuyenCuaToiAsync(token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return View(null);
                }

                // Lấy thông tin chi tiết về quyền
                if (result.Data != null)
                {
                    var detailResult = await _phanQuyenService.GetByIdAsync(result.Data.Id, token);
                    if (detailResult.Success)
                    {
                        return View(detailResult.Data);
                    }
                }

                return View(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PhanQuyen Index");
                TempData["error"] = "Lỗi hệ thống!";
                return View(null);
            }
        }

        // GET: PhanQuyen/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var token = GetUserToken();
                var result = await _phanQuyenService.GetByIdAsync(id, token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                if (result.Data is not PhanQuyenDetailDTO detailDTO)
                {
                    TempData["error"] = "Không thể lấy thông tin chi tiết phân quyền";
                    return RedirectToAction(nameof(Index));
                }

                return View(detailDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PhanQuyen Details for ID: {Id}", id);
                TempData["error"] = "Lỗi hệ thống!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}