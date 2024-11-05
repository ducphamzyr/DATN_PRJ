using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class GioHang
    {
        [Key]
        public long GioHangID { get; set; }
        public string TenNguoiDung { get; set; }

        [Required(ErrorMessage = "Ngày cập nhật lần cuối là bắt buộc")]
        public DateTime CapNhatLanCuoi { get; set; }
        public virtual ICollection<GioHangChiTiet> GioHangChiTiets { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
