using Microsoft.AspNetCore.Mvc;
using DATN_MVC.Areas.Admin.Controllers.Base;
using DATN_MVC.Services;
using DATN_MVC.Models.PhanQuyen;


namespace DATN_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhanQuyenController : AdminBaseController
    {
        private readonly IPhanQuyenService _phanQuyenService;

        public PhanQuyenController(IPhanQuyenService phanQuyenService)
        {
            _phanQuyenService = phanQuyenService;
        }

        public async Task<IActionResult> Index()
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
            catch (Exception ex)
            {
                TempData["error"] = "System error!";
                return View(new List<PhanQuyenDTO>());
            }
        }
        [HttpPost]
[ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var token = GetUserToken();
                // Chắc chắn rằng API trả về PhanQuyenDetailDTO bao gồm DanhSachTaiKhoan
                var result = await _phanQuyenService.GetByIdAsync(id, token);

                if (result.Success)
                {
                    if (result.Data is PhanQuyenDetailDTO detailDTO)
                        return View(detailDTO);

                    TempData["error"] = "Invalid data format";
                    return RedirectToAction(nameof(Index));
                }

                TempData["error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = "System error!";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View(new CreatePhanQuyenDTO());
        }

        [HttpPost]
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
            catch
            {
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

                if (result.Success)
                {
                    var model = new UpdatePhanQuyenDTO
                    {
                        TenPhanQuyen = result.Data.TenPhanQuyen
                    };
                    return View(model);
                }

                TempData["error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "System error!";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
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
            catch
            {
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
                var result = await _phanQuyenService.DeleteAsync(id, token);

                if (result.Success)
                    TempData["success"] = "Delete role successfully!";
                else
                    TempData["error"] = result.Message;
            }
            catch
            {
                TempData["error"] = "System error!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}