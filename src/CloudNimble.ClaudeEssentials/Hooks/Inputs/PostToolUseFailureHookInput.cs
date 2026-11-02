using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a PostToolUseFailure hook.
    /// This hook runs after a tool call fails.
    /// </summary>
    public class PostToolUseFailureHookInput : ToolHookInputBase<object>
    {

        /// <summary>
        /// Gets or sets the description of what went wrong during the tool execution.
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the failure was caused by a user interruption.
        /// </summary>
        [JsonPropertyName("is_interrupt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsInterrupt { get; set; }

        /// <summary>
        /// Gets or sets the tool execution time in milliseconds, excluding permission-prompt and hook time.
        /// </summary>
        [JsonPropertyName("duration_ms")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? DurationMs { get; set; }

    }

}
