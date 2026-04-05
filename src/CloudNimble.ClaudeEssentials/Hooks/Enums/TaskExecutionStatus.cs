using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the execution status of a Claude Code task. Named <c>TaskExecutionStatus</c> to avoid
    /// collision with <see cref="System.Threading.Tasks.TaskStatus" />.
    /// </summary>
    public enum TaskExecutionStatus
    {

        /// <summary>
        /// The task finished successfully.
        /// </summary>
        [JsonStringEnumMemberName("completed")]
        Completed,

        /// <summary>
        /// The task is currently executing.
        /// </summary>
        [JsonStringEnumMemberName("running")]
        Running,

        /// <summary>
        /// The task encountered an error during execution.
        /// </summary>
        [JsonStringEnumMemberName("error")]
        Error,

        /// <summary>
        /// The task was stopped before it could finish.
        /// </summary>
        [JsonStringEnumMemberName("interrupted")]
        Interrupted

    }

}
