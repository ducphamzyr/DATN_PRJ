using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class PhuongThucThanhToan
    {
        [Key]
        public int PhuongThucThanhToanID { get; set; }

        [Required(ErrorMessage = "Tên thanh toán không được để trống")]
        [StringLength(100, ErrorMessage = "Tên thanh toán không được vượt quá 100 ký tự")]
        public string TenThanhToan { get; set; }

        [Required(ErrorMessage = "Nhà cung cấp không được để trống")]
        [StringLength(100, ErrorMessage = "Nhà cung cấp không được vượt quá 100 ký tự")]
        public string NhaCungCap { get; set; }

        public ICollection<DonHang> DonHangs { get; set; }
    }
}