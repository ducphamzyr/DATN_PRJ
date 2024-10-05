namespace DATN_API.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public int AccountID { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }

        public UserAccount UserAccount { get; set; }
    }
}
