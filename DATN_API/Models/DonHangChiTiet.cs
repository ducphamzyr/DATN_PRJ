using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN_API.Models
{
    public class DonHangChiTiet
    {
        [Key]
        public int DonHangChiTietID { get; set; }

        [Required(ErrorMessage = "Sản phẩm không được để trống")]
        public int SanPhamID { get; set; }

        [ForeignKey("SanPhamID")]
        public SanPham SanPham { get; set; }

        [Required(ErrorMessage = "Đơn hàng không được để trống")]
        public int DonHangID { get; set; }

        [ForeignKey("DonHangID")]
        public DonHang DonHang { get; set; }

        public int? MaGiamGiaID { get; set; }

        [ForeignKey("MaGiamGiaID")]
        public MaGiamGia MaGiamGia { get; set; }

        [Required(ErrorMessage = "IMEI không được để trống")]
        [StringLength(50, ErrorMessage = "IMEI không được vượt quá 50 ký tự")]
        public string IMEI { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Giá trị không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public decimal GiaTri { get; set; }

        [Required(ErrorMessage = "Ghi chú không được để trống")]
        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string GhiChu { get; set; }
    }
}