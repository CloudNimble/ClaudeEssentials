using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the ReadNotifications tool.
    /// </summary>
    public class ReadNotificationsToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the notifications drained from the queue.
        /// </summary>
        [JsonPropertyName("notifications")]
        public List<SessionNotification> Notifications { get; set; } = [];

        /// <summary>
        /// Gets or sets the RFC 3339 timestamp, by this machine's clock, of when this call read the queue.
        /// </summary>
        [JsonPropertyName("read_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ReadAt { get; set; }

        /// <summary>
        /// Gets or sets the notifications still queued after this drain. Call the tool again to read them.
        /// </summary>
        [JsonPropertyName("remaining")]
        public int Remaining { get; set; }

        #endregion

    }

}
