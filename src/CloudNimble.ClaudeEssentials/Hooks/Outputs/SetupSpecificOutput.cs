using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a Setup hook.
    /// </summary>
    public class SetupSpecificOutput : HookSpecificOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.Setup;

        /// <summary>
        /// Gets or sets additional context to add.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AdditionalContext { get; set; }

        #endregion

    }

}
