using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the type of notification sent by Claude Code.
    /// </summary>
    public enum NotificationType
    {

        /// <summary>
        /// A permission prompt notification requiring user action.
        /// </summary>
        [JsonStringEnumMemberName("permission_prompt")]
        PermissionPrompt,

        /// <summary>
        /// An idle prompt notification indicating Claude is waiting for input.
        /// </summary>
        [JsonStringEnumMemberName("idle_prompt")]
        IdlePrompt,

        /// <summary>
        /// A notification indicating successful authentication.
        /// </summary>
        [JsonStringEnumMemberName("auth_success")]
        AuthSuccess,

        /// <summary>
        /// An elicitation dialog notification for gathering user input.
        /// </summary>
        [JsonStringEnumMemberName("elicitation_dialog")]
        ElicitationDialog

    }

}
