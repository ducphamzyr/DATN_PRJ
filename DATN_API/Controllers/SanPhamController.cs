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
    public class SanPhamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SanPhamController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<SanPhamDTO>>>> GetAllSanPham(
            [FromQuery] string? search,
            [FromQuery] int? nhanHieuId,
            [FromQuery] int? phanLoaiId,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string? status)
        {
            try
            {
                var query = _context.SanPhams
                    .Include(s => s.NhanHieu)
                    .Include(s => s.PhanLoai)
                    .AsQueryable();

                // Áp dụng các bộ lọc
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(s => s.TenSanPham.Contains(search) ||
                                           s.NhanHieu.TenNhanHieu.Contains(search));
                }

                if (nhanHieuId.HasValue)
                {
                    query = query.Where(s => s.NhanHieuId == nhanHieuId);
                }

                if (phanLoaiId.HasValue)
                {
                    query = query.Where(s => s.PhanLoaiId == phanLoaiId);
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(s => s.Gia >= minPrice);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(s => s.Gia <= maxPrice);
                }

                if (!string.IsNullOrEmpty(status) && Enum.TryParse<ENums.TrangThaiSanPham>(status, out var trangThai))
                {
                    query = query.Where(s => s.Status == trangThai);
                }

                var sanPhams = await query
                    .Select(s => new SanPhamDTO
                    {
                        Id = s.Id,
                        TenSanPham = s.TenSanPham,
                        NhanHieuId = s.NhanHieuId,
                        TenNhanHieu = s.NhanHieu.TenNhanHieu,
                        PhanLoaiId = s.PhanLoaiId,
                        TenPhanLoai = s.PhanLoai.TenPhanLoai,
                        TrangThai = s.Status.ToString(),
                        GhiChu = s.GhiChu,
                        Mau = s.Mau,
                        BoNho = s.BoNho,
                        KheSim = s.KheSim,
                        Ram = s.Ram,
                        Gia = s.Gia,
                        ConLai = s.ConLai,
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    })
                    .ToListAsync();

                return Ok(ApiResponse<IEnumerable<SanPhamDTO>>.Succeed(sanPhams));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<SanPhamDTO>>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<SanPhamDetailDTO>>> GetSanPhamById(string id)
        {
            try
            {
                var sanPham = await _context.SanPhams
                    .Include(s => s.NhanHieu)
                    .Include(s => s.PhanLoai)
                    .Include(s => s.DonHangChiTiets)
                        .ThenInclude(ct => ct.DonHang)
                    .Include(s => s.GioHangChiTiets)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (sanPham == null)
                    return NotFound(ApiResponse<SanPhamDetailDTO>.Fail("Không tìm thấy sản phẩm"));

                var result = new SanPhamDetailDTO
                {
                    Id = sanPham.Id,
                    TenSanPham = sanPham.TenSanPham,
                    NhanHieu = new NhanHieuDTO
                    {
                        Id = sanPham.NhanHieu.Id,
                        TenNhanHieu = sanPham.NhanHieu.TenNhanHieu
                    },
                    PhanLoai = new PhanLoaiDTO
                    {
                        Id = sanPham.PhanLoai.Id,
                        TenPhanLoai = sanPham.PhanLoai.TenPhanLoai
                    },
                    TrangThai = sanPham.Status.ToString(),
                    GhiChu = sanPham.GhiChu,
                    Mau = sanPham.Mau,
                    BoNho = sanPham.BoNho,
                    KheSim = sanPham.KheSim,
                    Ram = sanPham.Ram,
                    Gia = sanPham.Gia,
                    ConLai = sanPham.ConLai,
                    CreatedAt = sanPham.CreatedAt,
                    UpdatedAt = sanPham.UpdatedAt,
                    TongDaBan = sanPham.DonHangChiTiets
                        .Count(ct => ct.DonHang.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh),
                    DangTrongGioHang = sanPham.GioHangChiTiets.Sum(ct => ct.SoLuong),
                    DoanhThu = sanPham.DonHangChiTiets
                        .Where(ct => ct.DonHang.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh)
                        .Sum(ct => ct.GiaTri * ct.SoLuong)
                };

                return Ok(ApiResponse<SanPhamDetailDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<SanPhamDetailDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<SanPhamDTO>>> CreateSanPham(CreateSanPhamDTO createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra nhãn hiệu và phân loại
                var nhanHieu = await _context.NhanHieus.FindAsync(createDto.NhanHieuId);
                var phanLoai = await _context.PhanLoais.FindAsync(createDto.PhanLoaiId);

                if (nhanHieu == null || phanLoai == null)
                    return BadRequest(ApiResponse<SanPhamDTO>.Fail("Nhãn hiệu hoặc phân loại không tồn tại"));

                // Tạo mã sản phẩm
                string productId = await GenerateProductId(nhanHieu.TenNhanHieu);

                var sanPham = new SanPham
                {
                    Id = productId,
                    TenSanPham = createDto.TenSanPham,
                    NhanHieuId = createDto.NhanHieuId,
                    PhanLoaiId = createDto.PhanLoaiId,
                    GhiChu = createDto.GhiChu,
                    Mau = createDto.Mau,
                    BoNho = createDto.BoNho,
                    KheSim = createDto.KheSim,
                    Ram = createDto.Ram,
                    Gia = createDto.Gia,
                    ConLai = createDto.ConLai,
                    Status = createDto.ConLai > 0 ? ENums.TrangThaiSanPham.ConHang : ENums.TrangThaiSanPham.HetHang
                };

                _context.SanPhams.Add(sanPham);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var result = new SanPhamDTO
                {
                    Id = sanPham.Id,
                    TenSanPham = sanPham.TenSanPham,
                    NhanHieuId = sanPham.NhanHieuId,
                    TenNhanHieu = nhanHieu.TenNhanHieu,
                    PhanLoaiId = sanPham.PhanLoaiId,
                    TenPhanLoai = phanLoai.TenPhanLoai,
                    TrangThai = sanPham.Status.ToString(),
                    GhiChu = sanPham.GhiChu,
                    Mau = sanPham.Mau,
                    BoNho = sanPham.BoNho,
                    KheSim = sanPham.KheSim,
                    Ram = sanPham.Ram,
                    Gia = sanPham.Gia,
                    ConLai = sanPham.ConLai,
                    CreatedAt = sanPham.CreatedAt
                };

                return CreatedAtAction(nameof(GetSanPhamById), new { id = sanPham.Id },
                    ApiResponse<SanPhamDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<SanPhamDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<SanPhamDTO>>> UpdateSanPham(string id, UpdateSanPhamDTO updateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var sanPham = await _context.SanPhams
                    .Include(s => s.NhanHieu)
                    .Include(s => s.PhanLoai)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (sanPham == null)
                    return NotFound(ApiResponse<SanPhamDTO>.Fail("Không tìm thấy sản phẩm"));

                // Kiểm tra nhãn hiệu và phân loại
                if (updateDto.NhanHieuId != sanPham.NhanHieuId)
                {
                    var nhanHieu = await _context.NhanHieus.FindAsync(updateDto.NhanHieuId);
                    if (nhanHieu == null)
                        return BadRequest(ApiResponse<SanPhamDTO>.Fail("Nhãn hiệu không tồn tại"));
                }

                if (updateDto.PhanLoaiId != sanPham.PhanLoaiId)
                {
                    var phanLoai = await _context.PhanLoais.FindAsync(updateDto.PhanLoaiId);
                    if (phanLoai == null)
                        return BadRequest(ApiResponse<SanPhamDTO>.Fail("Phân loại không tồn tại"));
                }

                sanPham.TenSanPham = updateDto.TenSanPham;
                sanPham.NhanHieuId = updateDto.NhanHieuId;
                sanPham.PhanLoaiId = updateDto.PhanLoaiId;
                sanPham.GhiChu = updateDto.GhiChu;
                sanPham.Mau = updateDto.Mau;
                sanPham.BoNho = updateDto.BoNho;
                sanPham.KheSim = updateDto.KheSim;
                sanPham.Ram = updateDto.Ram;
                sanPham.Gia = updateDto.Gia;
                sanPham.ConLai = updateDto.ConLai;
                sanPham.Status = updateDto.TrangThai;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var result = new SanPhamDTO
                {
                    Id = sanPham.Id,
                    TenSanPham = sanPham.TenSanPham,
                    NhanHieuId = sanPham.NhanHieuId,
                    TenNhanHieu = sanPham.NhanHieu.TenNhanHieu,
                    PhanLoaiId = sanPham.PhanLoaiId,
                    TenPhanLoai = sanPham.PhanLoai.TenPhanLoai,
                    TrangThai = sanPham.Status.ToString(),
                    GhiChu = sanPham.GhiChu,
                    Mau = sanPham.Mau,
                    BoNho = sanPham.BoNho,
                    KheSim = sanPham.KheSim,
                    Ram = sanPham.Ram,
                    Gia = sanPham.Gia,
                    ConLai = sanPham.ConLai,
                    CreatedAt = sanPham.CreatedAt,
                    UpdatedAt = sanPham.UpdatedAt
                };

                return Ok(ApiResponse<SanPhamDTO>.Succeed(result));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<SanPhamDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }
        [HttpPost("{id}/update-stock")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateStock(string id, [FromBody] int soLuong)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var sanPham = await _context.SanPhams.FindAsync(id);
                if (sanPham == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy sản phẩm"));

                sanPham.ConLai += soLuong;
                if (sanPham.ConLai < 0)
                    return BadRequest(ApiResponse<bool>.Fail("Số lượng không hợp lệ"));

                sanPham.Status = sanPham.ConLai > 0 ? ENums.TrangThaiSanPham.ConHang : ENums.TrangThaiSanPham.HetHang;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.Succeed(true, $"Đã cập nhật số lượng tồn kho: {sanPham.ConLai}"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost("{id}/update-status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateStatus(string id, [FromBody] string status)
        {
            try
            {
                var sanPham = await _context.SanPhams.FindAsync(id);
                if (sanPham == null)
                    return NotFound(ApiResponse<bool>.Fail("Không tìm thấy sản phẩm"));

                if (!Enum.TryParse<ENums.TrangThaiSanPham>(status, out var trangThai))
                    return BadRequest(ApiResponse<bool>.Fail("Trạng thái không hợp lệ"));

                sanPham.Status = trangThai;
                await _context.SaveChangesAsync();

                return Ok(ApiResponse<bool>.Succeed(true, "Cập nhật trạng thái thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("statistics")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<ThongKeSanPhamDTO>>> GetStatistics()
        {
            try
            {
                var thongKe = new ThongKeSanPhamDTO
                {
                    TongSanPham = await _context.SanPhams.CountAsync(),
                    SanPhamDangBan = await _context.SanPhams.CountAsync(s => s.Status == ENums.TrangThaiSanPham.ConHang),
                    SanPhamHetHang = await _context.SanPhams.CountAsync(s => s.Status == ENums.TrangThaiSanPham.HetHang),
                    SanPhamNgungBan = await _context.SanPhams.CountAsync(s => s.Status == ENums.TrangThaiSanPham.NgungCungCap),
                    TongGiaTriTonKho = await _context.SanPhams.SumAsync(s => s.Gia * s.ConLai),

                    ThongKeTheoNhanHieu = await _context.NhanHieus
                        .Include(nh => nh.SanPhams)
                        .ToDictionaryAsync(
                            nh => nh.TenNhanHieu,
                            nh => nh.SanPhams.Count
                        ),

                    ThongKeTheoPhanLoai = await _context.PhanLoais
                        .Include(pl => pl.SanPhams)
                        .ToDictionaryAsync(
                            pl => pl.TenPhanLoai,
                            pl => pl.SanPhams.Count
                        ),

                    Top10BanChay = await _context.SanPhams
                        .Include(s => s.DonHangChiTiets)
                            .ThenInclude(ct => ct.DonHang)
                        .Where(s => s.DonHangChiTiets.Any(ct =>
                            ct.DonHang.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh))
                        .Select(s => new SanPhamBanChayDTO
                        {
                            Id = s.Id,
                            TenSanPham = s.TenSanPham,
                            SoLuongBan = s.DonHangChiTiets
                                .Count(ct => ct.DonHang.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh),
                            DoanhThu = s.DonHangChiTiets
                                .Where(ct => ct.DonHang.TrangThai == ENums.TrangThaiDonHang.DaHoanThanh)
                                .Sum(ct => ct.GiaTri * ct.SoLuong)
                        })
                        .OrderByDescending(s => s.SoLuongBan)
                        .Take(10)
                        .ToListAsync(),

                    Top10TonKho = await _context.SanPhams
                        .Where(s => s.ConLai > 0)
                        .Select(s => new SanPhamTonKhoDTO
                        {
                            Id = s.Id,
                            TenSanPham = s.TenSanPham,
                            SoLuongTon = s.ConLai,
                            GiaTriTon = s.Gia * s.ConLai,
                            SoNgayTon = EF.Functions.DateDiffDay(s.UpdatedAt ?? s.CreatedAt, DateTime.UtcNow)
                        })
                        .OrderByDescending(s => s.GiaTriTon)
                        .Take(10)
                        .ToListAsync()
                };

                return Ok(ApiResponse<ThongKeSanPhamDTO>.Succeed(thongKe));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ThongKeSanPhamDTO>.Fail($"Lỗi server: {ex.Message}"));
            }
        }

        private async Task<string> GenerateProductId(string brandName)
        {
            // Format: [Brand Prefix]-[Year][Month]-[Sequential Number]
            // Example: IP-2401-001 (iPhone product created in January 2024)
            string prefix = GetBrandPrefix(brandName);
            string yearMonth = DateTime.Now.ToString("yyMM");

            var lastProduct = await _context.SanPhams
                .Where(s => s.Id.StartsWith($"{prefix}-{yearMonth}"))
                .OrderByDescending(s => s.Id)
                .FirstOrDefaultAsync();

            int sequence = 1;
            if (lastProduct != null)
            {
                string lastSequence = lastProduct.Id.Split('-').Last();
                sequence = int.Parse(lastSequence) + 1;
            }

            return $"{prefix}-{yearMonth}-{sequence:D3}";
        }

        private string GetBrandPrefix(string brandName)
        {
            return brandName.ToUpper() switch
            {
                "APPLE" => "IP",
                "SAMSUNG" => "SS",
                "XIAOMI" => "MI",
                "OPPO" => "OP",
                "VIVO" => "VV",
                "REALME" => "RM",
                "NOKIA" => "NK",
                _ => "OT" // Others
            };
        }
    }
}