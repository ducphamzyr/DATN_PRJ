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
    public class PhanLoaiController : ControllerBase
    {
        private readonly IAllRepositories<PhanLoai> _phanLoaiRepo;
        private readonly AppDbContext _context;

        public PhanLoaiController(IAllRepositories<PhanLoai> phanLoaiRepo, AppDbContext context)
        {
            _phanLoaiRepo = phanLoaiRepo;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<PhanLoaiDTO>>>> GetAllPhanLoai()
        {
            try
            {
                var phanLoais = await _phanLoaiRepo.GetAll();
                var phanLoaiDTOs = phanLoais.Select(pl => new PhanLoaiDTO
                {
                    Id = pl.Id,
                    TenPhanLoai = pl.TenPhanLoai
                });

                return Ok(ApiResponse<IEnumerable<PhanLoaiDTO>>.Succeed(phanLoaiDTOs));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<PhanLoaiDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PhanLoaiDTO>>> GetPhanLoaiById(int id)
        {
            try
            {
                var phanLoai = await _phanLoaiRepo.GetById(id);
                if (phanLoai == null)
                    return NotFound(ApiResponse<PhanLoaiDTO>.Fail("Không tìm thấy phân loại"));

                var phanLoaiDTO = new PhanLoaiDTO
                {
                    Id = phanLoai.Id,
                    TenPhanLoai = phanLoai.TenPhanLoai
                };

                return Ok(ApiResponse<PhanLoaiDTO>.Succeed(phanLoaiDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PhanLoaiDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<PhanLoaiDTO>>> CreatePhanLoai(CreatePhanLoaiDTO createDTO)
        {
            try
            {
                if (await _context.PhanLoais.AnyAsync(pl => pl.TenPhanLoai == createDTO.TenPhanLoai))
                    return BadRequest(ApiResponse<PhanLoaiDTO>.Fail("Tên phân loại đã tồn tại"));

                var phanLoai = new PhanLoai
                {
                    TenPhanLoai = createDTO.TenPhanLoai
                };

                var result = await _phanLoaiRepo.Create(phanLoai);
                if (!result)
                    return BadRequest(ApiResponse<PhanLoaiDTO>.Fail("Không thể tạo phân loại"));

                var phanLoaiDTO = new PhanLoaiDTO
                {
                    Id = phanLoai.Id,
                    TenPhanLoai = phanLoai.TenPhanLoai
                };

                return CreatedAtAction(nameof(GetPhanLoaiById), new { id = phanLoai.Id },
                    ApiResponse<PhanLoaiDTO>.Succeed(phanLoaiDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PhanLoaiDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdatePhanLoai(int id, CreatePhanLoaiDTO updateDTO)
        {
            try
            {
                var phanLoai = await _phanLoaiRepo.GetById(id);
                if (phanLoai == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy phân loại"));

                if (await _context.PhanLoais.AnyAsync(pl =>
                    pl.TenPhanLoai == updateDTO.TenPhanLoai && pl.Id != id))
                    return BadRequest(ApiResponse<bool>.Fail("Tên phân loại đã tồn tại"));

                phanLoai.TenPhanLoai = updateDTO.TenPhanLoai;

                var result = await _phanLoaiRepo.Update(phanLoai);
                if (!result)
                    return BadRequest(ApiResponse<bool>.Fail("Không thể cập nhật phân loại"));

                return Ok(ApiResponse<bool>.Succeed(true, "Cập nhật thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePhanLoai(int id)
        {
            try
            {
                var phanLoai = await _phanLoaiRepo.GetById(id);
                if (phanLoai == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy phân loại"));

                if (await _context.SanPhams.AnyAsync(sp => sp.PhanLoaiId == id))
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa phân loại đang được sử dụng"));

                var result = await _phanLoaiRepo.Delete(id);
                if (!result)
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa phân loại"));

                return Ok(ApiResponse<bool>.Succeed(true, "Xóa thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
    }
}