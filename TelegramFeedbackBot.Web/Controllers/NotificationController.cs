using BridgeWebTelegram.Core.Domain;
using BridgeWebTelegram.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BridgeWebTelegram.Web.Controllers
{
    [ApiController]
    [Route("Notification")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly NotificationDirector _notificationDirector;

        public NotificationController(ILogger<NotificationController> logger, NotificationDirector notificationDirector)
        {
            _logger = logger;
            _notificationDirector = notificationDirector;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Everything is fine");
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] PostNotificaitonDto resource)
        {
            await _notificationDirector.RedirectNotificationToOwner(new IssueNotificaitonDto { Resource = resource.resource ?? "Got it", Timestamp = DateTime.Now });
            return Ok();
        }
    }
}
