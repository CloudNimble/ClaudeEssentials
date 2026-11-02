using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a UserPromptSubmit hook.
    /// Contains additional context to add to Claude's processing of the user prompt.
    /// </summary>
    public class UserPromptSubmitSpecificOutput : HookSpecificOutputBase
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.UserPromptSubmit;

        /// <summary>
        /// Gets or sets additional context information to provide to Claude.
        /// This information is added to Claude's context when processing the user's prompt.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        public string? AdditionalContext { get; set; }

        /// <summary>
        /// Gets or sets the session title.
        /// </summary>
        [JsonPropertyName("sessionTitle")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SessionTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to omit the original prompt from the block message when the decision is block.
        /// </summary>
        [JsonPropertyName("suppressOriginalPrompt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? SuppressOriginalPrompt { get; set; }

    }

}
