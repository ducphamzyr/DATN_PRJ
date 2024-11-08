using System.ComponentModel.DataAnnotations;

namespace DATN_API.DTOs
{
    public class KhachHangDTO
    {
        public int Id { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string TrangThaiTaiKhoan { get; set; }
        public int TongDonHang { get; set; }
        public decimal TongTienMua { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class KhachHangDetailDTO
    {
        public int Id { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Thông tin tài khoản
        public TaiKhoanKhachHangDTO TaiKhoan { get; set; }

        // Thống kê mua hàng
        public int TongDonHang { get; set; }
        public int DonHangThanhCong { get; set; }
        public int DonHangHuy { get; set; }
        public decimal TongTienMua { get; set; }
        public List<DonHangKhachHangDTO> LichSuMuaHang { get; set; }

        // Giỏ hàng hiện tại
        public GioHangKhachHangDTO GioHang { get; set; }
    }

    public class CreateKhachHangDTO
    {
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không được vượt quá 100 ký tự")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [StringLength(255, ErrorMessage = "Địa chỉ không được vượt quá 255 ký tự")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
        public string SoDienThoai { get; set; }
    }

    public class UpdateKhachHangDTO
    {
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không được vượt quá 100 ký tự")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [StringLength(255, ErrorMessage = "Địa chỉ không được vượt quá 255 ký tự")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
        public string SoDienThoai { get; set; }
    }

    public class KhachHangThongKeDTO
    {
        public int TongKhachHang { get; set; }
        public int KhachHangMoi { get; set; }
        public int KhachHangHoatDong { get; set; }
        public decimal DoanhThu { get; set; }
        public List<KhachHangDTO> Top10KhachHang { get; set; }
    }

    public class TaiKhoanKhachHangDTO
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; }
        public string TenPhanQuyen { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
    }

    public class DonHangKhachHangDTO
    {
        public int Id { get; set; }
        public string TrangThai { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayDat { get; set; }
    }

    public class GioHangKhachHangDTO
    {
        public int SoLuongSanPham { get; set; }
        public decimal TongTien { get; set; }
        public List<SanPhamGioHangDTO> ChiTiet { get; set; }
    }

    public class SanPhamGioHangDTO
    {
        public string SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}