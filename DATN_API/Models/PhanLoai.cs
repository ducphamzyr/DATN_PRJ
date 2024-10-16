using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class PhanLoai
    {
        [Key]
        public long PhanLoaiID { get; set; }

        [Required(ErrorMessage = "Tên phân loại là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên phân loại không được dài quá 100 ký tự")]
        public string TenPhanLoai { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
