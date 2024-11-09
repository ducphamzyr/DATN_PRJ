using System.ComponentModel.DataAnnotations;

namespace DATN_MVC.Models.Auth
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
    }
}
