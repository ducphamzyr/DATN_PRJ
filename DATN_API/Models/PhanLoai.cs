using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class PhanLoai : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenPhanLoai { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
