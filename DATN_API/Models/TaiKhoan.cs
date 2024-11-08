using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DATN_API.Models.ENums;

namespace DATN_API.Models
{
    public class TaiKhoan : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("KhachHang")]
        public int KhachHangId { get; set; }

        [Required]
        [ForeignKey("PhanQuyen")]
        public int PhanQuyenId { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        public string MatKhauHash { get; set; }

        [Required]
        public TrangThaiTaiKhoan Status { get; set; }

        public virtual KhachHang KhachHang { get; set; }
        public virtual PhanQuyen PhanQuyen { get; set; }
        public virtual GioHang GioHang { get; set; }
        public virtual ICollection<DonHang> DonHangs { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}
