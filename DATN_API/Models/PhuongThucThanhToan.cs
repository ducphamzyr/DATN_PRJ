using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class PhuongThucThanhToan
    {
        [Key]
        public long PhuongThucThanhToanID { get; set; }

        [Required(ErrorMessage = "Tên thanh toán là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên thanh toán không được dài quá 100 ký tự")]
        public string TenThanhToan { get; set; }

        [StringLength(100, ErrorMessage = "Nhà cung cấp không được dài quá 100 ký tự")]
        public string NhaCungCap { get; set; }

        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
