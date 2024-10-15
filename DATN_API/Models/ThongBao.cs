using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN_API.Models
{
    public class ThongBao
    {
        [Key]
        public int ThongBaoID { get; set; }

        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public int TaiKhoanID { get; set; }

        [ForeignKey("TaiKhoanID")]
        public TaiKhoan TaiKhoan { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string NoiDung { get; set; }

        [Required(ErrorMessage = "Ghi chú không được để trống")]
        public string GhiChu { get; set; }

        [Required(ErrorMessage = "Ngày tạo không được để trống")]
        public DateTime NgayTao { get; set; }

        public DateTime? HetHan { get; set; }
    }
}