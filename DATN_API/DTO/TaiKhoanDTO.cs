using DATN_API.DTOs.PhanQuyen;
using System.ComponentModel.DataAnnotations;
using DATN_API.DTOs;

namespace DATN_API.DTOs.TaiKhoan
{
    public class TaiKhoanDTO
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; }
        public string TenKhachHang { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string TenPhanQuyen { get; set; }
        public string TrangThai { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateTaiKhoanDTO
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Phân quyền không được để trống")]
        public int PhanQuyenId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }
    }

    public class TaiKhoanDetailDTO
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; }
        public string TrangThai { get; set; }
        public PhanQuyenDTO PhanQuyen { get; set; }
        public KhachHangBaseDTO ThongTinKhachHang { get; set; }
        public List<DonHangBaseDTO> LichSuDonHang { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class KhachHangBaseDTO
    {
        public int Id { get; set; }
        public string TenKhachHang { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
    }

    public class DonHangBaseDTO
    {
        public int Id { get; set; }
        public string TrangThai { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class UpdateTrangThaiTaiKhoanDTO
    {
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; }
        public string LyDo { get; set; }
    }
}