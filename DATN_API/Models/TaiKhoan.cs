using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class TaiKhoan
    {
        [Key]
        public long TaiKhoanID { get; set; }

        [Required(ErrorMessage = "Mã khách hàng là bắt buộc")]
        public long KhachHangID { get; set; }

        [Required(ErrorMessage = "Mã giỏ hàng là bắt buộc")]
        public long GioHangID { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được dài quá 50 ký tự")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string MatKhauHash { get; set; }

        [Required(ErrorMessage = "Mã phân quyền là bắt buộc")]
        public long PhanQuyenID { get; set; }

        [Required(ErrorMessage = "Ngày tạo là bắt buộc")]
        public DateTime CreateAt { get; set; }

        public virtual KhachHang KhachHang { get; set; }
        public virtual GioHang GioHang { get; set; }
        public virtual PhanQuyen PhanQuyen { get; set; }
    }
}
