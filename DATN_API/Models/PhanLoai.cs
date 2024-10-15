using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class PhanLoai
    {
        [Key]
        public int PhanLoaiID { get; set; }

        [Required(ErrorMessage = "Tên phân loại không được để trống")]
        [StringLength(100, ErrorMessage = "Tên phân loại không được vượt quá 100 ký tự")]
        public string TenPhanLoai { get; set; }

        public ICollection<SanPham> SanPhams { get; set; }
    }
}