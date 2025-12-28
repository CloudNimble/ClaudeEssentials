using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a SessionStart hook.
    /// Combines base output fields with SessionStart-specific output.
    /// </summary>
    public class SessionStartHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing additional context.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public SessionStartSpecificOutput? HookSpecificOutput { get; set; }

    }

}
