using System.ComponentModel.DataAnnotations;

namespace DATN_MVC.Models.PhuongThucThanhToan
{
    public class PhuongThucThanhToanDTO
    {
        public int Id { get; set; }
        public string TenThanhToan { get; set; }
        public string NhaCungCap { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class CreatePhuongThucThanhToanDTO_MVC
    {
        [Required(ErrorMessage = "Tên phương thức thanh toán không được để trống")]
        [StringLength(100, ErrorMessage = "Tên phương thức thanh toán không được vượt quá 100 ký tự")]
        public string TenThanhToan { get; set; }

        [Required(ErrorMessage = "Nhà cung cấp không được để trống")]
        [StringLength(100, ErrorMessage = "Nhà cung cấp không được vượt quá 100 ký tự")]
        public string NhaCungCap { get; set; }
    }
    public class UpdatePhuongThucThanhToanDTO_MVC
    {
        [Required(ErrorMessage = "Tên phương thức thanh toán không được để trống")]
        [StringLength(100, ErrorMessage = "Tên phương thức thanh toán không được vượt quá 100 ký tự")]
        public string TenThanhToan { get; set; }
        [Required(ErrorMessage = "Nhà cung cấp không được để trống")]
        [StringLength(100, ErrorMessage = "Nhà cung cấp không được vượt quá 100 ký tự")]
        public string NhaCungCap { get; set; }
    }
}
