using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class DonHangChiTiet : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("DonHang")]
        public int DonHangId { get; set; }

        [Required]
        [ForeignKey("SanPham")]
        public string SanPhamId { get; set; }

        [ForeignKey("MaGiamGia")]
        public int? MaGiamGiaId { get; set; }

        [Required]
        [StringLength(50)]
        public string IMEI { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SoLuong { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal GiaTri { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public virtual DonHang DonHang { get; set; }
        public virtual SanPham SanPham { get; set; }
        public virtual MaGiamGia MaGiamGia { get; set; }
    }

}
