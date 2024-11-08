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
    public class KhachHangController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KhachHangController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<IEnumerable<KhachHangDTO>>>> GetAllKhachHang()
        {
            try
            {
                var khachHangs = await _context.KhachHangs
                    .Include(k => k.TaiKhoan)
                        .ThenInclude(t => t.DonHangs)
                            .ThenInclude(d => d.DonHangChiTiets)
                    .Select(k => new KhachHangDTO
                    {
                        Id = k.Id,
                        TenKhachHang = k.TenKhachHang,
                        DiaChi = k.DiaChi,
                        Email = k.Email,
                        SoDienThoai = k.SoDienThoai,
                        TrangThaiTaiKhoan = k.TaiKhoan.Status.ToString(),
                        TongDonHang = k.TaiKhoan.DonHangs.Count,
                        TongTienMua = k.TaiKhoan.DonHangs
                            .Where(d => d.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh)
                            .SelectMany(d => d.DonHangChiTiets)
                            .Sum(ct => ct.GiaTri * ct.SoLuong),
                        CreatedAt = k.CreatedAt,
                        UpdatedAt = k.UpdatedAt
                    })
                    .ToListAsync();

                return Ok(ApiResponse<IEnumerable<KhachHangDTO>>.Succeed(khachHangs));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<KhachHangDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<KhachHangDetailDTO>>> GetKhachHangById(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                var khachHang = await _context.KhachHangs
                    .Include(k => k.TaiKhoan)
                        .ThenInclude(t => t.PhanQuyen)
                    .Include(k => k.TaiKhoan.DonHangs)
                        .ThenInclude(d => d.DonHangChiTiets)
                    .Include(k => k.TaiKhoan.GioHang)
                        .ThenInclude(g => g.GioHangChiTiets)
                            .ThenInclude(ct => ct.SanPham)
                    .FirstOrDefaultAsync(k => k.Id == id);

                if (khachHang == null)
                    return NotFound(ApiResponse<KhachHangDetailDTO>.Fail("Không tìm thấy khách hàng"));

                // Kiểm tra quyền truy cập
                if (userRole != "Admin" && khachHang.TaiKhoan.Id.ToString() != userId)
                    return Forbid();

                var donHangs = khachHang.TaiKhoan.DonHangs.OrderByDescending(d => d.CreatedAt).Take(10);

                var result = new KhachHangDetailDTO
                {
                    Id = khachHang.Id,
                    TenKhachHang = khachHang.TenKhachHang,
                    DiaChi = khachHang.DiaChi,
                    Email = khachHang.Email,
                    SoDienThoai = khachHang.SoDienThoai,
                    CreatedAt = khachHang.CreatedAt,
                    UpdatedAt = khachHang.UpdatedAt,

                    TaiKhoan = new TaiKhoanKhachHangDTO
                    {
                        Id = khachHang.TaiKhoan.Id,
                        TenDangNhap = khachHang.TaiKhoan.TenDangNhap,
                        TenPhanQuyen = khachHang.TaiKhoan.PhanQuyen.TenPhanQuyen,
                        TrangThai = khachHang.TaiKhoan.Status.ToString(),
                        NgayTao = khachHang.TaiKhoan.CreatedAt
                    },

                    TongDonHang = khachHang.TaiKhoan.DonHangs.Count,
                    DonHangThanhCong = khachHang.TaiKhoan.DonHangs.Count(d => d.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh),
                    DonHangHuy = khachHang.TaiKhoan.DonHangs.Count(d => d.TrangThai == ENums.TrangThaiDonHang.DaHuy),
                    TongTienMua = khachHang.TaiKhoan.DonHangs
                        .Where(d => d.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh)
                        .SelectMany(d => d.DonHangChiTiets)
                        .Sum(ct => ct.GiaTri * ct.SoLuong),

                    LichSuMuaHang = donHangs.Select(d => new DonHangKhachHangDTO
                    {
                        Id = d.Id,
                        TrangThai = d.TrangThai.ToString(),
                        TongTien = d.DonHangChiTiets.Sum(ct => ct.GiaTri * ct.SoLuong),
                        NgayDat = d.CreatedAt
                    }).ToList(),

                    GioHang = khachHang.TaiKhoan.GioHang != null ? new GioHangKhachHangDTO
                    {
                        SoLuongSanPham = khachHang.TaiKhoan.GioHang.GioHangChiTiets.Sum(ct => ct.SoLuong),
                        TongTien = khachHang.TaiKhoan.GioHang.GioHangChiTiets.Sum(ct => ct.SanPham.Gia * ct.SoLuong),
                        ChiTiet = khachHang.TaiKhoan.GioHang.GioHangChiTiets.Select(ct => new SanPhamGioHangDTO
                        {
                            SanPhamId = ct.SanPhamId,
                            TenSanPham = ct.SanPham.TenSanPham,
                            SoLuong = ct.SoLuong,
                            DonGia = ct.SanPham.Gia,
                            ThanhTien = ct.SanPham.Gia * ct.SoLuong
                        }).ToList()
                    } : null
                };

                return Ok(ApiResponse<KhachHangDetailDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<KhachHangDetailDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<KhachHangDTO>>> CreateKhachHang(CreateKhachHangDTO createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra email đã tồn tại
                if (await _context.KhachHangs.AnyAsync(k => k.Email == createDto.Email))
                    return BadRequest(ApiResponse<KhachHangDTO>.Fail("Email đã được sử dụng"));

                // Kiểm tra số điện thoại đã tồn tại
                if (await _context.KhachHangs.AnyAsync(k => k.SoDienThoai == createDto.SoDienThoai))
                    return BadRequest(ApiResponse<KhachHangDTO>.Fail("Số điện thoại đã được sử dụng"));

                var khachHang = new KhachHang
                {
                    TenKhachHang = createDto.TenKhachHang,
                    DiaChi = createDto.DiaChi,
                    Email = createDto.Email,
                    SoDienThoai = createDto.SoDienThoai
                };

                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var result = new KhachHangDTO
                {
                    Id = khachHang.Id,
                    TenKhachHang = khachHang.TenKhachHang,
                    DiaChi = khachHang.DiaChi,
                    Email = khachHang.Email,
                    SoDienThoai = khachHang.SoDienThoai,
                    TongDonHang = 0,
                    TongTienMua = 0,
                    CreatedAt = khachHang.CreatedAt
                };

                return CreatedAtAction(nameof(GetKhachHangById), new { id = khachHang.Id },
                    ApiResponse<KhachHangDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<KhachHangDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<KhachHangDTO>>> UpdateKhachHang(int id, UpdateKhachHangDTO updateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                var khachHang = await _context.KhachHangs
                    .Include(k => k.TaiKhoan)
                    .FirstOrDefaultAsync(k => k.Id == id);

                if (khachHang == null)
                    return NotFound(ApiResponse<KhachHangDTO>.Fail("Không tìm thấy khách hàng"));

                // Kiểm tra quyền truy cập
                if (userRole != "Admin" && khachHang.TaiKhoan.Id.ToString() != userId)
                    return Forbid();

                // Kiểm tra số điện thoại đã tồn tại
                if (await _context.KhachHangs.AnyAsync(k => k.SoDienThoai == updateDto.SoDienThoai && k.Id != id))
                    return BadRequest(ApiResponse<KhachHangDTO>.Fail("Số điện thoại đã được sử dụng"));

                khachHang.TenKhachHang = updateDto.TenKhachHang;
                khachHang.DiaChi = updateDto.DiaChi;
                khachHang.SoDienThoai = updateDto.SoDienThoai;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var result = new KhachHangDTO
                {
                    Id = khachHang.Id,
                    TenKhachHang = khachHang.TenKhachHang,
                    DiaChi = khachHang.DiaChi,
                    Email = khachHang.Email,
                    SoDienThoai = khachHang.SoDienThoai,
                    CreatedAt = khachHang.CreatedAt,
                    UpdatedAt = khachHang.UpdatedAt
                };

                return Ok(ApiResponse<KhachHangDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<KhachHangDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
        [HttpGet("statistics")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<KhachHangThongKeDTO>>> GetStatistics()
        {
            try
            {
                var statistics = new KhachHangThongKeDTO
                {
                    TongKhachHang = await _context.KhachHangs.CountAsync(),
                    KhachHangMoi = await _context.KhachHangs
                        .CountAsync(k => k.CreatedAt.Month == DateTime.Now.Month),
                    KhachHangHoatDong = await _context.KhachHangs
                        .CountAsync(k => k.TaiKhoan.Status == ENums.TrangThaiTaiKhoan.HoatDong),
                    DoanhThu = await _context.DonHangs
                        .Where(d => d.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh)
                        .SelectMany(d => d.DonHangChiTiets)
                        .SumAsync(ct => ct.GiaTri * ct.SoLuong),
                    Top10KhachHang = await _context.KhachHangs
                        .Include(k => k.TaiKhoan)
                            .ThenInclude(t => t.DonHangs)
                                .ThenInclude(d => d.DonHangChiTiets)
                        .Select(k => new KhachHangDTO
                        {
                            Id = k.Id,
                            TenKhachHang = k.TenKhachHang,
                            Email = k.Email,
                            SoDienThoai = k.SoDienThoai,
                            TrangThaiTaiKhoan = k.TaiKhoan.Status.ToString(),
                            TongDonHang = k.TaiKhoan.DonHangs.Count,
                            TongTienMua = k.TaiKhoan.DonHangs
                                .Where(d => d.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh)
                                .SelectMany(d => d.DonHangChiTiets)
                                .Sum(ct => ct.GiaTri * ct.SoLuong)
                        })
                        .OrderByDescending(k => k.TongTienMua)
                        .Take(10)
                        .ToListAsync()
                };

                return Ok(ApiResponse<KhachHangThongKeDTO>.Succeed(statistics));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<KhachHangThongKeDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteKhachHang(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var khachHang = await _context.KhachHangs
                    .Include(k => k.TaiKhoan)
                    .FirstOrDefaultAsync(k => k.Id == id);

                if (khachHang == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy khách hàng"));

                if (khachHang.TaiKhoan?.PhanQuyen?.TenPhanQuyen == "Admin")
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa khách hàng có tài khoản Admin"));

                // Kiểm tra có đơn hàng không
                if (await _context.DonHangs.AnyAsync(d => d.TaiKhoanId == khachHang.TaiKhoan.Id))
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa khách hàng đã có đơn hàng"));

                if (khachHang.TaiKhoan != null)
                {
                    // Xóa giỏ hàng nếu có
                    var gioHang = await _context.GioHangs
                        .Include(g => g.GioHangChiTiets)
                        .FirstOrDefaultAsync(g => g.TaiKhoanId == khachHang.TaiKhoan.Id);

                    if (gioHang != null)
                    {
                        _context.GioHangChiTiets.RemoveRange(gioHang.GioHangChiTiets);
                        _context.GioHangs.Remove(gioHang);
                    }

                    // Xóa thông báo
                    var thongBaos = await _context.ThongBaos
                        .Where(t => t.TaiKhoanId == khachHang.TaiKhoan.Id)
                        .ToListAsync();
                    _context.ThongBaos.RemoveRange(thongBaos);

                    // Xóa tài khoản
                    _context.TaiKhoans.Remove(khachHang.TaiKhoan);
                }

                // Xóa khách hàng
                _context.KhachHangs.Remove(khachHang);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Xóa khách hàng thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
    }
}