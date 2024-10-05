namespace DATN_API.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public ICollection<UserAccount> UserAccounts { get; set; }
    }
}
