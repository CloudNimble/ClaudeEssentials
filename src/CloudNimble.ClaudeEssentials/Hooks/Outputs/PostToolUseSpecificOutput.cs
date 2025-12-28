using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Enums;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a PostToolUse hook.
    /// Contains additional context to provide to Claude after tool execution.
    /// </summary>
    public class PostToolUseSpecificOutput : HookSpecificOutputBase
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.PostToolUse;

        /// <summary>
        /// Gets or sets additional context information to provide to Claude.
        /// This information is added to Claude's context about the tool execution.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        public string? AdditionalContext { get; set; }

    }

}
