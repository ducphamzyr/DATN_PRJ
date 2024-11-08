using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DATN_API.Models.ENums;

namespace DATN_API.Models
{
    public class DonHang : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("TaiKhoan")]
        public int TaiKhoanId { get; set; }

        [ForeignKey("NhanVien")]
        public int? NhanVienId { get; set; }

        [Required]
        [ForeignKey("PhuongThucThanhToan")]
        public int PhuongThucThanhToanId { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChiGiaoHang { get; set; }

        [Required]
        public TrangThaiDonHang TrangThai { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual NhanVien NhanVien { get; set; }
        public virtual PhuongThucThanhToan PhuongThucThanhToan { get; set; }
        public virtual ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
    }
}
