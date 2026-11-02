using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a MessageDisplay hook, which runs as assistant message text is displayed.
    /// </summary>
    public class MessageDisplayHookInput : HookInputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets the newly completed lines since the prior flush.
        /// </summary>
        [JsonPropertyName("delta")]
        public string Delta { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this is the message's last flush.
        /// </summary>
        [JsonPropertyName("final")]
        public bool Final { get; set; }

        /// <summary>
        /// Gets or sets the zero-based index of this delta within the message.
        /// </summary>
        [JsonPropertyName("index")]
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the UUID of the assistant message being displayed, which is stable across every flush of the same message.
        /// </summary>
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the UUID of the current turn.
        /// </summary>
        [JsonPropertyName("turn_id")]
        public string TurnId { get; set; } = string.Empty;

        #endregion

    }

}
