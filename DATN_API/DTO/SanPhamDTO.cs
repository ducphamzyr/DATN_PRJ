using System.ComponentModel.DataAnnotations;
using static DATN_API.Models.ENums;

namespace DATN_API.DTOs
{
    public class SanPhamDTO
    {
        public string Id { get; set; }
        public string TenSanPham { get; set; }
        public int NhanHieuId { get; set; }
        public string TenNhanHieu { get; set; }
        public int PhanLoaiId { get; set; }
        public string TenPhanLoai { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string Mau { get; set; }
        public string BoNho { get; set; }
        public string KheSim { get; set; }
        public string Ram { get; set; }
        public decimal Gia { get; set; }
        public int ConLai { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class SanPhamDetailDTO
    {
        public string Id { get; set; }
        public string TenSanPham { get; set; }
        public NhanHieuDTO NhanHieu { get; set; }
        public PhanLoaiDTO PhanLoai { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string Mau { get; set; }
        public string BoNho { get; set; }
        public string KheSim { get; set; }
        public string Ram { get; set; }
        public decimal Gia { get; set; }
        public int ConLai { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Thống kê
        public int TongDaBan { get; set; }
        public int DangTrongGioHang { get; set; }
        public decimal DoanhThu { get; set; }
    }

    public class CreateSanPhamDTO
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(255)]
        public string TenSanPham { get; set; }

        [Required(ErrorMessage = "Nhãn hiệu không được để trống")]
        public int NhanHieuId { get; set; }

        [Required(ErrorMessage = "Phân loại không được để trống")]
        public int PhanLoaiId { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Màu sắc không được để trống")]
        [StringLength(50)]
        public string Mau { get; set; }

        [Required(ErrorMessage = "Bộ nhớ không được để trống")]
        [StringLength(50)]
        public string BoNho { get; set; }

        [Required(ErrorMessage = "Thông tin khe sim không được để trống")]
        [StringLength(50)]
        public string KheSim { get; set; }

        [Required(ErrorMessage = "RAM không được để trống")]
        [StringLength(50)]
        public string Ram { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0")]
        public int ConLai { get; set; }
    }

    public class UpdateSanPhamDTO
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(255)]
        public string TenSanPham { get; set; }

        [Required]
        public int NhanHieuId { get; set; }

        [Required]
        public int PhanLoaiId { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        [Required]
        [StringLength(50)]
        public string Mau { get; set; }

        [Required]
        [StringLength(50)]
        public string BoNho { get; set; }

        [Required]
        [StringLength(50)]
        public string KheSim { get; set; }

        [Required]
        [StringLength(50)]
        public string Ram { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Gia { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ConLai { get; set; }

        [Required]
        public TrangThaiSanPham TrangThai { get; set; }
    }

    public class ThongKeSanPhamDTO
    {
        public int TongSanPham { get; set; }
        public int SanPhamDangBan { get; set; }
        public int SanPhamHetHang { get; set; }
        public int SanPhamNgungBan { get; set; }
        public decimal TongGiaTriTonKho { get; set; }
        public Dictionary<string, int> ThongKeTheoNhanHieu { get; set; }
        public Dictionary<string, int> ThongKeTheoPhanLoai { get; set; }
        public List<SanPhamBanChayDTO> Top10BanChay { get; set; }
        public List<SanPhamTonKhoDTO> Top10TonKho { get; set; }
    }

    public class SanPhamBanChayDTO
    {
        public string Id { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuongBan { get; set; }
        public decimal DoanhThu { get; set; }
    }

    public class SanPhamTonKhoDTO
    {
        public string Id { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuongTon { get; set; }
        public decimal GiaTriTon { get; set; }
        public int SoNgayTon { get; set; }
    }
}