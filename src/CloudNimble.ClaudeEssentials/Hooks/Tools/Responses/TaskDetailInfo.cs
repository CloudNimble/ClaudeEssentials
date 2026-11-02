using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the details of a task returned by the TaskGet tool.
    /// </summary>
    public class TaskDetailInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the IDs of the tasks blocking this task.
        /// </summary>
        [JsonPropertyName("blockedBy")]
        public List<string> BlockedBy { get; set; } = [];

        /// <summary>
        /// Gets or sets the IDs of the tasks this task blocks.
        /// </summary>
        [JsonPropertyName("blocks")]
        public List<string> Blocks { get; set; } = [];

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the task ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

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
