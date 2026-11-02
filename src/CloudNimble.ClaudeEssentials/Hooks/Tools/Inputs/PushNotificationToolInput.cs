using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the PushNotification tool.
    /// </summary>
    public class PushNotificationToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the notification body. Mobile operating systems truncate long messages.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the notification status.
        /// </summary>
        [JsonPropertyName("status")]
        public PushNotificationStatus Status { get; set; }

        #endregion

    }

}
