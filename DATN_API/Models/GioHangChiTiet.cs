using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN_API.Models
{
    public class GioHangChiTiet
    {
        [Key]
        public int GioHangChiTietID { get; set; }

        [Required(ErrorMessage = "Giỏ hàng không được để trống")]
        public int GioHangID { get; set; }

        [ForeignKey("GioHangID")]
        public GioHang GioHang { get; set; }

        [Required(ErrorMessage = "Sản phẩm không được để trống")]
        public int SanPhamID { get; set; }

        [ForeignKey("SanPhamID")]
        public SanPham SanPham { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Ghi chú không được để trống")]
        public string GhiChu { get; set; }
    }
}