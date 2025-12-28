using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Enums;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a UserPromptSubmit hook.
    /// Combines base output fields with UserPromptSubmit-specific output.
    /// </summary>
    public class UserPromptSubmitHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the decision for the user prompt submission.
        /// Set to <see cref="HookDecision.Block"/> to block the prompt from being processed.
        /// </summary>
        [JsonPropertyName("decision")]
        public HookDecision? Decision { get; set; }

        /// <summary>
        /// Gets or sets the reason for blocking the prompt.
        /// Only shown to the user when the prompt is blocked.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets the hook-specific output containing additional context.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public UserPromptSubmitSpecificOutput? HookSpecificOutput { get; set; }

    }

}
