using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the status of a notification sent through the PushNotification tool.
    /// </summary>
    public enum PushNotificationStatus
    {

        /// <summary>
        /// The notification was sent proactively by the model.
        /// </summary>
        [JsonStringEnumMemberName("proactive")]
        Proactive

    }

}
