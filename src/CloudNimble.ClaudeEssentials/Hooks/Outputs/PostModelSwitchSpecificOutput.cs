using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a PostModelSwitch hook.
    /// </summary>
    public class PostModelSwitchSpecificOutput : HookSpecificOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.PostModelSwitch;

        /// <summary>
        /// Gets or sets context that reaches the model with the next request the new model serves.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AdditionalContext { get; set; }

        #endregion

    }

}
