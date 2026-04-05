using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the TaskStop tool.
    /// The TaskStop tool stops a running background task by its ID.
    /// </summary>
    public class TaskStopToolInput
    {

        /// <summary>
        /// Gets or sets the ID of the background task to stop.
        /// </summary>
        [JsonPropertyName("task_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TaskId { get; set; }

        /// <summary>
        /// Gets or sets the shell ID. Deprecated: use <see cref="TaskId"/> instead.
        /// </summary>
        [JsonPropertyName("shell_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ShellId { get; set; }

    }

}
