using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class PhuongThucThanhToan : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenThanhToan { get; set; }

        [StringLength(100)]
        public string NhaCungCap { get; set; }

        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
