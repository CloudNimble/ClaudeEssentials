using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the PushNotification tool.
    /// </summary>
    public class PushNotificationToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets why the notification was not delivered.
        /// </summary>
        [JsonPropertyName("disabledReason")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PushNotificationDisabledReason? DisabledReason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a local notification was sent.
        /// </summary>
        [JsonPropertyName("localSent")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? LocalSent { get; set; }

        /// <summary>
        /// Gets or sets the notification message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether a push notification was sent.
        /// </summary>
        [JsonPropertyName("pushSent")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? PushSent { get; set; }

        /// <summary>
        /// Gets or sets the ISO timestamp captured at tool execution on the emitting process.
        /// </summary>
        [JsonPropertyName("sentAt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SentAt { get; set; }

        #endregion

    }

}
