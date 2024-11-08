using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class PhanQuyen : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenPhanQuyen { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
