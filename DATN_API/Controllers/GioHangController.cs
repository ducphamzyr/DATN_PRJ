// Controllers/GioHangController.cs
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
    public class GioHangController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GioHangController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<GioHangDTO>>> GetMyCart()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var gioHang = await _context.GioHangs
                    .Include(g => g.GioHangChiTiets)
                        .ThenInclude(ct => ct.SanPham)
                    .Where(g => g.TaiKhoanId == userId)
                    .Select(g => new GioHangDTO
                    {
                        Id = g.Id,
                        TaiKhoanId = g.TaiKhoanId,
                        TongSanPham = g.GioHangChiTiets.Sum(ct => ct.SoLuong),
                        TongTien = g.GioHangChiTiets.Sum(ct => ct.SanPham.Gia * ct.SoLuong),
                        ChiTietGioHang = g.GioHangChiTiets.Select(ct => new GioHangChiTietDTO
                        {
                            Id = ct.Id,
                            SanPhamId = ct.SanPhamId,
                            TenSanPham = ct.SanPham.TenSanPham,
                            GiaBan = ct.SanPham.Gia,
                            SoLuong = ct.SoLuong,
                            ThanhTien = ct.SanPham.Gia * ct.SoLuong,
                            GhiChu = ct.GhiChu,
                            Mau = ct.SanPham.Mau,
                            BoNho = ct.SanPham.BoNho,
                            Ram = ct.SanPham.Ram,
                            SoLuongTonKho = ct.SanPham.ConLai
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (gioHang == null)
                {
                    return Ok(ApiResponse<GioHangDTO>.Succeed(new GioHangDTO
                    {
                        TaiKhoanId = userId,
                        TongSanPham = 0,
                        TongTien = 0,
                        ChiTietGioHang = new List<GioHangChiTietDTO>()
                    }));
                }

                return Ok(ApiResponse<GioHangDTO>.Succeed(gioHang));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<GioHangDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<bool>>> AddToCart(ThemVaoGioDTO themVaoGioDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                // Kiểm tra sản phẩm
                var sanPham = await _context.SanPhams.FindAsync(themVaoGioDto.SanPhamId);
                if (sanPham == null)
                    return BadRequest(ApiResponse<bool>.Fail("Sản phẩm không tồn tại"));

                if (sanPham.ConLai < themVaoGioDto.SoLuong)
                    return BadRequest(ApiResponse<bool>.Fail("Số lượng sản phẩm không đủ"));

                // Lấy hoặc tạo giỏ hàng
                var gioHang = await _context.GioHangs
                    .Include(g => g.GioHangChiTiets)
                    .FirstOrDefaultAsync(g => g.TaiKhoanId == userId);

                if (gioHang == null)
                {
                    gioHang = new GioHang
                    {
                        TaiKhoanId = userId
                    };
                    _context.GioHangs.Add(gioHang);
                    await _context.SaveChangesAsync();
                }

                // Kiểm tra sản phẩm đã có trong giỏ
                var chiTiet = gioHang.GioHangChiTiets
                    .FirstOrDefault(ct => ct.SanPhamId == themVaoGioDto.SanPhamId);

                if (chiTiet != null)
                {
                    // Cập nhật số lượng
                    if (sanPham.ConLai < chiTiet.SoLuong + themVaoGioDto.SoLuong)
                        return BadRequest(ApiResponse<bool>.Fail("Số lượng sản phẩm không đủ"));

                    chiTiet.SoLuong += themVaoGioDto.SoLuong;
                    chiTiet.GhiChu = themVaoGioDto.GhiChu;
                }
                else
                {
                    // Thêm mới
                    chiTiet = new GioHangChiTiet
                    {
                        GioHangId = gioHang.Id,
                        SanPhamId = themVaoGioDto.SanPhamId,
                        SoLuong = themVaoGioDto.SoLuong,
                        GhiChu = themVaoGioDto.GhiChu
                    };
                    _context.GioHangChiTiets.Add(chiTiet);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Thêm vào giỏ hàng thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateCartItem(CapNhatGioHangDTO capNhatDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var chiTiet = await _context.GioHangChiTiets
                    .Include(ct => ct.GioHang)
                    .Include(ct => ct.SanPham)
                    .FirstOrDefaultAsync(ct => ct.Id == capNhatDto.GioHangChiTietId
                        && ct.GioHang.TaiKhoanId == userId);

                if (chiTiet == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy sản phẩm trong giỏ hàng"));

                if (chiTiet.SanPham.ConLai < capNhatDto.SoLuong)
                    return BadRequest(ApiResponse<bool>.Fail("Số lượng sản phẩm không đủ"));

                chiTiet.SoLuong = capNhatDto.SoLuong;
                chiTiet.GhiChu = capNhatDto.GhiChu;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Cập nhật giỏ hàng thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("remove/{chiTietId}")]
        public async Task<ActionResult<ApiResponse<bool>>> RemoveFromCart(int chiTietId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var chiTiet = await _context.GioHangChiTiets
                    .Include(ct => ct.GioHang)
                    .FirstOrDefaultAsync(ct => ct.Id == chiTietId
                        && ct.GioHang.TaiKhoanId == userId);

                if (chiTiet == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy sản phẩm trong giỏ hàng"));

                _context.GioHangChiTiets.Remove(chiTiet);

                // Kiểm tra và xóa giỏ hàng nếu trống
                var gioHang = await _context.GioHangs
                    .Include(g => g.GioHangChiTiets)
                    .FirstOrDefaultAsync(g => g.Id == chiTiet.GioHangId);

                if (gioHang.GioHangChiTiets.Count == 1) // Chỉ còn item đang xóa
                    _context.GioHangs.Remove(gioHang);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Xóa sản phẩm khỏi giỏ hàng thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("clear")]
        public async Task<ActionResult<ApiResponse<bool>>> ClearCart()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var gioHang = await _context.GioHangs
                    .Include(g => g.GioHangChiTiets)
                    .FirstOrDefaultAsync(g => g.TaiKhoanId == userId);

                if (gioHang != null)
                {
                    _context.GioHangChiTiets.RemoveRange(gioHang.GioHangChiTiets);
                    _context.GioHangs.Remove(gioHang);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return Ok(ApiResponse<bool>.Succeed(true, "Xóa giỏ hàng thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
    }
}