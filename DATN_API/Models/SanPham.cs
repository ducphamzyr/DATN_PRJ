using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN_API.Models
{
    public class SanPham
    {
        [Key]
        public int SanPhamID { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm không được vượt quá 200 ký tự")]
        public string TenSanPham { get; set; }

        [Required(ErrorMessage = "Nhãn hiệu không được để trống")]
        public int NhanHieuID { get; set; }

        [ForeignKey("NhanHieuID")]
        public NhanHieu NhanHieu { get; set; }

        [Required(ErrorMessage = "Ngày phát hành không được để trống")]
        public DateTime NgayPhatHanh { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; }

        [Required(ErrorMessage = "Ghi chú không được để trống")]
        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Phân loại không được để trống")]
        public int PhanLoaiID { get; set; }

        [ForeignKey("PhanLoaiID")]
        public PhanLoai PhanLoai { get; set; }

        public ICollection<SanPhamChiTiet> SanPhamChiTiets { get; set; }
        public ICollection<MaGiamGia> MaGiamGias { get; set; }
    }
}