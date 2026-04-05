using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the TaskOutput tool.
    /// The TaskOutput tool retrieves output from a background task by its ID.
    /// </summary>
    public class TaskOutputToolInput
    {

        /// <summary>
        /// Gets or sets the task ID to get output from.
        /// </summary>
        [JsonPropertyName("task_id")]
        public string TaskId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether to wait for completion.
        /// </summary>
        [JsonPropertyName("block")]
        public bool Block { get; set; }

        /// <summary>
        /// Gets or sets the max wait time in milliseconds.
        /// </summary>
        [JsonPropertyName("timeout")]
        public int Timeout { get; set; }

    }

}
