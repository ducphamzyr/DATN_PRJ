using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DATN_API.DTOs.PhanQuyen;
using DATN_API.Models;
using DATN_API.DTOs.Common;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PhanQuyenController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhanQuyenController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<PhanQuyenDTO>>>> GetAllPhanQuyen()
        {
            try
            {
                var phanQuyens = await _context.PhanQuyens
                    .Select(pq => new PhanQuyenDTO
                    {
                        Id = pq.Id,
                        TenPhanQuyen = pq.TenPhanQuyen,
                        SoLuongTaiKhoan = pq.TaiKhoans.Count,
                        CreatedAt = pq.CreatedAt,
                        UpdatedAt = pq.UpdatedAt
                    })
                    .ToListAsync();

                return Ok(ApiResponse<IEnumerable<PhanQuyenDTO>>.Succeed(phanQuyens));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<PhanQuyenDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PhanQuyenDetailDTO>>> GetPhanQuyenById(int id)
        {
            try
            {
                var phanQuyen = await _context.PhanQuyens
                    .Include(pq => pq.TaiKhoans)
                        .ThenInclude(t => t.KhachHang)
                    .FirstOrDefaultAsync(pq => pq.Id == id);

                if (phanQuyen == null)
                    return NotFound(ApiResponse<PhanQuyenDetailDTO>.Fail("Không tìm thấy phân quyền"));

                var result = new PhanQuyenDetailDTO
                {
                    Id = phanQuyen.Id,
                    TenPhanQuyen = phanQuyen.TenPhanQuyen,
                    CreatedAt = phanQuyen.CreatedAt,
                    UpdatedAt = phanQuyen.UpdatedAt,
                    DanhSachTaiKhoan = phanQuyen.TaiKhoans.Select(t => new TaiKhoanTrongPhanQuyenDTO
                    {
                        Id = t.Id,
                        TenDangNhap = t.TenDangNhap,
                        TenKhachHang = t.KhachHang.TenKhachHang,
                        Email = t.KhachHang.Email,
                        TrangThai = t.Status.ToString(),
                        CreatedAt = t.CreatedAt
                    }).ToList()
                };

                return Ok(ApiResponse<PhanQuyenDetailDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PhanQuyenDetailDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PhanQuyenDTO>>> CreatePhanQuyen(CreatePhanQuyenDTO createDto)
        {
            try
            {
                if (await _context.PhanQuyens.AnyAsync(pq => pq.TenPhanQuyen == createDto.TenPhanQuyen))
                    return BadRequest(ApiResponse<PhanQuyenDTO>.Fail("Tên phân quyền đã tồn tại"));

                var phanQuyen = new PhanQuyen
                {
                    TenPhanQuyen = createDto.TenPhanQuyen
                };

                _context.PhanQuyens.Add(phanQuyen);
                await _context.SaveChangesAsync();

                var result = new PhanQuyenDTO
                {
                    Id = phanQuyen.Id,
                    TenPhanQuyen = phanQuyen.TenPhanQuyen,
                    SoLuongTaiKhoan = 0,
                    CreatedAt = phanQuyen.CreatedAt,
                    UpdatedAt = phanQuyen.UpdatedAt
                };

                return CreatedAtAction(nameof(GetPhanQuyenById), new { id = phanQuyen.Id },
                    ApiResponse<PhanQuyenDTO>.Succeed(result, "Tạo phân quyền thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PhanQuyenDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdatePhanQuyen(int id, UpdatePhanQuyenDTO updateDto)
        {
            try
            {
                var phanQuyen = await _context.PhanQuyens.FindAsync(id);
                if (phanQuyen == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy phân quyền"));

                // KHÔNG SỬA ĐC QUYỀN ADMIN
                if (phanQuyen.TenPhanQuyen == "Admin")
                    return BadRequest(ApiResponse<bool>.Fail("Không thể sửa phân quyền Admin"));

                if (await _context.PhanQuyens.AnyAsync(pq =>
                    pq.TenPhanQuyen == updateDto.TenPhanQuyen && pq.Id != id))
                    return BadRequest(ApiResponse<bool>.Fail("Tên phân quyền đã tồn tại"));

                phanQuyen.TenPhanQuyen = updateDto.TenPhanQuyen;
                await _context.SaveChangesAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Cập nhật phân quyền thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePhanQuyen(int id)
        {
            try
            {
                var phanQuyen = await _context.PhanQuyens.FindAsync(id);
                if (phanQuyen == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy phân quyền"));

                // Không cho phép xóa phân quyền Admin
                if (phanQuyen.TenPhanQuyen == "Admin")
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa phân quyền Admin"));

                // Kiểm tra xem phân quyền có đang được sử dụng không
                if (await _context.TaiKhoans.AnyAsync(tk => tk.PhanQuyenId == id))
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa phân quyền đang được sử dụng bởi tài khoản"));

                _context.PhanQuyens.Remove(phanQuyen);
                await _context.SaveChangesAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Xóa phân quyền thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<ApiResponse<Dictionary<string, int>>>> GetStatistics()
        {
            try
            {
                var statistics = await _context.PhanQuyens
                    .Include(pq => pq.TaiKhoans)
                    .ToDictionaryAsync(
                        pq => pq.TenPhanQuyen,
                        pq => pq.TaiKhoans.Count
                    );

                return Ok(ApiResponse<Dictionary<string, int>>.Succeed(statistics));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<Dictionary<string, int>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
    }
}