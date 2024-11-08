using System.ComponentModel.DataAnnotations;

namespace DATN_API.DTOs
{
    public class MaGiamGiaDTO
    {
        public int Id { get; set; }
        public string TenMa { get; set; }
        public string MaApDung { get; set; }
        public string LoaiMa { get; set; }
        public string DanhSachTaiKhoan { get; set; }
        public decimal SoTienToiThieu { get; set; }
        public decimal SoTienToiDa { get; set; }
        public decimal PhanTramGiam { get; set; }
        public DateTime NgayHetHan { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateMaGiamGiaDTO
    {
        [Required(ErrorMessage = "Tên mã không được để trống")]
        [StringLength(100, ErrorMessage = "Tên mã không được vượt quá 100 ký tự")]
        public string TenMa { get; set; }

        [Required(ErrorMessage = "Mã áp dụng không được để trống")]
        [StringLength(20, ErrorMessage = "Mã áp dụng không được vượt quá 20 ký tự")]
        public string MaApDung { get; set; }

        [Required(ErrorMessage = "Loại mã không được để trống")]
        public int LoaiMa { get; set; }

        public string DanhSachTaiKhoan { get; set; }

        [Required(ErrorMessage = "Số tiền tối thiểu không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền tối thiểu phải lớn hơn hoặc bằng 0")]
        public decimal SoTienToiThieu { get; set; }

        [Required(ErrorMessage = "Số tiền tối đa không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền tối đa phải lớn hơn hoặc bằng 0")]
        public decimal SoTienToiDa { get; set; }

        [Required(ErrorMessage = "Phần trăm giảm không được để trống")]
        [Range(0, 100, ErrorMessage = "Phần trăm giảm phải từ 0 đến 100")]
        public decimal PhanTramGiam { get; set; }

        [Required(ErrorMessage = "Ngày hết hạn không được để trống")]
        public DateTime NgayHetHan { get; set; }
    }

    public class UpdateMaGiamGiaDTO
    {
        [Required(ErrorMessage = "Tên mã không được để trống")]
        [StringLength(100, ErrorMessage = "Tên mã không được vượt quá 100 ký tự")]
        public string TenMa { get; set; }

        [Required(ErrorMessage = "Mã áp dụng không được để trống")]
        [StringLength(20, ErrorMessage = "Mã áp dụng không được vượt quá 20 ký tự")]
        public string MaApDung { get; set; }

        [Required(ErrorMessage = "Loại mã không được để trống")]
        public int LoaiMa { get; set; }

        public string DanhSachTaiKhoan { get; set; }

        [Required(ErrorMessage = "Số tiền tối thiểu không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền tối thiểu phải lớn hơn hoặc bằng 0")]
        public decimal SoTienToiThieu { get; set; }

        [Required(ErrorMessage = "Số tiền tối đa không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền tối đa phải lớn hơn hoặc bằng 0")]
        public decimal SoTienToiDa { get; set; }

        [Required(ErrorMessage = "Phần trăm giảm không được để trống")]
        [Range(0, 100, ErrorMessage = "Phần trăm giảm phải từ 0 đến 100")]
        public decimal PhanTramGiam { get; set; }

        [Required(ErrorMessage = "Ngày hết hạn không được để trống")]
        public DateTime NgayHetHan { get; set; }
    }
}