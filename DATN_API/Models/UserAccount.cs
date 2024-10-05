using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DATN_API.Models
{
    public class UserAccount
    {
        [Key]
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }

        public Role Role { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Staff> Staffs { get; set; }
    }
}
