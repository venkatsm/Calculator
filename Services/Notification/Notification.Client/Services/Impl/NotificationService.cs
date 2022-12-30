using Notification.Client.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Client.Services.Impl
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationClient _notificationClient;

        public NotificationService()
        {
            _notificationClient = RestService.For<INotificationClient>("https://notification-api-ms.azurewebsites.net/");
        }

        public async Task SendNotification(NotificationRequest request)
        {
            await _notificationClient.SendNotification(request);
        }
    }
}
