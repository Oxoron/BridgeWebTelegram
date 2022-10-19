using LtMaterialsBot.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramFeedbackBot.Web.Controllers
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
        public async Task<IActionResult> Post([FromBody] PostNotificaiton resource)
        {   
            await _notificationDirector.RedirectNotificationToOwner(new IssueNotificaitonDto { Resource = resource.resource ?? "Got it", Timestamp = DateTime.Now });
            return Ok();
        }

        public class PostNotificaiton
        {
            public string resource { get; set; }
        }
    }
}
