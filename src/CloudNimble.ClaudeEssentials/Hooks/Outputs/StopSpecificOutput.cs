using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a Stop hook.
    /// </summary>
    public class StopSpecificOutput : HookSpecificOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.Stop;

        /// <summary>
        /// Gets or sets non-error feedback delivered to the model. The conversation continues so that the model can act on it.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AdditionalContext { get; set; }

        #endregion

    }

}
