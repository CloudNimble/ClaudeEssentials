using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents a permission decision returned by a PreToolUse or PreModelSwitch hook.
    /// </summary>
    public enum PermissionDecision
    {

        /// <summary>
        /// Allows the action without prompting the user.
        /// </summary>
        [JsonStringEnumMemberName("allow")]
        Allow,

        /// <summary>
        /// Denies the action and informs Claude of the reason.
        /// </summary>
        [JsonStringEnumMemberName("deny")]
        Deny,

        /// <summary>
        /// Asks the user to confirm the action.
        /// </summary>
        [JsonStringEnumMemberName("ask")]
        Ask,

        /// <summary>
        /// Defers the decision to the normal permission flow.
        /// </summary>
        [JsonStringEnumMemberName("defer")]
        Defer

    }

}
