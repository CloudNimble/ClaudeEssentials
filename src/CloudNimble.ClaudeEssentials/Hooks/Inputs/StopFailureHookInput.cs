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
        /// Gets or sets the type of error that caused the stop failure.
        /// </summary>
        [JsonPropertyName("error_type")]
        public StopFailureErrorType ErrorType { get; set; }

    }

}
