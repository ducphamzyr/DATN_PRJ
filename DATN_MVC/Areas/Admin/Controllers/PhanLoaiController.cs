using Microsoft.AspNetCore.Mvc;
using DATN_MVC.Areas.Admin.Controllers.Base;
using DATN_MVC.Services;
using DATN_MVC.Models;

namespace DATN_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhanLoaiController : AdminBaseController
    {
        private readonly IPhanLoaiService _phanLoaiService;
        private readonly ILogger<PhanLoaiController> _logger;

        public PhanLoaiController(IPhanLoaiService phanLoaiService, ILogger<PhanLoaiController> logger)
        {
            _phanLoaiService = phanLoaiService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var token = GetUserToken();
                var result = await _phanLoaiService.GetAllAsync(token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return View(new List<PhanLoaiDTO>());
                }

                return View(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index action");
                TempData["error"] = "System error!";
                return View(new List<PhanLoaiDTO>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var token = GetUserToken();
                var result = await _phanLoaiService.GetByIdAsync(id, token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                return View(result.Data);
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
            return View(new CreatePhanLoaiDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePhanLoaiDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var token = GetUserToken();
                var result = await _phanLoaiService.CreateAsync(model, token);

                if (result.Success)
                {
                    TempData["success"] = "Create category successfully!";
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
                var result = await _phanLoaiService.GetByIdAsync(id, token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                var model = new UpdatePhanLoaiDTO
                {
                    TenPhanLoai = result.Data.TenPhanLoai
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
        public async Task<IActionResult> Edit(int id, UpdatePhanLoaiDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var token = GetUserToken();
                var result = await _phanLoaiService.EditAsync(id, model, token);

                if (result.Success)
                {
                    TempData["success"] = "Update category successfully!";
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
                var result = await _phanLoaiService.DeleteAsync(id, token);

                if (result.Success)
                    TempData["success"] = "Delete category successfully!";
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