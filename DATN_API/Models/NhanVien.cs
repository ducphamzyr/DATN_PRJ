using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN_API.Models
{
    public class NhanVien
    {
        [Key]
        public int NhanVienID { get; set; }

        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public int TaiKhoanID { get; set; }

        [ForeignKey("TaiKhoanID")]
        public TaiKhoan TaiKhoan { get; set; }

        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        [StringLength(100, ErrorMessage = "Tên nhân viên không được vượt quá 100 ký tự")]
        public string TenNhanVien { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Địa chỉ thường trú không được để trống")]
        public string DiaChiThuongTru { get; set; }

        [Required(ErrorMessage = "Quê quán không được để trống")]
        public string QueQuan { get; set; }

        [Required(ErrorMessage = "Số CCCD không được để trống")]
        [StringLength(20, ErrorMessage = "Số CCCD không được vượt quá 20 ký tự")]
        public string SoCCCD { get; set; }

        [Required(ErrorMessage = "Lương hiện tại không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Lương hiện tại phải lớn hơn hoặc bằng 0")]
        public decimal LuongHienTai { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; }

        [Required(ErrorMessage = "Ngày nhận việc không được để trống")]
        public DateTime NgayNhanViec { get; set; }

        public ICollection<DonHang> DonHangs { get; set; }
    }
}