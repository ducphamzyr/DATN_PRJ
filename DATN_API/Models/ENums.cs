namespace DATN_API.Models
{
    public class ENums
    {
        public enum TrangThaiDonHang
        {
            Moi,
            DangXuLy,
            DaHoanThanh,
            DangCho,
            DaHuy
        }

        public enum TrangThaiTaiKhoan
        {
            HoatDong,
            NgungHoatDong,
            BiKhoa,
            DaXoa
        }

        public enum TrangThaiSanPham
        {
            ConHang,
            HetHang,
            NgungCungCap,
            SapToi
        }

        public enum TrangThaiNhanVien
        {
            HoatDong,
            Ngung,
            KetThuc,
            TamNghi
        }

        public enum LoaiGiamGia
        {
            PhanTram,
            GiamGia
        }
    }
}
