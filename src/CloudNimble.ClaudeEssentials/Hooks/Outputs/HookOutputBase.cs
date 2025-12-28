using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Base class containing common fields for all hook outputs.
    /// Hook outputs are written to stdout as JSON when the hook exits with code 0.
    /// </summary>
    public abstract class HookOutputBase
    {

        /// <summary>
        /// Gets or sets a value indicating whether to continue with the operation.
        /// When set to false, the operation will be stopped.
        /// Defaults to true.
        /// </summary>
        /// <remarks>
        /// This property is always serialized to ensure explicit intent is communicated.
        /// </remarks>
        [JsonPropertyName("continue")]
        public bool Continue { get; set; } = true;

        /// <summary>
        /// Gets or sets the reason message shown when <see cref="Continue"/> is false.
        /// This message is displayed to the user when the operation is stopped.
        /// </summary>
        [JsonPropertyName("stopReason")]
        public string? StopReason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to suppress output from the transcript.
        /// When true, the hook's output will not appear in the conversation transcript.
        /// Defaults to false.
        /// </summary>
        [JsonPropertyName("suppressOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool SuppressOutput { get; set; }

        /// <summary>
        /// Gets or sets an optional warning message to include in the system context.
        /// This message is shown to Claude as additional context.
        /// </summary>
        [JsonPropertyName("systemMessage")]
        public string? SystemMessage { get; set; }

    }

}
