using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the TaskUpdate tool, which changes an existing task.
    /// </summary>
    public class TaskUpdateToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the present continuous form shown in the spinner while in progress, such as "Running tests".
        /// </summary>
        [JsonPropertyName("activeForm")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ActiveForm { get; set; }

        /// <summary>
        /// Gets or sets the task IDs that block this task.
        /// </summary>
        [JsonPropertyName("addBlockedBy")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? AddBlockedBy { get; set; }

        /// <summary>
        /// Gets or sets the task IDs that this task blocks.
        /// </summary>
        [JsonPropertyName("addBlocks")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? AddBlocks { get; set; }

        /// <summary>
        /// Gets or sets the new description for the task.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the metadata keys to merge into the task. Set a key to null to delete it.
        /// </summary>
        [JsonPropertyName("metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, JsonElement>? Metadata { get; set; }

        /// <summary>
        /// Gets or sets the new owner for the task.
        /// </summary>
        [JsonPropertyName("owner")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Owner { get; set; }

        /// <summary>
        /// Gets or sets the new status for the task.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TaskUpdateStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets the new subject for the task.
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the ID of the task to update.
        /// </summary>
        [JsonPropertyName("taskId")]
        public string TaskId { get; set; } = string.Empty;

        #endregion

    }

}
