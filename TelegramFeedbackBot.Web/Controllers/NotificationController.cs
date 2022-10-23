using BridgeWebTelegram.Core.Domain;
using BridgeWebTelegram.Web.Dtos;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var notificationToPass = new IssueNotificaitonDto { 
                Resource = resource.resource ?? "Unknown resource", 
                AdditionalParam1 = environment ?? "Unknown",
                Timestamp = DateTime.Now };
            await _notificationDirector.RedirectNotificationToOwner(notificationToPass);
            return Ok();
        }
    }
}
