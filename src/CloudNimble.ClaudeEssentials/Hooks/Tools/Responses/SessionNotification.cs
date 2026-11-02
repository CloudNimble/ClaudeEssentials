using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a notification delivered to a session.
    /// </summary>
    public class SessionNotification
    {

        #region Properties

        /// <summary>
        /// Gets or sets the RFC 3339 timestamp, by this machine's clock, of when the notification reached this session's queue.
        /// </summary>
        [JsonPropertyName("arrived_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ArrivedAt { get; set; }

        /// <summary>
        /// Gets or sets the verbatim notification body.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the server-assigned stable ID, which is the deduplication key across redeliveries.
        /// </summary>
        [JsonPropertyName("notification_id")]
        public string NotificationId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the server-attested source token, such as <c>github_webhook</c>. This is an open set.
        /// </summary>
        [JsonPropertyName("origin")]
        public string Origin { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the RFC 3339 timestamp of when the backend queued the notification.
        /// </summary>
        [JsonPropertyName("queued_at")]
        public string QueuedAt { get; set; } = string.Empty;

        #endregion

    }

}
