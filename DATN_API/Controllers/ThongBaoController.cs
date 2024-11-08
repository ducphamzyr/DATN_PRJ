// Controllers/ThongBaoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DATN_API.DTOs;
using DATN_API.DTOs.Common;
using DATN_API.Models;
using System.Security.Claims;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ThongBaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ThongBaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ThongBaoDTO>>>> GetMyNotifications()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var thongBaos = await _context.ThongBaos
                    .Include(t => t.TaiKhoan)
                    .Where(t => t.TaiKhoanId == userId && t.HetHan > DateTime.Now)
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => new ThongBaoDTO
                    {
                        Id = t.Id,
                        TieuDe = t.TieuDe,
                        NoiDung = t.NoiDung,
                        GhiChu = t.GhiChu,
                        HetHan = t.HetHan,
                        CreatedAt = t.CreatedAt,
                        UpdatedAt = t.UpdatedAt
                    })
                    .ToListAsync();

                return Ok(ApiResponse<IEnumerable<ThongBaoDTO>>.Succeed(thongBaos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<ThongBaoDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ThongBaoDTO>>>> GetAllNotifications()
        {
            try
            {
                var thongBaos = await _context.ThongBaos
                    .Include(t => t.TaiKhoan)
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => new ThongBaoDTO
                    {
                        Id = t.Id,
                        TaiKhoanId = t.TaiKhoanId,
                        TenTaiKhoan = t.TaiKhoan.KhachHang.TenKhachHang,
                        TieuDe = t.TieuDe,
                        NoiDung = t.NoiDung,
                        GhiChu = t.GhiChu,
                        HetHan = t.HetHan,
                        CreatedAt = t.CreatedAt,
                        UpdatedAt = t.UpdatedAt
                    })
                    .ToListAsync();

                return Ok(ApiResponse<IEnumerable<ThongBaoDTO>>.Succeed(thongBaos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<ThongBaoDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ThongBaoDetailDTO>>> GetThongBaoById(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                var thongBao = await _context.ThongBaos
                    .Include(t => t.TaiKhoan)
                        .ThenInclude(tk => tk.KhachHang)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (thongBao == null)
                    return NotFound(ApiResponse<ThongBaoDetailDTO>.Fail("Không tìm thấy thông báo"));

                // Kiểm tra quyền truy cập
                if (userRole != "Admin" && thongBao.TaiKhoanId != userId)
                    return Forbid();

                var result = new ThongBaoDetailDTO
                {
                    Id = thongBao.Id,
                    TieuDe = thongBao.TieuDe,
                    NoiDung = thongBao.NoiDung,
                    GhiChu = thongBao.GhiChu,
                    HetHan = thongBao.HetHan,
                    TaiKhoan = new ThongTinTaiKhoanDTO
                    {
                        Id = thongBao.TaiKhoan.Id,
                        TenDangNhap = thongBao.TaiKhoan.TenDangNhap,
                        TenKhachHang = thongBao.TaiKhoan.KhachHang.TenKhachHang,
                        Email = thongBao.TaiKhoan.KhachHang.Email
                    },
                    CreatedAt = thongBao.CreatedAt,
                    UpdatedAt = thongBao.UpdatedAt
                };

                return Ok(ApiResponse<ThongBaoDetailDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ThongBaoDetailDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<ThongBaoDTO>>> CreateThongBao(CreateThongBaoDTO createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra tài khoản tồn tại
                var taiKhoan = await _context.TaiKhoans.FindAsync(createDto.TaiKhoanId);
                if (taiKhoan == null)
                    return BadRequest(ApiResponse<ThongBaoDTO>.Fail("Tài khoản không tồn tại"));

                var thongBao = new ThongBao
                {
                    TaiKhoanId = createDto.TaiKhoanId,
                    TieuDe = createDto.TieuDe,
                    NoiDung = createDto.NoiDung,
                    GhiChu = createDto.GhiChu,
                    HetHan = createDto.HetHan
                };

                _context.ThongBaos.Add(thongBao);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var result = new ThongBaoDTO
                {
                    Id = thongBao.Id,
                    TaiKhoanId = thongBao.TaiKhoanId,
                    TieuDe = thongBao.TieuDe,
                    NoiDung = thongBao.NoiDung,
                    GhiChu = thongBao.GhiChu,
                    HetHan = thongBao.HetHan,
                    CreatedAt = thongBao.CreatedAt
                };

                return CreatedAtAction(nameof(GetThongBaoById), new { id = thongBao.Id },
                    ApiResponse<ThongBaoDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<ThongBaoDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost("broadcast")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> BroadcastThongBao(BroadcastThongBaoDTO broadcastDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var taiKhoans = await _context.TaiKhoans
                    .Where(t => t.Status == ENums.TrangThaiTaiKhoan.HoatDong)
                    .ToListAsync();

                var thongBaos = taiKhoans.Select(t => new ThongBao
                {
                    TaiKhoanId = t.Id,
                    TieuDe = broadcastDto.TieuDe,
                    NoiDung = broadcastDto.NoiDung,
                    GhiChu = broadcastDto.GhiChu,
                    HetHan = broadcastDto.HetHan
                }).ToList();

                _context.ThongBaos.AddRange(thongBaos);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, $"Đã gửi thông báo đến {thongBaos.Count} người dùng"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteThongBao(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var thongBao = await _context.ThongBaos.FindAsync(id);
                if (thongBao == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy thông báo"));

                _context.ThongBaos.Remove(thongBao);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Xóa thông báo thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("clear")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> ClearExpiredNotifications()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var expiredThongBaos = await _context.ThongBaos
                    .Where(t => t.HetHan < DateTime.Now)
                    .ToListAsync();

                _context.ThongBaos.RemoveRange(expiredThongBaos);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, $"Đã xóa {expiredThongBaos.Count} thông báo hết hạn"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
    }
}