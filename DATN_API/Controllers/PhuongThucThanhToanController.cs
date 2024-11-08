using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DATN_API.DTOs;
using DATN_API.DTOs.Common;
using DATN_API.Models;
using DATN_API.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhuongThucThanhToanController : ControllerBase
    {
        private readonly IAllRepositories<PhuongThucThanhToan> _ptttRepo;
        private readonly AppDbContext _context;

        public PhuongThucThanhToanController(IAllRepositories<PhuongThucThanhToan> ptttRepo, AppDbContext context)
        {
            _ptttRepo = ptttRepo;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<PhuongThucThanhToanDTO>>>> GetAllPhuongThucThanhToan()
        {
            try
            {
                var ptttList = await _ptttRepo.GetAll();
                var ptttDTOs = ptttList.Select(pt => new PhuongThucThanhToanDTO
                {
                    Id = pt.Id,
                    TenThanhToan = pt.TenThanhToan,
                    NhaCungCap = pt.NhaCungCap,
                    CreatedAt = pt.CreatedAt,
                    UpdatedAt = pt.UpdatedAt
                });

                return Ok(ApiResponse<IEnumerable<PhuongThucThanhToanDTO>>.Succeed(ptttDTOs));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<PhuongThucThanhToanDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PhuongThucThanhToanDTO>>> GetPhuongThucThanhToanById(int id)
        {
            try
            {
                var pttt = await _ptttRepo.GetById(id);
                if (pttt == null)
                    return NotFound(ApiResponse<PhuongThucThanhToanDTO>.Fail("Không tìm thấy phương thức thanh toán"));

                var ptttDTO = new PhuongThucThanhToanDTO
                {
                    Id = pttt.Id,
                    TenThanhToan = pttt.TenThanhToan,
                    NhaCungCap = pttt.NhaCungCap,
                    CreatedAt = pttt.CreatedAt,
                    UpdatedAt = pttt.UpdatedAt
                };

                return Ok(ApiResponse<PhuongThucThanhToanDTO>.Succeed(ptttDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PhuongThucThanhToanDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<PhuongThucThanhToanDTO>>> CreatePhuongThucThanhToan(CreatePhuongThucThanhToanDTO createDTO)
        {
            try
            {
                if (await _context.PhuongThucThanhToans.AnyAsync(pt => pt.TenThanhToan == createDTO.TenThanhToan))
                    return BadRequest(ApiResponse<PhuongThucThanhToanDTO>.Fail("Tên phương thức thanh toán đã tồn tại"));

                var pttt = new PhuongThucThanhToan
                {
                    TenThanhToan = createDTO.TenThanhToan,
                    NhaCungCap = createDTO.NhaCungCap
                };

                var result = await _ptttRepo.Create(pttt);
                if (!result)
                    return BadRequest(ApiResponse<PhuongThucThanhToanDTO>.Fail("Không thể tạo phương thức thanh toán"));

                var ptttDTO = new PhuongThucThanhToanDTO
                {
                    Id = pttt.Id,
                    TenThanhToan = pttt.TenThanhToan,
                    NhaCungCap = pttt.NhaCungCap,
                    CreatedAt = pttt.CreatedAt,
                    UpdatedAt = pttt.UpdatedAt
                };

                return CreatedAtAction(nameof(GetPhuongThucThanhToanById), new { id = pttt.Id },
                    ApiResponse<PhuongThucThanhToanDTO>.Succeed(ptttDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PhuongThucThanhToanDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdatePhuongThucThanhToan(int id, UpdatePhuongThucThanhToanDTO updateDTO)
        {
            try
            {
                var pttt = await _ptttRepo.GetById(id);
                if (pttt == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy phương thức thanh toán"));

                if (await _context.PhuongThucThanhToans.AnyAsync(pt =>
                    pt.TenThanhToan == updateDTO.TenThanhToan && pt.Id != id))
                    return BadRequest(ApiResponse<bool>.Fail("Tên phương thức thanh toán đã tồn tại"));

                pttt.TenThanhToan = updateDTO.TenThanhToan;
                pttt.NhaCungCap = updateDTO.NhaCungCap;

                var result = await _ptttRepo.Update(pttt);
                if (!result)
                    return BadRequest(ApiResponse<bool>.Fail("Không thể cập nhật phương thức thanh toán"));

                return Ok(ApiResponse<bool>.Succeed(true, "Cập nhật thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePhuongThucThanhToan(int id)
        {
            try
            {
                var pttt = await _ptttRepo.GetById(id);
                if (pttt == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy phương thức thanh toán"));

                if (await _context.DonHangs.AnyAsync(dh => dh.PhuongThucThanhToanId == id))
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa phương thức thanh toán đang được sử dụng"));

                var result = await _ptttRepo.Delete(id);
                if (!result)
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa phương thức thanh toán"));

                return Ok(ApiResponse<bool>.Succeed(true, "Xóa thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
    }
}