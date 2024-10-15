using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DATN_API.Models
{
    public class NhanHieu
    {
        [Key]
        public int NhanHieuID { get; set; }

        [Required(ErrorMessage = "Tên nhãn hiệu không được để trống")]
        [StringLength(100, ErrorMessage = "Tên nhãn hiệu không được vượt quá 100 ký tự")]
        public string TenNhanHieu { get; set; }

        [Required(ErrorMessage = "Xuất xứ không được để trống")]
        [StringLength(100, ErrorMessage = "Xuất xứ không được vượt quá 100 ký tự")]
        public string XuatXu { get; set; }

        public ICollection<SanPham> SanPhams { get; set; }
    }
}