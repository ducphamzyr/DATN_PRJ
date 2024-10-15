using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN_API.Models
{
    public class GioHang
    {
        [Key]
        public int GioHangID { get; set; }

        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public int TaiKhoanID { get; set; }

        [ForeignKey("TaiKhoanID")]
        public TaiKhoan TaiKhoan { get; set; }

        [Required(ErrorMessage = "Ngày cập nhật cuối không được để trống")]
        public DateTime CapNhatLanCuoi { get; set; }

        public ICollection<GioHangChiTiet> GioHangChiTiets { get; set; }
    }
}