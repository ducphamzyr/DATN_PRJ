using System.ComponentModel.DataAnnotations;

namespace DATN_API.DTOs
{
    public class ThongBaoDTO
    {
        public int Id { get; set; }
        public int TaiKhoanId { get; set; }
        public string TenTaiKhoan { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string GhiChu { get; set; }
        public DateTime HetHan { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ThongBaoDetailDTO
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string GhiChu { get; set; }
        public DateTime HetHan { get; set; }
        public ThongTinTaiKhoanDTO TaiKhoan { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateThongBaoDTO
    {
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public int TaiKhoanId { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(255, ErrorMessage = "Tiêu đề không được vượt quá 255 ký tự")]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string NoiDung { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Thời hạn không được để trống")]
        public DateTime HetHan { get; set; }
    }

    public class BroadcastThongBaoDTO
    {
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(255, ErrorMessage = "Tiêu đề không được vượt quá 255 ký tự")]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string NoiDung { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Thời hạn không được để trống")]
        public DateTime HetHan { get; set; }
    }

    public class ThongTinTaiKhoanDTO
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; }
        public string TenKhachHang { get; set; }
        public string Email { get; set; }
    }
}