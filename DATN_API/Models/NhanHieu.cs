using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class NhanHieu : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenNhanHieu { get; set; }

        [Required]
        [StringLength(100)]
        public string XuatXu { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
