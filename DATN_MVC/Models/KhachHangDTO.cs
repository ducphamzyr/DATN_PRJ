using DATN_MVC.Models.DonHang;

namespace DATN_MVC.Models.KhachHang
{
    public class KhachHangDTO
    {
        public int Id { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string TrangThaiTaiKhoan { get; set; }
        public int TongDonHang { get; set; }
        public decimal TongTienMua { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ThongKeKhachHangDTO
    {
        public decimal TongTienMua { get; set; }
        public int TongDonHang { get; set; }
        public int DonHangThanhCong { get; set; }
        public int DonHangHuy { get; set; }
        public decimal TongTienThangHienTai { get; set; }
        public List<DonHangDTO> LichSuMuaHang { get; set; }
    }
}