using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN_API.Models
{
    public class SanPhamChiTiet
    {
        [Key]
        public int SanPhamChiTietID { get; set; }

        [Required(ErrorMessage = "Sản phẩm không được để trống")]
        public int SanPhamID { get; set; }

        [ForeignKey("SanPhamID")]
        public SanPham SanPham { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string TenSanPham { get; set; }

        [Required(ErrorMessage = "Màu không được để trống")]
        [StringLength(50, ErrorMessage = "Màu không được vượt quá 50 ký tự")]
        public string Mau { get; set; }

        [Required(ErrorMessage = "Bộ nhớ không được để trống")]
        public string BoNho { get; set; }

        [Required(ErrorMessage = "RAM không được để trống")]
        public string RAM { get; set; }

        [Required(ErrorMessage = "Kích sim không được để trống")]
        public string KichSim { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage = "Còn lại không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng còn lại phải lớn hơn hoặc bằng 0")]
        public int ConLai { get; set; }
    }
}