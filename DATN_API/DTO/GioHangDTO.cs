using System.ComponentModel.DataAnnotations;

namespace DATN_API.DTOs
{
    public class GioHangDTO
    {
        public int Id { get; set; }
        public int TaiKhoanId { get; set; }
        public string TenKhachHang { get; set; }
        public int TongSanPham { get; set; }
        public decimal TongTien { get; set; }
        public List<GioHangChiTietDTO> ChiTietGioHang { get; set; }
    }

    public class GioHangChiTietDTO
    {
        public int Id { get; set; }
        public int GioHangId { get; set; }
        public string SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public string HinhAnh { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }
        // Thông tin thêm của sản phẩm
        public string Mau { get; set; }
        public string BoNho { get; set; }
        public string Ram { get; set; }
        public int SoLuongTonKho { get; set; }
    }

    public class ThemVaoGioDTO
    {
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public string SanPhamId { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        public string GhiChu { get; set; }
    }

    public class CapNhatGioHangDTO
    {
        [Required(ErrorMessage = "ID chi tiết giỏ hàng không được để trống")]
        public int GioHangChiTietId { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        public string GhiChu { get; set; }
    }

    public class GioHangSummaryDTO
    {
        public int TongSanPham { get; set; }
        public decimal TongTien { get; set; }
        // Có thể mở rộng thêm thông tin khuyến mãi
        public List<KhuyenMaiApDungDTO> KhuyenMaiKhaDung { get; set; }
    }

    public class KhuyenMaiApDungDTO
    {
        public int Id { get; set; }
        public string TenKhuyenMai { get; set; }
        public string MaKhuyenMai { get; set; }
        public string LoaiGiamGia { get; set; }
        public decimal GiaTriGiam { get; set; }
        public DateTime HanSuDung { get; set; }
    }

    public class ChuyenGioHangDTO
    {
        [Required(ErrorMessage = "Tài khoản đích không được để trống")]
        public int TaiKhoanDichId { get; set; }

        [Required(ErrorMessage = "Danh sách sản phẩm không được để trống")]
        public List<int> DanhSachChiTietId { get; set; }

        public string GhiChu { get; set; }
    }

    public class KiemTraGioHangDTO
    {
        public bool SanPhamHopLe { get; set; }
        public List<string> SanPhamKhongHopLe { get; set; }
        public List<string> SanPhamHetHang { get; set; }
        public bool CoTheThanhToan { get; set; }
        public string LyDo { get; set; }
    }

    public class ThongKeGioHangDTO
    {
        public int TongGioHang { get; set; }
        public int GioHangCoSanPham { get; set; }
        public int GioHangTrong { get; set; }
        public decimal TongGiaTriGioHang { get; set; }
        public decimal TrungBinhGiaTriGioHang { get; set; }
        public int SanPhamPhoBien { get; set; }
        public List<SanPhamGioHangThongKeDTO> Top10SanPham { get; set; }
    }

    public class SanPhamGioHangThongKeDTO
    {
        public string SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public int SoLanThem { get; set; }
        public int TongSoLuong { get; set; }
        public decimal TongGiaTri { get; set; }
    }
}