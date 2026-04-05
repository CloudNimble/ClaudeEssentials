using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the status of a to-do item tracked by Claude Code.
    /// </summary>
    public enum TodoItemStatus
    {

        /// <summary>
        /// The to-do item has not yet been started.
        /// </summary>
        [JsonStringEnumMemberName("pending")]
        Pending,

        /// <summary>
        /// The to-do item is currently being worked on.
        /// </summary>
        [JsonStringEnumMemberName("in_progress")]
        InProgress,

        /// <summary>
        /// The to-do item has been finished.
        /// </summary>
        [JsonStringEnumMemberName("completed")]
        Completed

    }

}
