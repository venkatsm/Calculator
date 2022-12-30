using Notification.Client.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Client.Services
{
    public interface INotificationService
    {
        Task SendNotification(NotificationRequest request);
    }
}
