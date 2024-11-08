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
    public class DonHangController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DonHangController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<IEnumerable<DonHangDTO>>>> GetAllDonHang()
        {
            try
            {
                var donHangs = await _context.DonHangs
                    .Include(d => d.TaiKhoan)
                        .ThenInclude(t => t.KhachHang)
                    .Include(d => d.NhanVien)
                    .Include(d => d.PhuongThucThanhToan)
                    .Include(d => d.DonHangChiTiets)
                    .Select(d => new DonHangDTO
                    {
                        Id = d.Id,
                        TaiKhoanId = d.TaiKhoanId,
                        TenKhachHang = d.TaiKhoan.KhachHang.TenKhachHang,
                        NhanVienId = d.NhanVienId,
                        TenNhanVien = d.NhanVien.TenNhanVien,
                        DiaChiGiaoHang = d.DiaChiGiaoHang,
                        TrangThai = d.TrangThai.ToString(),
                        GhiChu = d.GhiChu,
                        PhuongThucThanhToan = d.PhuongThucThanhToan.TenThanhToan,
                        TongTien = d.DonHangChiTiets.Sum(ct => ct.GiaTri * ct.SoLuong),
                        TongSanPham = d.DonHangChiTiets.Sum(ct => ct.SoLuong),
                        CreatedAt = d.CreatedAt,
                        UpdatedAt = d.UpdatedAt
                    })
                    .OrderByDescending(d => d.CreatedAt)
                    .ToListAsync();

                return Ok(ApiResponse<IEnumerable<DonHangDTO>>.Succeed(donHangs));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<DonHangDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("my-orders")]
        public async Task<ActionResult<ApiResponse<IEnumerable<DonHangDTO>>>> GetMyOrders()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var donHangs = await _context.DonHangs
                    .Include(d => d.TaiKhoan)
                        .ThenInclude(t => t.KhachHang)
                    .Include(d => d.NhanVien)
                    .Include(d => d.PhuongThucThanhToan)
                    .Include(d => d.DonHangChiTiets)
                    .Where(d => d.TaiKhoanId == int.Parse(userId))
                    .Select(d => new DonHangDTO
                    {
                        Id = d.Id,
                        TaiKhoanId = d.TaiKhoanId,
                        TenKhachHang = d.TaiKhoan.KhachHang.TenKhachHang,
                        NhanVienId = d.NhanVienId,
                        TenNhanVien = d.NhanVien.TenNhanVien,
                        DiaChiGiaoHang = d.DiaChiGiaoHang,
                        TrangThai = d.TrangThai.ToString(),
                        GhiChu = d.GhiChu,
                        PhuongThucThanhToan = d.PhuongThucThanhToan.TenThanhToan,
                        TongTien = d.DonHangChiTiets.Sum(ct => ct.GiaTri * ct.SoLuong),
                        TongSanPham = d.DonHangChiTiets.Sum(ct => ct.SoLuong),
                        CreatedAt = d.CreatedAt,
                        UpdatedAt = d.UpdatedAt
                    })
                    .OrderByDescending(d => d.CreatedAt)
                    .ToListAsync();

                return Ok(ApiResponse<IEnumerable<DonHangDTO>>.Succeed(donHangs));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<DonHangDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DonHangDetailDTO>>> GetDonHangById(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                var donHang = await _context.DonHangs
                    .Include(d => d.TaiKhoan)
                        .ThenInclude(t => t.KhachHang)
                    .Include(d => d.NhanVien)
                    .Include(d => d.PhuongThucThanhToan)
                    .Include(d => d.DonHangChiTiets)
                        .ThenInclude(ct => ct.SanPham)
                    .Include(d => d.DonHangChiTiets)
                        .ThenInclude(ct => ct.MaGiamGia)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (donHang == null)
                    return NotFound(ApiResponse<DonHangDetailDTO>.Fail("Không tìm thấy đơn hàng"));

                // Kiểm tra quyền truy cập
                if (userRole != "Admin" && donHang.TaiKhoanId != int.Parse(userId))
                    return Forbid();

                var result = new DonHangDetailDTO
                {
                    Id = donHang.Id,
                    MaDonHang = $"DH{donHang.Id:D6}",
                    ThongTinKhachHang = new ThongTinKhachHangDTO
                    {
                        Id = donHang.TaiKhoan.KhachHang.Id,
                        TenKhachHang = donHang.TaiKhoan.KhachHang.TenKhachHang,
                        Email = donHang.TaiKhoan.KhachHang.Email,
                        SoDienThoai = donHang.TaiKhoan.KhachHang.SoDienThoai
                    },
                    ThongTinNhanVien = donHang.NhanVien != null ? new ThongTinNhanVienDTO
                    {
                        Id = donHang.NhanVien.Id,
                        TenNhanVien = donHang.NhanVien.TenNhanVien,
                        SoDienThoai = donHang.NhanVien.SoDienThoai
                    } : null,
                    DiaChiGiaoHang = donHang.DiaChiGiaoHang,
                    TrangThai = donHang.TrangThai.ToString(),
                    GhiChu = donHang.GhiChu,
                    PhuongThucThanhToan = donHang.PhuongThucThanhToan.TenThanhToan,
                    TongTienHang = donHang.DonHangChiTiets.Sum(ct => ct.GiaTri * ct.SoLuong),
                    TongGiamGia = donHang.DonHangChiTiets
                        .Where(ct => ct.MaGiamGiaId != null)
                        .Sum(ct => ct.GiaTri * ct.SoLuong * ct.MaGiamGia.PhanTramGiam / 100),
                    NgayDat = donHang.CreatedAt,
                    NgayXuLy = donHang.UpdatedAt,
                    ChiTietDonHang = donHang.DonHangChiTiets.Select(ct => new DonHangChiTietDTO
                    {
                        Id = ct.Id,
                        SanPhamId = ct.SanPhamId,
                        TenSanPham = ct.SanPham.TenSanPham,
                        MaGiamGiaId = ct.MaGiamGiaId,
                        TenMaGiamGia = ct.MaGiamGia?.TenMa,
                        IMEI = ct.IMEI,
                        SoLuong = ct.SoLuong,
                        GiaTri = ct.GiaTri,
                        ThanhTien = ct.GiaTri * ct.SoLuong,
                        GhiChu = ct.GhiChu
                    }).ToList()
                };

                return Ok(ApiResponse<DonHangDetailDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<DonHangDetailDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DonHangDTO>>> CreateDonHang(CreateDonHangDTO createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                // Kiểm tra phương thức thanh toán
                var phuongThucThanhToan = await _context.PhuongThucThanhToans
                    .FindAsync(createDto.PhuongThucThanhToanId);
                if (phuongThucThanhToan == null)
                    return BadRequest(ApiResponse<DonHangDTO>.Fail("Phương thức thanh toán không tồn tại"));

                // Tạo đơn hàng
                var donHang = new DonHang
                {
                    TaiKhoanId = userId,
                    PhuongThucThanhToanId = createDto.PhuongThucThanhToanId,
                    DiaChiGiaoHang = createDto.DiaChiGiaoHang,
                    GhiChu = createDto.GhiChu,
                    TrangThai = ENums.TrangThaiDonHang.Moi
                };

                _context.DonHangs.Add(donHang);
                await _context.SaveChangesAsync();

                decimal tongTien = 0;
                int tongSoLuong = 0;

                // Thêm chi tiết đơn hàng
                foreach (var chiTiet in createDto.ChiTietDonHang)
                {
                    // Kiểm tra sản phẩm
                    var sanPham = await _context.SanPhams.FindAsync(chiTiet.SanPhamId);
                    if (sanPham == null)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(ApiResponse<DonHangDTO>.Fail($"Sản phẩm {chiTiet.SanPhamId} không tồn tại"));
                    }

                    // Kiểm tra số lượng
                    if (sanPham.ConLai < chiTiet.SoLuong)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(ApiResponse<DonHangDTO>.Fail($"Sản phẩm {sanPham.TenSanPham} không đủ số lượng"));
                    }

                    // Kiểm tra mã giảm giá nếu có
                    if (chiTiet.MaGiamGiaId.HasValue)
                    {
                        var maGiamGia = await _context.MaGiamGias.FindAsync(chiTiet.MaGiamGiaId);
                        if (maGiamGia == null || !maGiamGia.IsActive || maGiamGia.NgayHetHan < DateTime.Now)
                        {
                            await transaction.RollbackAsync();
                            return BadRequest(ApiResponse<DonHangDTO>.Fail("Mã giảm giá không hợp lệ hoặc đã hết hạn"));
                        }
                    }

                    // Thêm chi tiết
                    var donHangChiTiet = new DonHangChiTiet
                    {
                        DonHangId = donHang.Id,
                        SanPhamId = chiTiet.SanPhamId,
                        MaGiamGiaId = chiTiet.MaGiamGiaId,
                        IMEI = chiTiet.IMEI,
                        SoLuong = chiTiet.SoLuong,
                        GiaTri = sanPham.Gia,
                        GhiChu = chiTiet.GhiChu
                    };

                    _context.DonHangChiTiets.Add(donHangChiTiet);

                    // Cập nhật số lượng sản phẩm
                    sanPham.ConLai -= chiTiet.SoLuong;
                    if (sanPham.ConLai == 0)
                        sanPham.Status = ENums.TrangThaiSanPham.HetHang;

                    tongTien += sanPham.Gia * chiTiet.SoLuong;
                    tongSoLuong += chiTiet.SoLuong;
                }

                // Xóa giỏ hàng nếu có
                var gioHang = await _context.GioHangs
                    .Include(g => g.GioHangChiTiets)
                    .FirstOrDefaultAsync(g => g.TaiKhoanId == userId);

                if (gioHang != null)
                {
                    _context.GioHangChiTiets.RemoveRange(gioHang.GioHangChiTiets);
                    _context.GioHangs.Remove(gioHang);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Tạo response
                var donHangDTO = new DonHangDTO
                {
                    Id = donHang.Id,
                    TaiKhoanId = donHang.TaiKhoanId,
                    TenKhachHang = donHang.TaiKhoan.KhachHang.TenKhachHang,
                    DiaChiGiaoHang = donHang.DiaChiGiaoHang,
                    TrangThai = donHang.TrangThai.ToString(),
                    GhiChu = donHang.GhiChu,
                    PhuongThucThanhToan = phuongThucThanhToan.TenThanhToan,
                    TongTien = tongTien,
                    TongSanPham = tongSoLuong,
                    CreatedAt = donHang.CreatedAt
                };

                return CreatedAtAction(nameof(GetDonHangById), new { id = donHang.Id },
                    ApiResponse<DonHangDTO>.Succeed(donHangDTO));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<DonHangDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("status/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateTrangThai(int id, UpdateTrangThaiDonHangDTO updateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var donHang = await _context.DonHangs
                    .Include(d => d.DonHangChiTiets)
                        .ThenInclude(ct => ct.SanPham)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (donHang == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy đơn hàng"));

                // Kiểm tra trạng thái hợp lệ
                if (!Enum.TryParse<ENums.TrangThaiDonHang>(updateDto.TrangThai, out var trangThaiMoi))
                    return BadRequest(ApiResponse<bool>.Fail("Trạng thái không hợp lệ"));

                // Kiểm tra logic chuyển trạng thái
                if (!IsValidStatusTransition(donHang.TrangThai, trangThaiMoi))
                    return BadRequest(ApiResponse<bool>.Fail("Không thể chuyển sang trạng thái này"));

                // Xử lý khi hủy đơn
                if (trangThaiMoi == ENums.TrangThaiDonHang.DaHuy)
                {
                    foreach (var chiTiet in donHang.DonHangChiTiets)
                    {
                        chiTiet.SanPham.ConLai += chiTiet.SoLuong;
                        if (chiTiet.SanPham.Status == ENums.TrangThaiSanPham.HetHang)
                            chiTiet.SanPham.Status = ENums.TrangThaiSanPham.ConHang;
                    }
                }

                donHang.TrangThai = trangThaiMoi;
                donHang.GhiChu = updateDto.GhiChu;

                if (trangThaiMoi == ENums.TrangThaiDonHang.DaHoanThanh)
                    donHang.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Cập nhật trạng thái thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("cancel/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> CancelDonHang(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var donHang = await _context.DonHangs
                    .Include(d => d.DonHangChiTiets)
                        .ThenInclude(ct => ct.SanPham)
                    .FirstOrDefaultAsync(d => d.Id == id && d.TaiKhoanId == userId);

                if (donHang == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy đơn hàng"));

                if (donHang.TrangThai != ENums.TrangThaiDonHang.Moi &&
                    donHang.TrangThai != ENums.TrangThaiDonHang.DangXuLy)
                    return BadRequest(ApiResponse<bool>.Fail("Không thể hủy đơn hàng này"));

                foreach (var chiTiet in donHang.DonHangChiTiets)
                {
                    chiTiet.SanPham.ConLai += chiTiet.SoLuong;
                    if (chiTiet.SanPham.Status == ENums.TrangThaiSanPham.HetHang)
                        chiTiet.SanPham.Status = ENums.TrangThaiSanPham.ConHang;
                }

                donHang.TrangThai = ENums.TrangThaiDonHang.DaHuy;
                donHang.GhiChu = "Khách hàng hủy đơn";

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Hủy đơn hàng thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        private bool IsValidStatusTransition(ENums.TrangThaiDonHang fromStatus, ENums.TrangThaiDonHang toStatus)
        {
            return (fromStatus, toStatus) switch
            {
                (ENums.TrangThaiDonHang.Moi, ENums.TrangThaiDonHang.DangXuLy) => true,
                (ENums.TrangThaiDonHang.Moi, ENums.TrangThaiDonHang.DaHuy) => true,
                (ENums.TrangThaiDonHang.DangXuLy, ENums.TrangThaiDonHang.DaHoanThanh) => true,
                (ENums.TrangThaiDonHang.DangXuLy, ENums.TrangThaiDonHang.DaHuy) => true,
                (ENums.TrangThaiDonHang.DaHoanThanh, _) => false,
                (ENums.TrangThaiDonHang.DaHuy, _) => false,
                _ => false
            };
        }
    }
}