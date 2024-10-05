namespace DATN_API.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string NotificationType { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<AccountNotification> AccountNotifications { get; set; }
    }
}
