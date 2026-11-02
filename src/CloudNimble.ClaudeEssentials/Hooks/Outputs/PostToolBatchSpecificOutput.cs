using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a PostToolBatch hook.
    /// </summary>
    public class PostToolBatchSpecificOutput : HookSpecificOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.PostToolBatch;

        /// <summary>
        /// Gets or sets additional context to add.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AdditionalContext { get; set; }

        #endregion

    }

}
