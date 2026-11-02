using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a UserPromptExpansion hook.
    /// </summary>
    public class UserPromptExpansionSpecificOutput : HookSpecificOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.UserPromptExpansion;

        /// <summary>
        /// Gets or sets additional context to add.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AdditionalContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to omit the original prompt from the block message when the decision is block.
        /// </summary>
        [JsonPropertyName("suppressOriginalPrompt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? SuppressOriginalPrompt { get; set; }

        #endregion

    }

}
