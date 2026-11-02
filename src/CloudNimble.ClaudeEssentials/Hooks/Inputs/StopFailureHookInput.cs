using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a StopFailure hook.
    /// This hook runs when a stop hook itself fails.
    /// </summary>
    public class StopFailureHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the API error that ended the turn.
        /// </summary>
        [JsonPropertyName("error")]
        public StopFailureErrorType Error { get; set; }

        /// <summary>
        /// Gets or sets further detail about the error.
        /// </summary>
        [JsonPropertyName("error_details")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorDetails { get; set; }

        /// <summary>
        /// Gets or sets the text of the last assistant message.
        /// </summary>
        [JsonPropertyName("last_assistant_message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LastAssistantMessage { get; set; }

    }

}
