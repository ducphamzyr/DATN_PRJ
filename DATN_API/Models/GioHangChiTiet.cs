using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class GioHangChiTiet : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("GioHang")]
        public int GioHangId { get; set; }

        [Required]
        [ForeignKey("SanPham")]
        public string SanPhamId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SoLuong { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public virtual GioHang GioHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
