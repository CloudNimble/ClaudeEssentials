using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the permission mode under which Claude Code is operating.
    /// </summary>
    public enum PermissionMode
    {

        /// <summary>
        /// Default permission mode requiring explicit user approval for sensitive operations.
        /// </summary>
        [JsonStringEnumMemberName("default")]
        Default,

        /// <summary>
        /// Plan mode where Claude explores and plans but doesn't execute changes.
        /// </summary>
        [JsonStringEnumMemberName("plan")]
        Plan,

        /// <summary>
        /// Mode that automatically accepts file edits without prompting.
        /// </summary>
        [JsonStringEnumMemberName("acceptEdits")]
        AcceptEdits,

        /// <summary>
        /// Mode that bypasses all permission prompts. Use with caution.
        /// </summary>
        [JsonStringEnumMemberName("bypassPermissions")]
        BypassPermissions

    }

}
