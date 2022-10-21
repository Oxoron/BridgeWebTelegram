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
        ///     https://youtsite/page/17
        /// </summary>
        public string resource { get; set; }
    }
}
