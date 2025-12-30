using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a Notification hook.
    /// This hook runs when Claude Code sends notifications.
    /// </summary>
    public class NotificationHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the notification message content.
        /// For example: "Claude needs your permission to use Bash".
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of notification being sent.
        /// </summary>
        [JsonPropertyName("notification_type")]
        public NotificationType NotificationType { get; set; }

    }

}
