using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class PhanQuyen
    {
        [Key]
        public int PhanQuyenID { get; set; }

        [Required(ErrorMessage = "Tên phân quyền không được để trống")]
        [StringLength(50, ErrorMessage = "Tên phân quyền không được vượt quá 50 ký tự")]
        public string TenPhanQuyen { get; set; }

        public ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}