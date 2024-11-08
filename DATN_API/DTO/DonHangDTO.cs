using System.ComponentModel.DataAnnotations;

namespace DATN_API.DTOs
{
    public class DonHangDTO
    {
        public int Id { get; set; }
        public int TaiKhoanId { get; set; }
        public string TenKhachHang { get; set; }
        public int? NhanVienId { get; set; }
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

    public class DonHangDetailDTO
    {
        public int Id { get; set; }
        public string MaDonHang { get; set; }
        public ThongTinKhachHangDTO ThongTinKhachHang { get; set; }
        public ThongTinNhanVienDTO ThongTinNhanVien { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public decimal TongTienHang { get; set; }
        public decimal TongGiamGia { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime? NgayXuLy { get; set; }
        public List<DonHangChiTietDTO> ChiTietDonHang { get; set; }
    }

    public class CreateDonHangDTO
    {
        [Required(ErrorMessage = "Địa chỉ giao hàng không được để trống")]
        public string DiaChiGiaoHang { get; set; }

        [Required(ErrorMessage = "Phương thức thanh toán không được để trống")]
        public int PhuongThucThanhToanId { get; set; }

        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Chi tiết đơn hàng không được để trống")]
        public List<CreateChiTietDonHangDTO> ChiTietDonHang { get; set; }
    }

    public class CreateChiTietDonHangDTO
    {
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public string SanPhamId { get; set; }

        public int? MaGiamGiaId { get; set; }

        [Required(ErrorMessage = "IMEI không được để trống")]
        [StringLength(50)]
        public string IMEI { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        public string GhiChu { get; set; }
    }

    public class DonHangChiTietDTO
    {
        public int Id { get; set; }
        public string SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public int? MaGiamGiaId { get; set; }
        public string TenMaGiamGia { get; set; }
        public string IMEI { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaTri { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }
    }

    public class ThongTinKhachHangDTO
    {
        public int Id { get; set; }
        public string TenKhachHang { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
    }

    public class ThongTinNhanVienDTO
    {
        public int Id { get; set; }
        public string TenNhanVien { get; set; }
        public string SoDienThoai { get; set; }
    }

    public class UpdateTrangThaiDonHangDTO
    {
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}