using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Enums;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a SessionEnd hook.
    /// This hook runs when a Claude Code session ends.
    /// </summary>
    public class SessionEndHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the reason why the session ended.
        /// </summary>
        [JsonPropertyName("reason")]
        public SessionEndReason Reason { get; set; }

    }

}
