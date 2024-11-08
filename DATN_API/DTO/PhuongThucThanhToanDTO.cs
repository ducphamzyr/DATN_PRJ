using System.ComponentModel.DataAnnotations;

namespace DATN_API.DTOs
{
    public class PhuongThucThanhToanDTO
    {
        public int Id { get; set; }
        public string TenThanhToan { get; set; }
        public string NhaCungCap { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreatePhuongThucThanhToanDTO
    {
        [Required(ErrorMessage = "Tên phương thức thanh toán không được để trống")]
        [StringLength(100, ErrorMessage = "Tên phương thức thanh toán không được vượt quá 100 ký tự")]
        public string TenThanhToan { get; set; }

        [StringLength(100, ErrorMessage = "Tên nhà cung cấp không được vượt quá 100 ký tự")]
        public string NhaCungCap { get; set; }
    }

    public class UpdatePhuongThucThanhToanDTO
    {
        [Required(ErrorMessage = "Tên phương thức thanh toán không được để trống")]
        [StringLength(100, ErrorMessage = "Tên phương thức thanh toán không được vượt quá 100 ký tự")]
        public string TenThanhToan { get; set; }

        [StringLength(100, ErrorMessage = "Tên nhà cung cấp không được vượt quá 100 ký tự")]
        public string NhaCungCap { get; set; }
    }
}