using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DATN_API.DTOs.Auth;
using DATN_API.Models;
using DATN_API.Services;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DATN_API.DTOs.Common;
// FILE NÀY SẼ SỬ DỤNG CÁC CHỨC NĂNG CỦA USER / API ADMIN SẼ Ở BÊN TAIKHOANCONTROLLER.CS
namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }
        [HttpPost("login")] // LOGIN TRẢ VỀ THÔNG TIN + JWT TOKEN BEARER
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> Login(LoginDTO loginDto) 
        {
            try
            {
                var taiKhoan = await _context.TaiKhoans
                    .Include(t => t.KhachHang)
                    .Include(t => t.PhanQuyen)
                    .FirstOrDefaultAsync(t => t.TenDangNhap == loginDto.TenDangNhap);

                if (taiKhoan == null)
                    return Ok(ApiResponse<LoginResponseDTO>.Fail("Tên đăng nhập không tồn tại"));

                var hashedPassword = HashPassword(loginDto.MatKhau);
                if (taiKhoan.MatKhauHash != hashedPassword)
                    return Ok(ApiResponse<LoginResponseDTO>.Fail("Mật khẩu không chính xác"));

                if (taiKhoan.Status != ENums.TrangThaiTaiKhoan.HoatDong)
                    return Ok(ApiResponse<LoginResponseDTO>.Fail("Tài khoản đã bị khóa hoặc ngừng hoạt động"));

                var token = _jwtService.GenerateToken(taiKhoan.Id.ToString(), taiKhoan.PhanQuyen.TenPhanQuyen);

                var response = new LoginResponseDTO
                {
                    Token = token,
                    TenDangNhap = taiKhoan.TenDangNhap,
                    TenKhachHang = taiKhoan.KhachHang.TenKhachHang,
                    Email = taiKhoan.KhachHang.Email,
                    TenPhanQuyen = taiKhoan.PhanQuyen.TenPhanQuyen,
                    TokenExpires = DateTime.UtcNow.AddHours(1)
                };

                return Ok(ApiResponse<LoginResponseDTO>.Succeed(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<LoginResponseDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost("register")] // ĐĂNG KÝ MỚI TÀI KHOẢN 
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> Register(RegisterDTO registerDto)
        {
            try
            {
                if (await _context.TaiKhoans.AnyAsync(t => t.TenDangNhap == registerDto.TenDangNhap))
                    return BadRequest(ApiResponse<LoginResponseDTO>.Fail("Tên đăng nhập đã tồn tại"));

                if (await _context.KhachHangs.AnyAsync(k => k.Email == registerDto.Email))
                    return BadRequest(ApiResponse<LoginResponseDTO>.Fail("Email đã được sử dụng"));

                var userRole = await _context.PhanQuyens
                    .FirstOrDefaultAsync(p => p.TenPhanQuyen == "User");

                if (userRole == null)
                    return StatusCode(500, ApiResponse<LoginResponseDTO>.Fail("Lỗi hệ thống: Không tìm thấy phân quyền"));

                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    var khachHang = new KhachHang
                    {
                        TenKhachHang = registerDto.TenKhachHang,
                        Email = registerDto.Email,
                        SoDienThoai = registerDto.SoDienThoai,
                        DiaChi = registerDto.DiaChi
                    };
                    _context.KhachHangs.Add(khachHang);
                    await _context.SaveChangesAsync();

                    var taiKhoan = new TaiKhoan
                    {
                        TenDangNhap = registerDto.TenDangNhap,
                        MatKhauHash = HashPassword(registerDto.MatKhau),
                        KhachHangId = khachHang.Id,
                        PhanQuyenId = userRole.Id,
                        Status = ENums.TrangThaiTaiKhoan.HoatDong // MẶC ĐỊNH CẤP TRẠNG THÁI "HOẠT ĐỘNG" CHO USERS MỚI !!
                    };
                    _context.TaiKhoans.Add(taiKhoan);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    var token = _jwtService.GenerateToken(taiKhoan.Id.ToString(), "User"); // TOKEN
                    var response = new LoginResponseDTO
                    {
                        Token = token,
                        TenDangNhap = taiKhoan.TenDangNhap,
                        TenKhachHang = khachHang.TenKhachHang,
                        Email = khachHang.Email,
                        TenPhanQuyen = "User",
                        TokenExpires = DateTime.UtcNow.AddHours(72) // THỜI GIAN TOKEN HẾT HẠN , MẶC ĐỊNH LÀ 3 NGÀY (72H)
                    };

                    return Ok(ApiResponse<LoginResponseDTO>.Succeed(response, "Đăng ký thành công"));
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<LoginResponseDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost("change-password")] // ĐỔI MẬT KHẨU
        [Authorize]
        public async Task<ActionResult<ApiResponse<bool>>> ChangePassword(ChangePasswordDTO passwordDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var taiKhoan = await _context.TaiKhoans.FindAsync(int.Parse(userId)); // TÌM TÀI KHOẢN

                if (taiKhoan == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy tài khoản"));

                var oldPasswordHash = HashPassword(passwordDto.MatKhauCu);
                if (taiKhoan.MatKhauHash != oldPasswordHash)
                    return BadRequest(ApiResponse<bool>.Fail("Mật khẩu cũ không chính xác"));

                taiKhoan.MatKhauHash = HashPassword(passwordDto.MatKhauMoi);
                await _context.SaveChangesAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Đổi mật khẩu thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("profile")] // TRẢ VỀ THÔNG TIN CỦA TÀI KHOẢN
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserProfileDTO>>> GetProfile()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var taiKhoan = await _context.TaiKhoans
                    .Include(t => t.KhachHang)
                    .Include(t => t.PhanQuyen)
                    .FirstOrDefaultAsync(t => t.Id == int.Parse(userId));

                if (taiKhoan == null)
                    return NotFound(ApiResponse<UserProfileDTO>.Fail("Không tìm thấy tài khoản"));

                var profile = new UserProfileDTO
                {
                    TenDangNhap = taiKhoan.TenDangNhap,
                    TenKhachHang = taiKhoan.KhachHang.TenKhachHang,
                    Email = taiKhoan.KhachHang.Email,
                    SoDienThoai = taiKhoan.KhachHang.SoDienThoai,
                    DiaChi = taiKhoan.KhachHang.DiaChi,
                    TenPhanQuyen = taiKhoan.PhanQuyen.TenPhanQuyen,
                    CreatedAt = taiKhoan.CreatedAt
                };

                return Ok(ApiResponse<UserProfileDTO>.Succeed(profile));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserProfileDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("profile")] // SỬA ĐỔI THÔNG TIN CỦA TÀI KHOẢN
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserProfileDTO>>> UpdateProfile(UpdateProfileDTO updateDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var taiKhoan = await _context.TaiKhoans
                    .Include(t => t.KhachHang)
                    .FirstOrDefaultAsync(t => t.Id == int.Parse(userId));

                if (taiKhoan == null)
                    return NotFound(ApiResponse<UserProfileDTO>.Fail("Không tìm thấy tài khoản"));

                taiKhoan.KhachHang.TenKhachHang = updateDto.TenKhachHang;
                taiKhoan.KhachHang.SoDienThoai = updateDto.SoDienThoai;
                taiKhoan.KhachHang.DiaChi = updateDto.DiaChi;

                await _context.SaveChangesAsync();

                return await GetProfile();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserProfileDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        private string HashPassword(string password) // HASH PASSWORD 
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}