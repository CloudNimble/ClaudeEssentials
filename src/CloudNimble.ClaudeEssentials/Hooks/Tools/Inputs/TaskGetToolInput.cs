using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the TaskGet tool, which retrieves a task by its identifier.
    /// </summary>
    public class TaskGetToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the ID of the task to retrieve.
        /// </summary>
        [JsonPropertyName("taskId")]
        public string TaskId { get; set; } = string.Empty;

        #endregion

    }

}
