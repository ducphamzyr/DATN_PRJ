using System.ComponentModel.DataAnnotations;
using static DATN_API.Models.ENums;

namespace DATN_API.Models
{
    public class MaGiamGia : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenMa { get; set; }

        [Required]
        [StringLength(50)]
        public string MaApDung { get; set; }

        [Required]
        public LoaiGiamGia LoaiMa { get; set; }

        public string DanhSachTaiKhoan { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal SoTienToiThieu { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal SoTienToiDa { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal PhanTramGiam { get; set; }

        [Required]
        public DateTime NgayHetHan { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
    }
}
