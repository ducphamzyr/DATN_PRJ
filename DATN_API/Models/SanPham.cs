using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class SanPham
    {
        [Key]
        public long SanPhamID { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(255, ErrorMessage = "Tên sản phẩm không được dài quá 255 ký tự")]
        public string TenSanPham { get; set; }

        [Required(ErrorMessage = "Mã nhãn hiệu là bắt buộc")]
        public long NhanHieuID { get; set; }

        [Required(ErrorMessage = "Ngày khởi tạo là bắt buộc")]
        public string NgayKhoiTao { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public string TrangThai { get; set; }

        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Mã phân loại là bắt buộc")]
        public long PhanLoaiID { get; set; }

        [Required(ErrorMessage = "Màu sắc là bắt buộc")]
        [StringLength(50, ErrorMessage = "Màu sắc không được dài quá 50 ký tự")]
        public string Mau { get; set; }

        [Required(ErrorMessage = "Bộ nhớ là bắt buộc")]
        [StringLength(50, ErrorMessage = "Bộ nhớ không được dài quá 50 ký tự")]
        public string BoNho { get; set; }

        [Required(ErrorMessage = "Khe sim là bắt buộc")]
        [StringLength(50, ErrorMessage = "Khe sim không được dài quá 50 ký tự")]
        public string KheSim { get; set; }

        [Required(ErrorMessage = "RAM là bắt buộc")]
        [StringLength(50, ErrorMessage = "RAM không được dài quá 50 ký tự")]
        public string Ram { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage = "Số lượng còn lại là bắt buộc")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng còn lại phải lớn hơn hoặc bằng 0")]
        public int ConLai { get; set; }

        public virtual NhanHieu NhanHieu { get; set; }
        public virtual PhanLoai PhanLoai { get; set; }
        public virtual ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
        public virtual ICollection<GioHangChiTiet> GioHangChiTiets { get; set; }
    }
}
