using Newtonsoft.Json;

namespace BridgeWebTelegram.Web.Dtos
{

    /// <summary>
    /// A notification coming from the Web.
    /// Informs about some trouble on the resource
    /// </summary>    
    public class PostNotificaitonDto
    {
        /// <summary>
        /// A resource where a trouble happens. Variants:
        ///     FactoryX
        ///     Entrance9042
        ///     https://yoursite/page/17
        /// </summary>        
        public string resource { get; set; }

        /// <summary>
        /// Additional parameter 1 sent by the client. It may be time zone, info on the page, client name or smth. like this.
        /// </summary>        
        public string? param1 { get; set; }

        /// <summary>
        /// Additional parameter 2 sent by the client. It may be time zone, info on the page, client name or smth. like this.
        /// </summary>
        
        public string? param2 { get; set; }
    }
}
