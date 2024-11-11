namespace DATN_MVC.Models.TaiKhoan
{
    public class GioHangDTO
    {
        public int Id { get; set; }
        public int TaiKhoanId { get; set; }
        public int TongSanPham { get; set; }
        public decimal TongTien { get; set; }
        public List<GioHangChiTietDTO> ChiTietGioHang { get; set; }
    }

    public class GioHangChiTietDTO
    {
        public int Id { get; set; }
        public string SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }
        public string Mau { get; set; }
        public string BoNho { get; set; }
        public string Ram { get; set; }
        public int SoLuongTonKho { get; set; }
    }
}