using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class KhachHang
    {
        [Key]
        public long KhachHangID { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không được dài quá 100 ký tự")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Ngày tạo là bắt buộc")]
        public DateTime NgayTao { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
