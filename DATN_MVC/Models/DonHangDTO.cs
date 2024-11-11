namespace DATN_MVC.Models.DonHang
{
    public class DonHangDTO
    {
        public int Id { get; set; }
        public string MaDonHang { get; set; }
        public string TenKhachHang { get; set; }
        public string TenNhanVien { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public decimal TongTien { get; set; }
        public int TongSanPham { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class DonHangChiTietDTO
    {
        public int Id { get; set; }
        public string SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public string IMEI { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaTri { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }
        public string TenMaGiamGia { get; set; }
        public int? PhanTramGiam { get; set; }
    }
}