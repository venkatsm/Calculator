using Notification.Client.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Client
{
    internal interface INotificationClient
    {
        [Post("/api/Notification")]
        Task SendNotification([Body] NotificationRequest request);
    }
}
