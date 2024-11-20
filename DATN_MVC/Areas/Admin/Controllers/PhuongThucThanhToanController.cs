using DATN_MVC.Areas.Admin.Controllers.Base;
using DATN_MVC.Models.PhuongThucThanhToan;
using DATN_MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhuongThucThanhToanController : AdminBaseController
    {
        private readonly IPhuongThucThanhToanService _PhuongThucThanhToanService;
        private readonly ILogger<PhuongThucThanhToanController> _logger;

        public PhuongThucThanhToanController(IPhuongThucThanhToanService phuongThucThanhToanService, ILogger<PhuongThucThanhToanController> logger)
        {
            _PhuongThucThanhToanService = phuongThucThanhToanService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var token = GetUserToken();
                var result = await _PhuongThucThanhToanService.GetAllAsync(token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return View(new List<PhuongThucThanhToanDTO>());
                }

                var statsResult = await _PhuongThucThanhToanService.LayThongKeAsync(token);
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
                return View(new List<PhuongThucThanhToanDTO>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var token = GetUserToken();
                var result = await _PhuongThucThanhToanService.GetByIdAsync(id, token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return RedirectToAction("Index");
                }

                return View(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Details action");
                TempData["error"] = "System error!";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Create()
        {
            return View(new CreatePhuongThucThanhToanDTO_MVC());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePhuongThucThanhToanDTO_MVC model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var token = GetUserToken();
                var result = await _PhuongThucThanhToanService.CreateAsync(model, token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return View(model);
                }

                TempData["success"] = "Thêm phương thức thanh toán thành công";
                return RedirectToAction("Index");
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
                var result = await _PhuongThucThanhToanService.GetByIdAsync(id, token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return RedirectToAction("Index");
                }

                return View(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Edit action");
                TempData["error"] = "System error!";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdatePhuongThucThanhToanDTO_MVC model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var token = GetUserToken();
                var result = await _PhuongThucThanhToanService.EditAsync(id, model, token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return View(model);
                }

                TempData["success"] = "Cập nhật phương thức thanh toán thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Edit action");
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
                var result = await _PhuongThucThanhToanService.DeleteAsync(id, token);

                if (!result.Success)
                {
                    TempData["error"] = result.Message;
                    return RedirectToAction("Index");
                }

                TempData["success"] = "Xóa phương thức thanh toán thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete action");
                TempData["error"] = "System error!";
                return RedirectToAction("Index");
            }
        }
    }
}
