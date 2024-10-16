using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class ThongBao
    {
        [Key]
        public long ThongBaoID { get; set; }

        [Required(ErrorMessage = "Mã tài khoản là bắt buộc")]
        public long TaiKhoanID { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        [StringLength(255, ErrorMessage = "Tiêu đề không được dài quá 255 ký tự")]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Nội dung là bắt buộc")]
        public string NoiDung { get; set; }

        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Ngày tạo là bắt buộc")]
        public DateTime NgayTao { get; set; }

        [Required(ErrorMessage = "Ngày hết hạn là bắt buộc")]
        public DateTime HetHan { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
