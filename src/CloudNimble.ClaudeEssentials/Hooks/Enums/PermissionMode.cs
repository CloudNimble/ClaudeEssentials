using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the permission mode under which Claude Code is operating.
    /// </summary>
    public enum PermissionMode
    {

        /// <summary>
        /// Standard behavior that prompts for dangerous operations.
        /// </summary>
        [JsonStringEnumMemberName("default")]
        Default,

        /// <summary>
        /// Automatically accepts file edit operations.
        /// </summary>
        [JsonStringEnumMemberName("acceptEdits")]
        AcceptEdits,

        /// <summary>
        /// Bypasses all permission checks. Use with caution.
        /// </summary>
        [JsonStringEnumMemberName("bypassPermissions")]
        BypassPermissions,

        /// <summary>
        /// Plans without executing changes.
        /// </summary>
        [JsonStringEnumMemberName("plan")]
        Plan,

        /// <summary>
        /// Denies anything that is not already allowed instead of prompting.
        /// </summary>
        [JsonStringEnumMemberName("dontAsk")]
        DontAsk,

        /// <summary>
        /// Claude Code makes permission decisions without prompting the user.
        /// </summary>
        [JsonStringEnumMemberName("auto")]
        Auto

    }

}
