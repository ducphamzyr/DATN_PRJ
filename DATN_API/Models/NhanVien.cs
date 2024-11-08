using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DATN_API.Models.ENums;

namespace DATN_API.Models
{
    public class NhanVien : BaseEntity
    {
        public int Id { get; set; }

        [ForeignKey("TaiKhoan")]
        public int? TaiKhoanId { get; set; }

        [Required]
        [StringLength(100)]
        public string TenNhanVien { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChiThuongTru { get; set; }

        [Required]
        [StringLength(255)]
        public string QueQuan { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 12)]
        public string SoCCCD { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal LuongHienTai { get; set; }

        [Required]
        public TrangThaiNhanVien TrangThai { get; set; }

        [Required]
        public DateTime NgayNhanViec { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
