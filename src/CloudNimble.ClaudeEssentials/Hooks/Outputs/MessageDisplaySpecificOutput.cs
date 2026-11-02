using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a MessageDisplay hook, which replaces text on screen without changing the stored message.
    /// </summary>
    public class MessageDisplaySpecificOutput : HookSpecificOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.MessageDisplay;

        /// <summary>
        /// Gets or sets the text displayed in place of the delta. Omit it to display the original.
        /// </summary>
        [JsonPropertyName("displayContent")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DisplayContent { get; set; }

        #endregion

    }

}
