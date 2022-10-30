using System.Threading.Tasks;
using Telegram.Bot;

namespace BridgeWebTelegram.Core.Domain
{
    /// <summary>
    /// Gets the notification from the client - and sends it to Telegram
    /// </summary>
    public class NotificationDirector
    {
        private readonly long _ownerId;
        private readonly ITelegramBotClient _botClient;

        public NotificationDirector(long ownerId, ITelegramBotClient botClient)
        {
            _ownerId = ownerId;
            _botClient = botClient;
        }

        public async Task RedirectNotificationToOwner(IssueNotificaitonDto notification)
        {
            // TODO replace by IMessageFormatter
            string messageToOwner = $"An issue noticed at the {notification.Resource} " +
                $"at {notification.Timestamp} .";

            if (!string.IsNullOrWhiteSpace(notification.AdditionalParam1))
            {
                messageToOwner += $"\r\n Param1 : {notification.AdditionalParam1}";
            }
            if (!string.IsNullOrWhiteSpace(notification.AdditionalParam2))
            {
                messageToOwner += $"\r\n Param2 : {notification.AdditionalParam2}";
            }
            await _botClient.SendTextMessageAsync(
                chatId: _ownerId,
                text: messageToOwner
                );
        }
    }
}
