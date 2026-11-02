using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a PostToolUse hook.
    /// Combines base output fields with PostToolUse-specific output.
    /// </summary>
    public class PostToolUseHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing additional context.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public PostToolUseSpecificOutput? HookSpecificOutput { get; set; }

    }

}
