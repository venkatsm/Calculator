using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Notification.Api.Dtos;
using Notification.Api.Hubs;

namespace Notification.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationController(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _hub.Clients.All.SendAsync("PublishNotification", DateTime.Now);
            return Ok(new { Message = "Request Completed" });
        }

        [HttpPost]
        public IActionResult SendNotification(NotificationRequest request)
        {
            _hub.Clients.All.SendAsync("PublishNotification", request);
            return Ok(new { Message = "Request Completed" });
        }
    }
}
