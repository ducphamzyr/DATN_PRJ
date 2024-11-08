using System.ComponentModel.DataAnnotations;

namespace DATN_API.DTOs
{
    public class NhanHieuDTO
    {
        public int Id { get; set; }
        public string TenNhanHieu { get; set; }
        public string XuatXu { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateNhanHieuDTO
    {
        [Required(ErrorMessage = "Tên nhãn hiệu không được để trống")]
        [StringLength(100, ErrorMessage = "Tên nhãn hiệu không được vượt quá 100 ký tự")]
        public string TenNhanHieu { get; set; }

        [Required(ErrorMessage = "Xuất xứ không được để trống")]
        [StringLength(100, ErrorMessage = "Xuất xứ không được vượt quá 100 ký tự")]
        public string XuatXu { get; set; }
    }

    public class UpdateNhanHieuDTO
    {
        [Required(ErrorMessage = "Tên nhãn hiệu không được để trống")]
        [StringLength(100, ErrorMessage = "Tên nhãn hiệu không được vượt quá 100 ký tự")]
        public string TenNhanHieu { get; set; }

        [Required(ErrorMessage = "Xuất xứ không được để trống")]
        [StringLength(100, ErrorMessage = "Xuất xứ không được vượt quá 100 ký tự")]
        public string XuatXu { get; set; }
    }
}