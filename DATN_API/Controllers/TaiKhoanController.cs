using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DATN_API.DTOs.TaiKhoan;
using DATN_API.Models;
using System.Security.Cryptography;
using System.Text;
using DATN_API.DTOs.Common;
using DATN_API.DTOs;
using DATN_API.DTOs.PhanQuyen;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TaiKhoanController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaiKhoanController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TaiKhoanDTO>>>> GetAllTaiKhoan()
        {
            try
            {
                var taiKhoans = await _context.TaiKhoans
                    .Include(t => t.KhachHang)
                    .Include(t => t.PhanQuyen)
                    .Select(t => new TaiKhoanDTO
                    {
                        Id = t.Id,
                        TenDangNhap = t.TenDangNhap,
                        TenKhachHang = t.KhachHang.TenKhachHang,
                        Email = t.KhachHang.Email,
                        SoDienThoai = t.KhachHang.SoDienThoai,
                        TenPhanQuyen = t.PhanQuyen.TenPhanQuyen,
                        TrangThai = t.Status.ToString(),
                        CreatedAt = t.CreatedAt
                    })
                    .ToListAsync();

                return Ok(ApiResponse<IEnumerable<TaiKhoanDTO>>.Succeed(taiKhoans));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<TaiKhoanDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TaiKhoanDetailDTO>>> GetTaiKhoanById(int id)
        {
            try
            {
                var taiKhoan = await _context.TaiKhoans
                    .Include(t => t.KhachHang)
                    .Include(t => t.PhanQuyen)
                    .Include(t => t.DonHangs)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (taiKhoan == null)
                    return NotFound(ApiResponse<TaiKhoanDetailDTO>.Fail("Không tìm thấy tài khoản"));

                var result = new TaiKhoanDetailDTO
                {
                    Id = taiKhoan.Id,
                    TenDangNhap = taiKhoan.TenDangNhap,
                    TrangThai = taiKhoan.Status.ToString(),
                    PhanQuyen = new PhanQuyenDTO
                    {
                        Id = taiKhoan.PhanQuyen.Id,
                        TenPhanQuyen = taiKhoan.PhanQuyen.TenPhanQuyen
                    },
                    ThongTinKhachHang = new KhachHangBaseDTO
                    {
                        Id = taiKhoan.KhachHang.Id,
                        TenKhachHang = taiKhoan.KhachHang.TenKhachHang,
                        Email = taiKhoan.KhachHang.Email,
                        SoDienThoai = taiKhoan.KhachHang.SoDienThoai,
                        DiaChi = taiKhoan.KhachHang.DiaChi
                    },
                    LichSuDonHang = taiKhoan.DonHangs.Select(d => new DonHangBaseDTO
                    {
                        Id = d.Id,
                        TrangThai = d.TrangThai.ToString(),
                        CreatedAt = d.CreatedAt
                    }).ToList(),
                    CreatedAt = taiKhoan.CreatedAt,
                    UpdatedAt = taiKhoan.UpdatedAt
                };

                return Ok(ApiResponse<TaiKhoanDetailDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<TaiKhoanDetailDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TaiKhoanDTO>>> CreateTaiKhoan(CreateTaiKhoanDTO createDto)
        {
            try
            {
                if (await _context.TaiKhoans.AnyAsync(t => t.TenDangNhap == createDto.TenDangNhap))
                    return BadRequest(ApiResponse<TaiKhoanDTO>.Fail("Tên đăng nhập đã tồn tại"));

                if (await _context.KhachHangs.AnyAsync(k => k.Email == createDto.Email))
                    return BadRequest(ApiResponse<TaiKhoanDTO>.Fail("Email đã được sử dụng"));

                // Kiểm tra phân quyền tồn tại
                var phanQuyen = await _context.PhanQuyens.FindAsync(createDto.PhanQuyenId);
                if (phanQuyen == null)
                    return BadRequest(ApiResponse<TaiKhoanDTO>.Fail("Phân quyền không tồn tại"));

                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    var khachHang = new KhachHang
                    {
                        TenKhachHang = createDto.TenKhachHang,
                        Email = createDto.Email,
                        SoDienThoai = createDto.SoDienThoai,
                        DiaChi = createDto.DiaChi
                    };
                    _context.KhachHangs.Add(khachHang);
                    await _context.SaveChangesAsync();

                    var taiKhoan = new TaiKhoan
                    {
                        TenDangNhap = createDto.TenDangNhap,
                        MatKhauHash = HashPassword(createDto.MatKhau),
                        KhachHangId = khachHang.Id,
                        PhanQuyenId = createDto.PhanQuyenId,
                        Status = ENums.TrangThaiTaiKhoan.HoatDong
                    };
                    _context.TaiKhoans.Add(taiKhoan);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    var result = new TaiKhoanDTO
                    {
                        Id = taiKhoan.Id,
                        TenDangNhap = taiKhoan.TenDangNhap,
                        TenKhachHang = khachHang.TenKhachHang,
                        Email = khachHang.Email,
                        SoDienThoai = khachHang.SoDienThoai,
                        TenPhanQuyen = phanQuyen.TenPhanQuyen,
                        TrangThai = taiKhoan.Status.ToString(),
                        CreatedAt = taiKhoan.CreatedAt
                    };

                    return Ok(ApiResponse<TaiKhoanDTO>.Succeed(result, "Tạo tài khoản thành công"));
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<TaiKhoanDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("status/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateTrangThai(int id, UpdateTrangThaiTaiKhoanDTO updateDto)
        {
            try
            {
                var taiKhoan = await _context.TaiKhoans
                    .Include(t => t.PhanQuyen)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (taiKhoan == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy tài khoản"));

                // KHÔNG CHO PHÉP TÁC ĐỘNG TỚI TÀI KHOẢN ADMIN KHÁC
                if (taiKhoan.PhanQuyen.TenPhanQuyen == "Admin")
                    return BadRequest(ApiResponse<bool>.Fail("Không thể thay đổi trạng thái tài khoản Admin"));

                if (!Enum.TryParse<ENums.TrangThaiTaiKhoan>(updateDto.TrangThai, out var trangThai))
                    return BadRequest(ApiResponse<bool>.Fail("Trạng thái không hợp lệ"));

                taiKhoan.Status = trangThai;
                await _context.SaveChangesAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Cập nhật trạng thái thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("role/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdatePhanQuyen(int id, [FromBody] int phanQuyenId)
        {
            try
            {
                var taiKhoan = await _context.TaiKhoans
                    .Include(t => t.PhanQuyen)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (taiKhoan == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy tài khoản"));

                // KHÔNG CHO THAY ĐỔI QUYỀN CỦA ADMIN
                if (taiKhoan.PhanQuyen.TenPhanQuyen == "Admin")
                    return BadRequest(ApiResponse<bool>.Fail("Không thể thay đổi quyền của tài khoản Admin"));

                var phanQuyen = await _context.PhanQuyens.FindAsync(phanQuyenId);
                if (phanQuyen == null)
                    return BadRequest(ApiResponse<bool>.Fail("Phân quyền không tồn tại"));

                // KHÔNG ĐƯỢC PHÉP CẤP QUYỀN ADMIN MỚI
                if (phanQuyen.TenPhanQuyen == "Admin")
                    return BadRequest(ApiResponse<bool>.Fail("Không thể cấp quyền Admin"));

                taiKhoan.PhanQuyenId = phanQuyenId;
                await _context.SaveChangesAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Cập nhật phân quyền thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost("reset-password/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> ResetPassword(int id)
        {
            try
            {
                var taiKhoan = await _context.TaiKhoans
                    .Include(t => t.PhanQuyen)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (taiKhoan == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy tài khoản"));

                // KHÔNG CHO RESET PASS ADMIN
                if (taiKhoan.PhanQuyen.TenPhanQuyen == "Admin")
                    return BadRequest(ApiResponse<bool>.Fail("Không thể reset mật khẩu tài khoản Admin"));

                // ĐỂ TẠM PASS SAU KHI RESET 
                taiKhoan.MatKhauHash = HashPassword("123456");
                await _context.SaveChangesAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Reset mật khẩu thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}