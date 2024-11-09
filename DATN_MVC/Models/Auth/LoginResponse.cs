namespace DATN_MVC.Models.Auth
{
    public class LoginResponse
    {
        public int Id { get; set; }  // Thêm property Id
        public string Token { get; set; }
        public string TenDangNhap { get; set; }
        public string TenKhachHang { get; set; }
        public string Email { get; set; }
        public string TenPhanQuyen { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
