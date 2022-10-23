using System;

namespace BridgeWebTelegram.Core.Domain
{
    /// <summary>
    /// Class represent info about a single notification has been sent by _client_.     
    /// Represents an info filled by the client code, so we don't trust it too much.
    /// </summary>
    public class IssueNotificaitonDto
    {
        /// <summary>
        /// A resource where an error occurs. Smth like https://yoursite/api/version/path
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// Additional parameter sent by the client. 
        /// </summary>
        public string AdditionalParam1 { get; set; }


        /// <summary>
        /// Additional parameter sent by the client. 
        /// </summary>
        public string AdditionalParam2 { get; set; }


        /// <summary>
        /// A timestamp on the client side. Bot can get the message later.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
