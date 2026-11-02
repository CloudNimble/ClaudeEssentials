using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the status of a task tracked by the task tools.
    /// </summary>
    public enum TaskItemStatus
    {

        /// <summary>
        /// The task has not been started.
        /// </summary>
        [JsonStringEnumMemberName("pending")]
        Pending,

        /// <summary>
        /// The task is in progress.
        /// </summary>
        [JsonStringEnumMemberName("in_progress")]
        InProgress,

        /// <summary>
        /// The task is complete.
        /// </summary>
        [JsonStringEnumMemberName("completed")]
        Completed

    }

}
