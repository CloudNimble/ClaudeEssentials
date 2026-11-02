using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the TaskUpdate tool.
    /// </summary>
    public class TaskUpdateToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the error message when the update failed.
        /// </summary>
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the status transition, when the status changed.
        /// </summary>
        [JsonPropertyName("statusChange")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TaskStatusChange? StatusChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the update succeeded.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the ID of the updated task.
        /// </summary>
        [JsonPropertyName("taskId")]
        public string TaskId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the names of the fields that changed.
        /// </summary>
        [JsonPropertyName("updatedFields")]
        public List<string> UpdatedFields { get; set; } = [];

        #endregion

    }

}
