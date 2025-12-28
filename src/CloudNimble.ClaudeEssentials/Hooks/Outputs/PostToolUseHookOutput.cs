using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Enums;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a PostToolUse hook.
    /// Combines base output fields with PostToolUse-specific output.
    /// </summary>
    public class PostToolUseHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the decision for the post-tool-use operation.
        /// Set to <see cref="HookDecision.Block"/> to block further processing.
        /// </summary>
        [JsonPropertyName("decision")]
        public HookDecision? Decision { get; set; }

        /// <summary>
        /// Gets or sets the reason for the decision.
        /// Required when <see cref="Decision"/> is set to explain the rationale.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets the hook-specific output containing additional context.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public PostToolUseSpecificOutput? HookSpecificOutput { get; set; }

    }

}
