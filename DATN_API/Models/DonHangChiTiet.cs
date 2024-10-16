using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class DonHangChiTiet
    {
        [Key]
        public long DonHangChiTietID { get; set; }

        [Required(ErrorMessage = "Mã sản phẩm là bắt buộc")]
        public long SanPhamID { get; set; }

        [Required(ErrorMessage = "Mã đơn hàng là bắt buộc")]
        public long DonHangID { get; set; }

        public long? MaGiamGiaID { get; set; }

        [Required(ErrorMessage = "IMEI là bắt buộc")]
        [StringLength(50, ErrorMessage = "IMEI không được dài quá 50 ký tự")]
        public string IMEI { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Giá trị là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public decimal GiaTri { get; set; }

        public string GhiChu { get; set; }

        public virtual SanPham SanPham { get; set; }
        public virtual DonHang DonHang { get; set; }
        public virtual MaGiamGia MaGiamGia { get; set; }
    }
}
