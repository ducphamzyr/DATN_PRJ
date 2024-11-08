using System.ComponentModel.DataAnnotations;

namespace DATN_API.DTOs
{
    public class NhanVienDTO
    {
        public int Id { get; set; }
        public string TenNhanVien { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string QueQuan { get; set; }
        public string SoCCCD { get; set; }
        public decimal LuongHienTai { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayNhanViec { get; set; }
        public int? TaiKhoanId { get; set; }
        public string? TenDangNhap { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class NhanVienDetailDTO : NhanVienDTO
    {
        public TaiKhoanNhanVienDTO ThongTinTaiKhoan { get; set; }
        public ThongKeDonHangNhanVienDTO ThongKeDonHang { get; set; }
        public List<DonHangGanDayDTO> DonHangGanDay { get; set; }
    }

    public class CreateNhanVienDTO
    {
        public string TenNhanVien { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string QueQuan { get; set; }
        public string SoCCCD { get; set; }
        public decimal LuongHienTai { get; set; }
        public DateTime NgayNhanViec { get; set; }
    }

    public class UpdateNhanVienDTO
    {
        public string TenNhanVien { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string QueQuan { get; set; }
        public string SoCCCD { get; set; }
        public decimal LuongHienTai { get; set; }
        public DateTime NgayNhanViec { get; set; }
    }

    public class TaiKhoanNhanVienDTO
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; }
        public string TenPhanQuyen { get; set; }
        public string TrangThai { get; set; }
    }

    public class ThongKeDonHangNhanVienDTO
    {
        public int? NhanVienId { get; set; }
        public string? TenNhanVien { get; set; }
        public int TongDonHang { get; set; }
        public int DonHangMoi { get; set; }
        public int DonHangDangXuLy { get; set; }
        public int DonHangHoanThanh { get; set; }
        public int DonHangHuy { get; set; }
    }

    public class DonHangGanDayDTO
    {
        public int Id { get; set; }
        public string TenKhachHang { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }

    public class AssignTaiKhoanDTO
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
    }

    public class ThongKeNhanVienDTO
    {
        public int TongNhanVien { get; set; }
        public int NhanVienHoatDong { get; set; }
        public int NhanVienMoi { get; set; }
        public int NhanVienNghi { get; set; }
        public decimal TongLuong { get; set; }
        public List<ThongKeDonHangNhanVienDTO> ThongKeTheoDonHang { get; set; }
    }
}