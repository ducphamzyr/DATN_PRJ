using System.ComponentModel.DataAnnotations;

namespace DATN_MVC.Models
{
    public class PhanLoaiDTO
    {
        public int Id { get; set; }
        public string TenPhanLoai { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Properties để tương thích với code
        public DateTime NgayTao => CreatedAt;
        public DateTime? NgayCapNhat => UpdatedAt;
    }

    public class CreatePhanLoaiDTO
    {
        [Required(ErrorMessage = "Tên phân loại không được để trống")]
        [StringLength(100, ErrorMessage = "Tên phân loại không được vượt quá 100 ký tự")]
        public string TenPhanLoai { get; set; }
    }

    public class UpdatePhanLoaiDTO
    {
        [Required(ErrorMessage = "Tên phân loại không được để trống")]
        [StringLength(100, ErrorMessage = "Tên phân loại không được vượt quá 100 ký tự")]
        public string TenPhanLoai { get; set; }
    }
}