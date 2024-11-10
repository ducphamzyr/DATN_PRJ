using System.ComponentModel.DataAnnotations;

namespace DATN_MVC.Models.PhanQuyen
{
    public class PhanQuyenDTO
    {
        public int Id { get; set; }
        public string TenPhanQuyen { get; set; }
        public int SoLuongTaiKhoan { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }

    public class CreatePhanQuyenDTO
    {
        [Required(ErrorMessage = "Tên phân quyền không được để trống")]
        [StringLength(100, ErrorMessage = "Tên phân quyền không được vượt quá 100 ký tự")]
        public string TenPhanQuyen { get; set; }
    }

    public class UpdatePhanQuyenDTO
    {
        [Required(ErrorMessage = "Tên phân quyền không được để trống")]
        [StringLength(100, ErrorMessage = "Tên phân quyền không được vượt quá 100 ký tự")]
        public string TenPhanQuyen { get; set; }
    }
    public class PhanQuyenDetailDTO : PhanQuyenDTO
    {
        public List<TaiKhoanTrongPhanQuyenDTO> DanhSachTaiKhoan { get; set; }
    }

    public class TaiKhoanTrongPhanQuyenDTO
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; }
        public string TenKhachHang { get; set; }
        public string Email { get; set; }
        public string TrangThai { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
