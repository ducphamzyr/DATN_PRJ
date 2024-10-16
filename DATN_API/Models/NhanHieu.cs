using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class NhanHieu
    {
        [Key]
        public long NhanHieuID { get; set; }

        [Required(ErrorMessage = "Tên nhãn hiệu là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên nhãn hiệu không được dài quá 100 ký tự")]
        public string TenNhanHieu { get; set; }

        [Required(ErrorMessage = "Xuất xứ là bắt buộc")]
        [StringLength(100, ErrorMessage = "Xuất xứ không được dài quá 100 ký tự")]
        public string XuatXu { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
