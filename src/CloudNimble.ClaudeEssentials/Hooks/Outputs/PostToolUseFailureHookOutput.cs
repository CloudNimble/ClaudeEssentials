using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a PostToolUseFailure hook.
    /// Combines base output fields with PostToolUseFailure-specific output.
    /// </summary>
    public class PostToolUseFailureHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing additional context about the failure.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public PostToolUseFailureSpecificOutput? HookSpecificOutput { get; set; }

    }

}
