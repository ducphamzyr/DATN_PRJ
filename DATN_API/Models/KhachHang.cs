using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class KhachHang : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenKhachHang { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChi { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
