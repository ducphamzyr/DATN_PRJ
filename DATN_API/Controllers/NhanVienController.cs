using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DATN_API.DTOs;
using DATN_API.DTOs.Common;
using DATN_API.Models;
using System.Text;
using System.Security.Cryptography;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class NhanVienController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NhanVienController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<NhanVienDTO>>>> GetAllNhanVien()
        {
            try
            {
                var nhanViens = await _context.NhanViens
                    .Include(nv => nv.TaiKhoan)
                    .Select(nv => new NhanVienDTO
                    {
                        Id = nv.Id,
                        TenNhanVien = nv.TenNhanVien,
                        SoDienThoai = nv.SoDienThoai,
                        DiaChiThuongTru = nv.DiaChiThuongTru,
                        QueQuan = nv.QueQuan,
                        SoCCCD = nv.SoCCCD,
                        LuongHienTai = nv.LuongHienTai,
                        TrangThai = nv.TrangThai.ToString(),
                        NgayNhanViec = nv.NgayNhanViec,
                        TaiKhoanId = nv.TaiKhoanId,
                        TenDangNhap = nv.TaiKhoan != null ? nv.TaiKhoan.TenDangNhap : null,
                        CreatedAt = nv.CreatedAt,
                        UpdatedAt = nv.UpdatedAt
                    })
                    .ToListAsync();

                return Ok(ApiResponse<IEnumerable<NhanVienDTO>>.Succeed(nhanViens));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<NhanVienDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<NhanVienDetailDTO>>> GetNhanVienById(int id)
        {
            try
            {
                var nhanVien = await _context.NhanViens
                    .Include(nv => nv.TaiKhoan)
                        .ThenInclude(t => t.PhanQuyen)
                    .Include(nv => nv.DonHangs)
                        .ThenInclude(d => d.TaiKhoan)
                            .ThenInclude(t => t.KhachHang)
                    .FirstOrDefaultAsync(nv => nv.Id == id);

                if (nhanVien == null)
                    return NotFound(ApiResponse<NhanVienDetailDTO>.Fail("Không tìm thấy nhân viên"));

                var result = new NhanVienDetailDTO
                {
                    Id = nhanVien.Id,
                    TenNhanVien = nhanVien.TenNhanVien,
                    SoDienThoai = nhanVien.SoDienThoai,
                    DiaChiThuongTru = nhanVien.DiaChiThuongTru,
                    QueQuan = nhanVien.QueQuan,
                    SoCCCD = nhanVien.SoCCCD,
                    LuongHienTai = nhanVien.LuongHienTai,
                    TrangThai = nhanVien.TrangThai.ToString(),
                    NgayNhanViec = nhanVien.NgayNhanViec,
                    CreatedAt = nhanVien.CreatedAt,
                    UpdatedAt = nhanVien.UpdatedAt,

                    ThongTinTaiKhoan = nhanVien.TaiKhoan != null ? new TaiKhoanNhanVienDTO
                    {
                        Id = nhanVien.TaiKhoan.Id,
                        TenDangNhap = nhanVien.TaiKhoan.TenDangNhap,
                        TenPhanQuyen = nhanVien.TaiKhoan.PhanQuyen.TenPhanQuyen,
                        TrangThai = nhanVien.TaiKhoan.Status.ToString()
                    } : null,

                    ThongKeDonHang = new ThongKeDonHangNhanVienDTO
                    {
                        TongDonHang = nhanVien.DonHangs.Count,
                        DonHangMoi = nhanVien.DonHangs.Count(d => d.TrangThai == ENums.TrangThaiDonHang.Moi),
                        DonHangDangXuLy = nhanVien.DonHangs.Count(d => d.TrangThai == ENums.TrangThaiDonHang.DangXuLy),
                        DonHangHoanThanh = nhanVien.DonHangs.Count(d => d.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh),
                        DonHangHuy = nhanVien.DonHangs.Count(d => d.TrangThai == ENums.TrangThaiDonHang.DaHuy)
                    },

                    DonHangGanDay = nhanVien.DonHangs
                        .OrderByDescending(d => d.CreatedAt)
                        .Take(10)
                        .Select(d => new DonHangGanDayDTO
                        {
                            Id = d.Id,
                            TenKhachHang = d.TaiKhoan.KhachHang.TenKhachHang,
                            TrangThai = d.TrangThai.ToString(),
                            NgayDat = d.CreatedAt,
                            NgayCapNhat = d.UpdatedAt
                        }).ToList()
                };

                return Ok(ApiResponse<NhanVienDetailDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<NhanVienDetailDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<NhanVienDTO>>> CreateNhanVien(CreateNhanVienDTO createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra CCCD đã tồn tại
                if (await _context.NhanViens.AnyAsync(nv => nv.SoCCCD == createDto.SoCCCD))
                    return BadRequest(ApiResponse<NhanVienDTO>.Fail("Số CCCD đã tồn tại"));

                // Kiểm tra số điện thoại đã tồn tại
                if (await _context.NhanViens.AnyAsync(nv => nv.SoDienThoai == createDto.SoDienThoai))
                    return BadRequest(ApiResponse<NhanVienDTO>.Fail("Số điện thoại đã tồn tại"));

                var nhanVien = new NhanVien
                {
                    TenNhanVien = createDto.TenNhanVien,
                    SoDienThoai = createDto.SoDienThoai,
                    DiaChiThuongTru = createDto.DiaChiThuongTru,
                    QueQuan = createDto.QueQuan,
                    SoCCCD = createDto.SoCCCD,
                    LuongHienTai = createDto.LuongHienTai,
                    NgayNhanViec = createDto.NgayNhanViec,
                    TrangThai = ENums.TrangThaiNhanVien.HoatDong
                };

                _context.NhanViens.Add(nhanVien);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var result = new NhanVienDTO
                {
                    Id = nhanVien.Id,
                    TenNhanVien = nhanVien.TenNhanVien,
                    SoDienThoai = nhanVien.SoDienThoai,
                    DiaChiThuongTru = nhanVien.DiaChiThuongTru,
                    QueQuan = nhanVien.QueQuan,
                    SoCCCD = nhanVien.SoCCCD,
                    LuongHienTai = nhanVien.LuongHienTai,
                    TrangThai = nhanVien.TrangThai.ToString(),
                    NgayNhanViec = nhanVien.NgayNhanViec,
                    CreatedAt = nhanVien.CreatedAt
                };

                return CreatedAtAction(nameof(GetNhanVienById), new { id = nhanVien.Id },
                    ApiResponse<NhanVienDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<NhanVienDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<NhanVienDTO>>> UpdateNhanVien(int id, UpdateNhanVienDTO updateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nhanVien = await _context.NhanViens.FindAsync(id);
                if (nhanVien == null)
                    return NotFound(ApiResponse<NhanVienDTO>.Fail("Không tìm thấy nhân viên"));

                // Kiểm tra CCCD đã tồn tại
                if (await _context.NhanViens.AnyAsync(nv => nv.SoCCCD == updateDto.SoCCCD && nv.Id != id))
                    return BadRequest(ApiResponse<NhanVienDTO>.Fail("Số CCCD đã tồn tại"));

                // Kiểm tra số điện thoại đã tồn tại
                if (await _context.NhanViens.AnyAsync(nv => nv.SoDienThoai == updateDto.SoDienThoai && nv.Id != id))
                    return BadRequest(ApiResponse<NhanVienDTO>.Fail("Số điện thoại đã tồn tại"));

                nhanVien.TenNhanVien = updateDto.TenNhanVien;
                nhanVien.SoDienThoai = updateDto.SoDienThoai;
                nhanVien.DiaChiThuongTru = updateDto.DiaChiThuongTru;
                nhanVien.QueQuan = updateDto.QueQuan;
                nhanVien.SoCCCD = updateDto.SoCCCD;
                nhanVien.LuongHienTai = updateDto.LuongHienTai;
                nhanVien.NgayNhanViec = updateDto.NgayNhanViec;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var result = new NhanVienDTO
                {
                    Id = nhanVien.Id,
                    TenNhanVien = nhanVien.TenNhanVien,
                    SoDienThoai = nhanVien.SoDienThoai,
                    DiaChiThuongTru = nhanVien.DiaChiThuongTru,
                    QueQuan = nhanVien.QueQuan,
                    SoCCCD = nhanVien.SoCCCD,
                    LuongHienTai = nhanVien.LuongHienTai,
                    TrangThai = nhanVien.TrangThai.ToString(),
                    NgayNhanViec = nhanVien.NgayNhanViec,
                    CreatedAt = nhanVien.CreatedAt,
                    UpdatedAt = nhanVien.UpdatedAt
                };

                return Ok(ApiResponse<NhanVienDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<NhanVienDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("status/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateTrangThai(int id, [FromBody] string trangThai)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nhanVien = await _context.NhanViens.FindAsync(id);
                if (nhanVien == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy nhân viên"));

                if (!Enum.TryParse<ENums.TrangThaiNhanVien>(trangThai, out var trangThaiMoi))
                    return BadRequest(ApiResponse<bool>.Fail("Trạng thái không hợp lệ"));

                nhanVien.TrangThai = trangThaiMoi;
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
        [HttpPost("{id}/assign-account")]
        public async Task<ActionResult<ApiResponse<bool>>> AssignAccount(int id, AssignTaiKhoanDTO assignDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nhanVien = await _context.NhanViens
                    .Include(nv => nv.TaiKhoan)
                    .FirstOrDefaultAsync(nv => nv.Id == id);

                if (nhanVien == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy nhân viên"));

                if (nhanVien.TaiKhoanId != null)
                    return BadRequest(ApiResponse<bool>.Fail("Nhân viên đã có tài khoản"));

                // Kiểm tra tên đăng nhập đã tồn tại
                if (await _context.TaiKhoans.AnyAsync(t => t.TenDangNhap == assignDto.TenDangNhap))
                    return BadRequest(ApiResponse<bool>.Fail("Tên đăng nhập đã tồn tại"));

                // Lấy phân quyền nhân viên
                var phanQuyenNhanVien = await _context.PhanQuyens
                    .FirstOrDefaultAsync(pq => pq.TenPhanQuyen == "NhanVien");

                if (phanQuyenNhanVien == null)
                    return StatusCode(500, ApiResponse<bool>.Fail("Không tìm thấy phân quyền nhân viên"));

                // Tạo tài khoản cho nhân viên
                var taiKhoan = new TaiKhoan
                {
                    TenDangNhap = assignDto.TenDangNhap,
                    MatKhauHash = HashPassword(assignDto.MatKhau),
                    PhanQuyenId = phanQuyenNhanVien.Id,
                    Status = ENums.TrangThaiTaiKhoan.HoatDong
                };

                _context.TaiKhoans.Add(taiKhoan);
                await _context.SaveChangesAsync();

                // Gán tài khoản cho nhân viên
                nhanVien.TaiKhoanId = taiKhoan.Id;
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Tạo tài khoản nhân viên thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpDelete("{id}/remove-account")]
        public async Task<ActionResult<ApiResponse<bool>>> RemoveAccount(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nhanVien = await _context.NhanViens
                    .Include(nv => nv.TaiKhoan)
                    .FirstOrDefaultAsync(nv => nv.Id == id);

                if (nhanVien == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy nhân viên"));

                if (nhanVien.TaiKhoanId == null)
                    return BadRequest(ApiResponse<bool>.Fail("Nhân viên chưa có tài khoản"));

                // Kiểm tra đơn hàng
                if (await _context.DonHangs.AnyAsync(d => d.NhanVienId == id))
                    return BadRequest(ApiResponse<bool>.Fail("Không thể xóa tài khoản của nhân viên đã xử lý đơn hàng"));

                var taiKhoan = nhanVien.TaiKhoan;
                nhanVien.TaiKhoanId = null;
                _context.TaiKhoans.Remove(taiKhoan);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Xóa tài khoản nhân viên thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<ApiResponse<ThongKeNhanVienDTO>>> GetStatistics()
        {
            try
            {
                var thongKe = new ThongKeNhanVienDTO
                {
                    TongNhanVien = await _context.NhanViens.CountAsync(),
                    NhanVienHoatDong = await _context.NhanViens
                        .CountAsync(nv => nv.TrangThai == ENums.TrangThaiNhanVien.HoatDong),
                    NhanVienMoi = await _context.NhanViens
                        .CountAsync(nv => nv.NgayNhanViec.Month == DateTime.Now.Month
                            && nv.NgayNhanViec.Year == DateTime.Now.Year),
                    NhanVienNghi = await _context.NhanViens
                        .CountAsync(nv => nv.TrangThai == ENums.TrangThaiNhanVien.TamNghi),
                    TongLuong = await _context.NhanViens
                        .Where(nv => nv.TrangThai == ENums.TrangThaiNhanVien.HoatDong)
                        .SumAsync(nv => nv.LuongHienTai),
                    ThongKeTheoDonHang = await _context.NhanViens
                        .Where(nv => nv.DonHangs.Any())
                        .Select(nv => new ThongKeDonHangNhanVienDTO
                        {
                            NhanVienId = nv.Id,
                            TenNhanVien = nv.TenNhanVien,
                            TongDonHang = nv.DonHangs.Count,
                            DonHangHoanThanh = nv.DonHangs.Count(d => d.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh),
                            DonHangHuy = nv.DonHangs.Count(d => d.TrangThai == ENums.TrangThaiDonHang.DaHuy)
                        }).ToListAsync()
                };

                return Ok(ApiResponse<ThongKeNhanVienDTO>.Succeed(thongKe));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ThongKeNhanVienDTO>.Fail($"Lỗi server: {ex.Message}"));
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