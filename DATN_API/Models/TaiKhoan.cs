using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN_API.Models
{
    public class TaiKhoan
    {
        [Key]
        public int TaiKhoanID { get; set; }

        public int? KhachHangID { get; set; }

        [ForeignKey("KhachHangID")]
        public KhachHang KhachHang { get; set; }

        public int? GioHangID { get; set; }

        [ForeignKey("GioHangID")]
        public GioHang GioHang { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(100, ErrorMessage = "Tên đăng nhập không được vượt quá 100 ký tự")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(255, ErrorMessage = "Mật khẩu không được vượt quá 255 ký tự")]
        public string MatKhauHash { get; set; }

        [Required(ErrorMessage = "Phân quyền không được để trống")]
        public int RoleID { get; set; }

        [ForeignKey("RoleID")]
        public PhanQuyen PhanQuyen { get; set; }

        [Required(ErrorMessage = "Ngày tạo không được để trống")]
        public DateTime CreateAt { get; set; }

        public DateTime? GioHangID_1 { get; set; }
    }
}