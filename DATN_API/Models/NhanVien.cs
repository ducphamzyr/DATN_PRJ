using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class NhanVien
    {
        [Key]
        public long NhanVienID { get; set; }

        public long TaiKhoanID { get; set; }

        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên nhân viên không được dài quá 100 ký tự")]
        public string TenNhanVien { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Địa chỉ thường trú là bắt buộc")]
        public string DiaChiThuongTru { get; set; }

        [Required(ErrorMessage = "Quê quán là bắt buộc")]
        public string QueQuan { get; set; }

        [Required(ErrorMessage = "Số CCCD là bắt buộc")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Số CCCD phải có đúng 12 ký tự")]
        public string SoCCCD { get; set; }

        [Required(ErrorMessage = "Lương hiện tại là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Lương hiện tại phải lớn hơn hoặc bằng 0")]
        public decimal LuongHienTai { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public int TrangThai { get; set; }

        [Required(ErrorMessage = "Ngày nhận việc là bắt buộc")]
        public DateTime NgayNhanViec { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
