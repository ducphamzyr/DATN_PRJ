using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN_API.Models
{
    public class MaGiamGia
    {
        [Key]
        public int MaGiamGiaID { get; set; }

        [Required(ErrorMessage = "Tên mã không được để trống")]
        [StringLength(50, ErrorMessage = "Tên mã không được vượt quá 50 ký tự")]
        public string TenMa { get; set; }

        [Required(ErrorMessage = "Mã áp dụng không được để trống")]
        [StringLength(20, ErrorMessage = "Mã áp dụng không được vượt quá 20 ký tự")]
        public string MaApDung { get; set; }

        [Required(ErrorMessage = "Loại mã không được để trống")]
        [StringLength(50, ErrorMessage = "Loại mã không được vượt quá 50 ký tự")]
        public string LoaiMa { get; set; }

        [Required(ErrorMessage = "Danh sách tài khoản không được để trống")]
        public string DanhSachTaiKhoan { get; set; }

        [Required(ErrorMessage = "Số tiền tối thiểu không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền tối thiểu phải lớn hơn hoặc bằng 0")]
        public decimal SoTienToiThieu { get; set; }

        [Required(ErrorMessage = "Số tiền tối đa không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền tối đa phải lớn hơn hoặc bằng 0")]
        public decimal SoTienToiDa { get; set; }

        [Required(ErrorMessage = "Phần trăm giảm không được để trống")]
        [Range(0, 100, ErrorMessage = "Phần trăm giảm phải từ 0 đến 100")]
        public int PhanTramGiam { get; set; }

        [Required(ErrorMessage = "Ngày tạo không được để trống")]
        public DateTime NgayTao { get; set; }

        [Required(ErrorMessage = "Ngày hết hạn không được để trống")]
        public DateTime NgayHetHan { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; }

        public int? SanPhamID { get; set; }

        [ForeignKey("SanPhamID")]
        public SanPham SanPham { get; set; }
    }
}