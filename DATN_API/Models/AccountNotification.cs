namespace DATN_API.Models
{
    public class AccountNotification
    {
        public int AccountNotificationID { get; set; }
        public int AccountID { get; set; }
        public int NotificationID { get; set; }
        public bool IsRead { get; set; }

        public UserAccount UserAccount { get; set; }
        public Notification Notification { get; set; }
    }
}
