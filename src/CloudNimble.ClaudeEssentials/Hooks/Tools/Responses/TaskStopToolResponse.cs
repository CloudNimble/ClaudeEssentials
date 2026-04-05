using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Claude Code TaskStop tool after
    /// stopping a running background task.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "message": "Task stopped successfully",
    ///   "task_id": "task_abc123",
    ///   "task_type": "shell",
    ///   "command": "npm run dev"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class TaskStopToolResponse
    {

        /// <summary>
        /// Gets or sets the status message about the operation.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the task that was stopped.
        /// </summary>
        [JsonPropertyName("task_id")]
        public string TaskId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the task that was stopped.
        /// </summary>
        [JsonPropertyName("task_type")]
        public string TaskType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the command or description of the stopped task.
        /// </summary>
        [JsonPropertyName("command")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Command { get; set; }

    }

}
