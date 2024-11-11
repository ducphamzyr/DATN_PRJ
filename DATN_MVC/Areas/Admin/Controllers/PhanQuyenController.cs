using Microsoft.AspNetCore.Mvc;
using DATN_MVC.Areas.Admin.Controllers.Base;
using DATN_MVC.Services;
using DATN_MVC.Models;

namespace DATN_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhanQuyenController : AdminBaseController
    {
        private readonly IPhanQuyenService _phanQuyenService;
        private readonly ILogger<PhanQuyenController> _logger;

        public PhanQuyenController(IPhanQuyenService phanQuyenService, ILogger<PhanQuyenController> logger)
        {
            _phanQuyenService = phanQuyenService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var token = GetUserToken();
                var result = await _phanQuyenService.GetAllAsync(token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return View(new List<PhanQuyenDTO>());
                }

                // Get statistics for dashboard
                var statsResult = await _phanQuyenService.LayThongKeAsync(token);
                if (statsResult.Success)
                {
                    ViewBag.Statistics = statsResult.Data;
                }

                return View(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index action");
                TempData["error"] = "System error!";
                return View(new List<PhanQuyenDTO>());
            }
        }

        [HttpGet]
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
                    TempData["error"] = "Invalid data format";
                    return RedirectToAction(nameof(Index));
                }

                return View(detailDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Details action for ID: {Id}", id);
                TempData["error"] = "System error!";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View(new CreatePhanQuyenDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePhanQuyenDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var token = GetUserToken();
                var result = await _phanQuyenService.CreateAsync(model, token);

                if (result.Success)
                {
                    TempData["success"] = "Create role successfully!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["error"] = result.Message;
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create action");
                TempData["error"] = "System error!";
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
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

                var model = new UpdatePhanQuyenDTO
                {
                    TenPhanQuyen = result.Data.TenPhanQuyen
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Edit action for ID: {Id}", id);
                TempData["error"] = "System error!";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdatePhanQuyenDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var token = GetUserToken();
                var result = await _phanQuyenService.EditAsync(id, model, token);

                if (result.Success)
                {
                    TempData["success"] = "Update role successfully!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["error"] = result.Message;
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Edit action for ID: {Id}", id);
                TempData["error"] = "System error!";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var token = GetUserToken();

                // Check if role is Admin
                var roleResult = await _phanQuyenService.GetByIdAsync(id, token);
                if (roleResult.Success && roleResult.Data?.TenPhanQuyen == "Admin")
                {
                    TempData["error"] = "Cannot delete Admin role!";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _phanQuyenService.DeleteAsync(id, token);

                if (result.Success)
                    TempData["success"] = "Delete role successfully!";
                else
                    TempData["error"] = result.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete action for ID: {Id}", id);
                TempData["error"] = "System error!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}