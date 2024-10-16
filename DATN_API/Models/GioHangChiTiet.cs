using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class GioHangChiTiet
    {
        [Key]
        public long GioHangChiTietID { get; set; }

        [Required(ErrorMessage = "Mã giỏ hàng là bắt buộc")]
        public long GioHangID { get; set; }

        [Required(ErrorMessage = "Mã sản phẩm là bắt buộc")]
        public long SanPhamID { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public long SoLuong { get; set; }

        public string GhiChu { get; set; }

        public virtual GioHang GioHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
