using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class ThongBao : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("TaiKhoan")]
        public int TaiKhoanId { get; set; }

        [Required]
        [StringLength(255)]
        public string TieuDe { get; set; }

        [Required]
        public string NoiDung { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        [Required]
        public DateTime HetHan { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
