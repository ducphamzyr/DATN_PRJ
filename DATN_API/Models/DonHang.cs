using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class DonHang
    {
        [Key]
        public long DonHangID { get; set; }

        [Required(ErrorMessage = "Mã tài khoản là bắt buộc")]
        public long TaiKhoanID { get; set; }

        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        public long NhanVienID { get; set; }

        [Required(ErrorMessage = "Mã phương thức thanh toán là bắt buộc")]
        public long PhuongThucThanhToanID { get; set; }

        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Ngày đặt hàng là bắt buộc")]
        public DateTime NgayDatHang { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public string TrangThai { get; set; }

        [Required(ErrorMessage = "Địa chỉ giao hàng là bắt buộc")]
        public string DiaChiGiaoHang { get; set; }

        [Required(ErrorMessage = "Ngày cập nhật lần cuối là bắt buộc")]
        public DateTime CapNhatLanCuoi { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual NhanVien NhanVien { get; set; }
        public virtual PhuongThucThanhToan PhuongThucThanhToan { get; set; }
        public virtual ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
    }
}
