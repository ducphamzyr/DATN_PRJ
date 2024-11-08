using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DATN_API.DTOs;
using DATN_API.DTOs.Common;
using DATN_API.Models;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaGiamGiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaGiamGiaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ApiResponse<IEnumerable<MaGiamGiaDTO>>>> GetAllMaGiamGia()
        {
            try
            {
                var maGiamGias = await _context.MaGiamGias
                    .Select(m => new MaGiamGiaDTO
                    {
                        Id = m.Id,
                        TenMa = m.TenMa,
                        MaApDung = m.MaApDung,
                        LoaiMa = m.LoaiMa.ToString(),
                        DanhSachTaiKhoan = m.DanhSachTaiKhoan,
                        SoTienToiThieu = m.SoTienToiThieu,
                        SoTienToiDa = m.SoTienToiDa,
                        PhanTramGiam = m.PhanTramGiam,
                        NgayHetHan = m.NgayHetHan,
                        IsActive = m.IsActive,
                        CreatedAt = m.CreatedAt,
                        UpdatedAt = m.UpdatedAt
                    })
                    .ToListAsync();

                return Ok(ApiResponse<IEnumerable<MaGiamGiaDTO>>.Succeed(maGiamGias));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<MaGiamGiaDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<MaGiamGiaDTO>>> GetMaGiamGiaById(int id)
        {
            try
            {
                var maGiamGia = await _context.MaGiamGias.FindAsync(id);
                if (maGiamGia == null)
                    return NotFound(ApiResponse<MaGiamGiaDTO>.Fail("Không tìm thấy mã giảm giá"));

                var result = new MaGiamGiaDTO
                {
                    Id = maGiamGia.Id,
                    TenMa = maGiamGia.TenMa,
                    MaApDung = maGiamGia.MaApDung,
                    LoaiMa = maGiamGia.LoaiMa.ToString(),
                    DanhSachTaiKhoan = maGiamGia.DanhSachTaiKhoan,
                    SoTienToiThieu = maGiamGia.SoTienToiThieu,
                    SoTienToiDa = maGiamGia.SoTienToiDa,
                    PhanTramGiam = maGiamGia.PhanTramGiam,
                    NgayHetHan = maGiamGia.NgayHetHan,
                    IsActive = maGiamGia.IsActive,
                    CreatedAt = maGiamGia.CreatedAt,
                    UpdatedAt = maGiamGia.UpdatedAt
                };

                return Ok(ApiResponse<MaGiamGiaDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<MaGiamGiaDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<MaGiamGiaDTO>>> CreateMaGiamGia(CreateMaGiamGiaDTO createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (await _context.MaGiamGias.AnyAsync(m => m.MaApDung == createDto.MaApDung))
                    return BadRequest(ApiResponse<MaGiamGiaDTO>.Fail("Mã áp dụng đã tồn tại"));

                var maGiamGia = new MaGiamGia
                {
                    TenMa = createDto.TenMa,
                    MaApDung = createDto.MaApDung,
                    // Chuyển đổi rõ ràng từ int sang enum LoaiGiamGia
                    LoaiMa = (ENums.LoaiGiamGia)createDto.LoaiMa,
                    DanhSachTaiKhoan = createDto.DanhSachTaiKhoan,
                    // Chuyển đổi rõ ràng từ decimal sang int
                    SoTienToiThieu = (int)createDto.SoTienToiThieu,
                    SoTienToiDa = (int)createDto.SoTienToiDa,
                    PhanTramGiam = createDto.PhanTramGiam,
                    NgayHetHan = createDto.NgayHetHan,
                    IsActive = true
                };

                _context.MaGiamGias.Add(maGiamGia);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var result = new MaGiamGiaDTO
                {
                    Id = maGiamGia.Id,
                    TenMa = maGiamGia.TenMa,
                    MaApDung = maGiamGia.MaApDung,
                    LoaiMa = maGiamGia.LoaiMa.ToString(),
                    DanhSachTaiKhoan = maGiamGia.DanhSachTaiKhoan,
                    SoTienToiThieu = maGiamGia.SoTienToiThieu,
                    SoTienToiDa = maGiamGia.SoTienToiDa,
                    PhanTramGiam = maGiamGia.PhanTramGiam,
                    NgayHetHan = maGiamGia.NgayHetHan,
                    IsActive = maGiamGia.IsActive,
                    CreatedAt = maGiamGia.CreatedAt
                };

                return CreatedAtAction(nameof(GetMaGiamGiaById), new { id = maGiamGia.Id },
                    ApiResponse<MaGiamGiaDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<MaGiamGiaDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<MaGiamGiaDTO>>> UpdateMaGiamGia(int id, UpdateMaGiamGiaDTO updateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var maGiamGia = await _context.MaGiamGias.FindAsync(id);
                if (maGiamGia == null)
                    return NotFound(ApiResponse<MaGiamGiaDTO>.Fail("Không tìm thấy mã giảm giá"));

                if (await _context.MaGiamGias.AnyAsync(m => m.MaApDung == updateDto.MaApDung && m.Id != id))
                    return BadRequest(ApiResponse<MaGiamGiaDTO>.Fail("Mã áp dụng đã tồn tại"));

                maGiamGia.TenMa = updateDto.TenMa;
                maGiamGia.MaApDung = updateDto.MaApDung;
                // Chuyển đổi rõ ràng từ int sang enum LoaiGiamGia
                maGiamGia.LoaiMa = (ENums.LoaiGiamGia)updateDto.LoaiMa;
                maGiamGia.DanhSachTaiKhoan = updateDto.DanhSachTaiKhoan;
                // Chuyển đổi rõ ràng từ decimal sang int
                maGiamGia.SoTienToiThieu = (int)updateDto.SoTienToiThieu;
                maGiamGia.SoTienToiDa = (int)updateDto.SoTienToiDa;
                maGiamGia.PhanTramGiam = updateDto.PhanTramGiam;
                maGiamGia.NgayHetHan = updateDto.NgayHetHan;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var result = new MaGiamGiaDTO
                {
                    Id = maGiamGia.Id,
                    TenMa = maGiamGia.TenMa,
                    MaApDung = maGiamGia.MaApDung,
                    LoaiMa = maGiamGia.LoaiMa.ToString(),
                    DanhSachTaiKhoan = maGiamGia.DanhSachTaiKhoan,
                    SoTienToiThieu = maGiamGia.SoTienToiThieu,
                    SoTienToiDa = maGiamGia.SoTienToiDa,
                    PhanTramGiam = maGiamGia.PhanTramGiam,
                    NgayHetHan = maGiamGia.NgayHetHan,
                    IsActive = maGiamGia.IsActive,
                    CreatedAt = maGiamGia.CreatedAt,
                    UpdatedAt = maGiamGia.UpdatedAt
                };

                return Ok(ApiResponse<MaGiamGiaDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<MaGiamGiaDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("status/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateTrangThai(int id)
        {
            try
            {
                var maGiamGia = await _context.MaGiamGias.FindAsync(id);
                if (maGiamGia == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy mã giảm giá"));

                maGiamGia.IsActive = !maGiamGia.IsActive;
                await _context.SaveChangesAsync();

                return Ok(ApiResponse<bool>.Succeed(true, $"Đã {(maGiamGia.IsActive ? "kích hoạt" : "vô hiệu hóa")} mã giảm giá"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteMaGiamGia(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var maGiamGia = await _context.MaGiamGias
                    .Include(m => m.DonHangChiTiets)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (maGiamGia == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy mã giảm giá"));

                if (maGiamGia.DonHangChiTiets.Any())
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa mã giảm giá đã được sử dụng"));

                _context.MaGiamGias.Remove(maGiamGia);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Xóa mã giảm giá thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("check/{code}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<MaGiamGiaDTO>>> CheckMaGiamGia(string code)
        {
            try
            {
                var maGiamGia = await _context.MaGiamGias
                    .FirstOrDefaultAsync(m => m.MaApDung == code && m.IsActive && m.NgayHetHan > DateTime.Now);

                if (maGiamGia == null)
                    return NotFound(ApiResponse<MaGiamGiaDTO>.Fail("Mã giảm giá không tồn tại hoặc đã hết hạn"));

                var result = new MaGiamGiaDTO
                {
                    Id = maGiamGia.Id,
                    TenMa = maGiamGia.TenMa,
                    MaApDung = maGiamGia.MaApDung,
                    LoaiMa = maGiamGia.LoaiMa.ToString(),
                    DanhSachTaiKhoan = maGiamGia.DanhSachTaiKhoan,
                    SoTienToiThieu = maGiamGia.SoTienToiThieu,
                    SoTienToiDa = maGiamGia.SoTienToiDa,
                    PhanTramGiam = maGiamGia.PhanTramGiam,
                    NgayHetHan = maGiamGia.NgayHetHan,
                    IsActive = maGiamGia.IsActive,
                    CreatedAt = maGiamGia.CreatedAt,
                    UpdatedAt = maGiamGia.UpdatedAt
                };

                return Ok(ApiResponse<MaGiamGiaDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<MaGiamGiaDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
    }
}