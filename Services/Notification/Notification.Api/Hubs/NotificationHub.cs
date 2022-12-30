using Microsoft.AspNetCore.SignalR;
using Notification.Api.Dtos;
using Notification.Api.Interfaces;

namespace Notification.Api.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task PublishNotification(NotificationRequest data) =>
            await Clients.Caller.SendAsync("PublishNotification", data);
    }
}
