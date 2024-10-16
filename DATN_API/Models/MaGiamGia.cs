using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class MaGiamGia
    {
        [Key]
        public long MaGiamGiaID { get; set; }

        [Required(ErrorMessage = "Tên mã giảm giá là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên mã giảm giá không được dài quá 100 ký tự")]
        public string TenMa { get; set; }

        [Required(ErrorMessage = "Mã áp dụng là bắt buộc")]
        [StringLength(50, ErrorMessage = "Mã áp dụng không được dài quá 50 ký tự")]
        public string MaApDung { get; set; }

        [Required(ErrorMessage = "Loại mã là bắt buộc")]
        [StringLength(50, ErrorMessage = "Loại mã không được dài quá 50 ký tự")]
        public string LoaiMa { get; set; }

        public string DanhSachTaiKhoan { get; set; }

        [Required(ErrorMessage = "Số tiền tối thiểu là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền tối thiểu phải lớn hơn hoặc bằng 0")]
        public decimal SoTienToiThieu { get; set; }

        [Required(ErrorMessage = "Số tiền tối đa là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền tối đa phải lớn hơn hoặc bằng 0")]
        public decimal SoTienToiDa { get; set; }

        [Required(ErrorMessage = "Phần trăm giảm là bắt buộc")]
        [Range(0, 100, ErrorMessage = "Phần trăm giảm phải nằm trong khoảng từ 0 đến 100")]
        public int PhanTramGiam { get; set; }

        [Required(ErrorMessage = "Ngày tạo là bắt buộc")]
        public DateTime NgayTao { get; set; }

        [Required(ErrorMessage = "Ngày hết hạn là bắt buộc")]
        public DateTime NgayHetHan { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public int TrangThai { get; set; }

        public virtual ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
    }
}
