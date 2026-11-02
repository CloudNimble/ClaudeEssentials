using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the TaskCreate tool, which adds a task to the session's task list.
    /// </summary>
    public class TaskCreateToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the present continuous form shown in the spinner while in progress, such as "Running tests".
        /// </summary>
        [JsonPropertyName("activeForm")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ActiveForm { get; set; }

        /// <summary>
        /// Gets or sets what needs to be done.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets arbitrary metadata to attach to the task.
        /// </summary>
        [JsonPropertyName("metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, JsonElement>? Metadata { get; set; }

        /// <summary>
        /// Gets or sets a brief title for the task.
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        #endregion

    }

}
