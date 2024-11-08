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
    public class NhanHieuController : ControllerBase
    {
        private readonly IAllRepositories<NhanHieu> _nhanHieuRepo;
        private readonly AppDbContext _context;

        public NhanHieuController(IAllRepositories<NhanHieu> nhanHieuRepo, AppDbContext context)
        {
            _nhanHieuRepo = nhanHieuRepo;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous] // CHO PHÉP POST ẨN DANH , KHÔNG CẦN TOKEN
        public async Task<ActionResult<ApiResponse<IEnumerable<NhanHieuDTO>>>> GetAllNhanHieu()
        {
            try
            {
                var nhanHieus = await _nhanHieuRepo.GetAll();
                var nhanHieuDTOs = nhanHieus.Select(nh => new NhanHieuDTO
                {
                    Id = nh.Id,
                    TenNhanHieu = nh.TenNhanHieu,
                    XuatXu = nh.XuatXu
                });

                return Ok(ApiResponse<IEnumerable<NhanHieuDTO>>.Succeed(nhanHieuDTOs));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<NhanHieuDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<NhanHieuDTO>>> GetNhanHieuById(int id)
        {
            try
            {
                var nhanHieu = await _nhanHieuRepo.GetById(id);
                if (nhanHieu == null)
                    return NotFound(ApiResponse<NhanHieuDTO>.Fail("Không tìm thấy nhãn hiệu"));

                var nhanHieuDTO = new NhanHieuDTO
                {
                    Id = nhanHieu.Id,
                    TenNhanHieu = nhanHieu.TenNhanHieu,
                    XuatXu = nhanHieu.XuatXu
                };

                return Ok(ApiResponse<NhanHieuDTO>.Succeed(nhanHieuDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<NhanHieuDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // YÊU CẦU QUYỀN ADMIN ĐỂ LÀM
        public async Task<ActionResult<ApiResponse<NhanHieuDTO>>> CreateNhanHieu(CreateNhanHieuDTO createDTO)
        {
            try
            {
                if (await _context.NhanHieus.AnyAsync(nh => nh.TenNhanHieu == createDTO.TenNhanHieu))
                    return BadRequest(ApiResponse<NhanHieuDTO>.Fail("Tên nhãn hiệu đã tồn tại"));

                var nhanHieu = new NhanHieu
                {
                    TenNhanHieu = createDTO.TenNhanHieu,
                    XuatXu = createDTO.XuatXu
                };

                var result = await _nhanHieuRepo.Create(nhanHieu);
                if (!result)
                    return BadRequest(ApiResponse<NhanHieuDTO>.Fail("Không thể tạo nhãn hiệu"));

                var nhanHieuDTO = new NhanHieuDTO
                {
                    Id = nhanHieu.Id,
                    TenNhanHieu = nhanHieu.TenNhanHieu,
                    XuatXu = nhanHieu.XuatXu
                };

                return CreatedAtAction(nameof(GetNhanHieuById), new { id = nhanHieu.Id },
                    ApiResponse<NhanHieuDTO>.Succeed(nhanHieuDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<NhanHieuDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateNhanHieu(int id, CreateNhanHieuDTO updateDTO)
        {
            try
            {
                var nhanHieu = await _nhanHieuRepo.GetById(id);
                if (nhanHieu == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy nhãn hiệu"));

                if (await _context.NhanHieus.AnyAsync(nh =>
                    nh.TenNhanHieu == updateDTO.TenNhanHieu && nh.Id != id))
                    return BadRequest(ApiResponse<bool>.Fail("Tên nhãn hiệu đã tồn tại"));

                nhanHieu.TenNhanHieu = updateDTO.TenNhanHieu;
                nhanHieu.XuatXu = updateDTO.XuatXu;

                var result = await _nhanHieuRepo.Update(nhanHieu);
                if (!result)
                    return BadRequest(ApiResponse<bool>.Fail("Không thể cập nhật nhãn hiệu"));

                return Ok(ApiResponse<bool>.Succeed(true, "Cập nhật thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteNhanHieu(int id)
        {
            try
            {
                var nhanHieu = await _nhanHieuRepo.GetById(id);
                if (nhanHieu == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy nhãn hiệu"));

                if (await _context.SanPhams.AnyAsync(sp => sp.NhanHieuId == id))
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa nhãn hiệu đang được sử dụng"));

                var result = await _nhanHieuRepo.Delete(id);
                if (!result)
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa nhãn hiệu"));

                return Ok(ApiResponse<bool>.Succeed(true, "Xóa thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
    }
}