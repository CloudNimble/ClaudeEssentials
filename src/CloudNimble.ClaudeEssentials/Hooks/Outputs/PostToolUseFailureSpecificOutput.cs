using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a PostToolUseFailure hook.
    /// Contains additional context about the tool failure for Claude.
    /// </summary>
    public class PostToolUseFailureSpecificOutput : HookSpecificOutputBase
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.PostToolUseFailure;

        /// <summary>
        /// Gets or sets additional context information for Claude about the failure.
        /// This information is added to Claude's context about the tool execution failure.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        public string? AdditionalContext { get; set; }

    }

}
