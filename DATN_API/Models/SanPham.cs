using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DATN_API.Models.ENums;

namespace DATN_API.Models
{
    public class SanPham : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Id { get; set; } // Custom ID format

        [Required]
        [StringLength(255)]
        public string TenSanPham { get; set; }

        [Required]
        [ForeignKey("NhanHieu")]
        public int NhanHieuId { get; set; }

        [Required]
        [ForeignKey("PhanLoai")]
        public int PhanLoaiId { get; set; }

        [Required]
        public TrangThaiSanPham Status { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        [Required]
        [StringLength(50)]
        public string Mau { get; set; }

        [Required]
        [StringLength(50)]
        public string BoNho { get; set; }

        [Required]
        [StringLength(50)]
        public string KheSim { get; set; }

        [Required]
        [StringLength(50)]
        public string Ram { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Gia { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ConLai { get; set; }

        public virtual NhanHieu NhanHieu { get; set; }
        public virtual PhanLoai PhanLoai { get; set; }
        public virtual ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
        public virtual ICollection<GioHangChiTiet> GioHangChiTiets { get; set; }
    }
}
