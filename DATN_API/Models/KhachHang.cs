using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class KhachHang
    {
        [Key]
        public int KhachHangID { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không được vượt quá 100 ký tự")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Ngày tạo không được để trống")]
        public DateTime NgayTao { get; set; }

        public ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}