namespace Notification.Client.Dtos
{
    public class NotificationRequest
    {
        public Guid Id { get; set; }
        public string SessionId { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
    }
}
