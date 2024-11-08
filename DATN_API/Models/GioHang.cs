using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class GioHang : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("TaiKhoan")]
        public int TaiKhoanId { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual ICollection<GioHangChiTiet> GioHangChiTiets { get; set; }
    }
}
