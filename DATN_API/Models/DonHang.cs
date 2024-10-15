using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN_API.Models
{
    public class DonHang
    {
        [Key]
        public int DonHangID { get; set; }

        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public int TaiKhoanID { get; set; }

        [ForeignKey("TaiKhoanID")]
        public TaiKhoan TaiKhoan { get; set; }

        public int? NhanVienID { get; set; }

        [ForeignKey("NhanVienID")]
        public NhanVien NhanVien { get; set; }

        [Required(ErrorMessage = "Phương thức thanh toán không được để trống")]
        public int PhuongThucThanhToanID { get; set; }

        [ForeignKey("PhuongThucThanhToanID")]
        public PhuongThucThanhToan PhuongThucThanhToan { get; set; }

        [Required(ErrorMessage = "Ghi chú không được để trống")]
        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Ngày đặt hàng không được để trống")]
        public DateTime NgayDatHang { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; }

        [Required(ErrorMessage = "Địa chỉ giao hàng không được để trống")]
        [StringLength(200, ErrorMessage = "Địa chỉ giao hàng không được vượt quá 200 ký tự")]
        public string DiaChiGiaoHang { get; set; }

        public DateTime? CapNhatLanCuoi { get; set; }

        public ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
    }
}