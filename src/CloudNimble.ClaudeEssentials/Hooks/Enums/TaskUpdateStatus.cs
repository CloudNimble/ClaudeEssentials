using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the status a task can be moved to by the TaskUpdate tool.
    /// </summary>
    public enum TaskUpdateStatus
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
        Completed,

        /// <summary>
        /// The task is deleted.
        /// </summary>
        [JsonStringEnumMemberName("deleted")]
        Deleted

    }

}
