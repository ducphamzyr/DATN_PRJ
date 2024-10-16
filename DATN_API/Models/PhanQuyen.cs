using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class PhanQuyen
    {
        [Key]
        public long PhanQuyenID { get; set; }

        [Required(ErrorMessage = "Tên phân quyền là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên phân quyền không được dài quá 100 ký tự")]
        public string TenPhanQuyen { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } //
    }
}
