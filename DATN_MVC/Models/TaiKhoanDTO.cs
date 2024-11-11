using DATN_MVC.Models.DonHang;
using System.ComponentModel.DataAnnotations;

namespace DATN_MVC.Models.TaiKhoan
{
    namespace DATN_MVC.Models.TaiKhoan
    {
        // DTO cho danh sách và hiển thị cơ bản
        public class TaiKhoanDTO
        {
            public int Id { get; set; }
            public string TenDangNhap { get; set; }
            public string TrangThai { get; set; }

            // Thông tin khách hàng
            public string TenKhachHang { get; set; }
            public string Email { get; set; }
            public string SoDienThoai { get; set; }
            public string DiaChi { get; set; }

            // Thông tin phân quyền
            public string TenPhanQuyen { get; set; }

            // Thống kê cơ bản
            public int SoLuongDonHang { get; set; }
            public int SoSanPhamTrongGio { get; set; }
            public decimal TongTienMua { get; set; }

            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }

        // DTO cho chi tiết
        public class TaiKhoanChiTietDTO : TaiKhoanDTO
        {
            public GioHangDTO GioHang { get; set; }
            public List<DonHangDTO> DonHangs { get; set; }
            public ThongKeTaiKhoanDTO ThongKe { get; set; }
        }

        public class ThongKeTaiKhoanDTO
        {
            public int TongSoDonHang { get; set; }
            public int DonHangThanhCong { get; set; }
            public int DonHangHuy { get; set; }
            public decimal TongTienThangHienTai { get; set; }
            public decimal TongTienTatCa { get; set; }
        }

        public class CreateTaiKhoanDTO
        {
            [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
            public string TenDangNhap { get; set; }

            [Required(ErrorMessage = "Mật khẩu không được để trống")]
            public string MatKhau { get; set; }

            [Required(ErrorMessage = "Phân quyền không được để trống")]
            public int PhanQuyenId { get; set; }

            // Thông tin khách hàng
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

        public class UpdateTaiKhoanDTO
        {
            public string TenKhachHang { get; set; }
            public string Email { get; set; }
            public string SoDienThoai { get; set; }
            public string DiaChi { get; set; }
            public int PhanQuyenId { get; set; }
            public string TrangThai { get; set; }
        }
    }
}
