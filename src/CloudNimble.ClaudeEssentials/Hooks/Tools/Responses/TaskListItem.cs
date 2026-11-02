using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a task returned by the TaskList tool.
    /// </summary>
    public class TaskListItem
    {

        #region Properties

        /// <summary>
        /// Gets or sets the IDs of the tasks blocking this task.
        /// </summary>
        [JsonPropertyName("blockedBy")]
        public List<string> BlockedBy { get; set; } = [];

        /// <summary>
        /// Gets or sets the task ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the task owner.
        /// </summary>
        [JsonPropertyName("owner")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Owner { get; set; }

        /// <summary>
        /// Gets or sets the task status.
        /// </summary>
        [JsonPropertyName("status")]
        public TaskItemStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the task subject.
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        #endregion

    }

}
