using Notification.Api.Dtos;

namespace Notification.Api.Interfaces
{
    public interface INotificationHub
    {
        Task PublishNotification(NotificationRequest request);
    }
}
