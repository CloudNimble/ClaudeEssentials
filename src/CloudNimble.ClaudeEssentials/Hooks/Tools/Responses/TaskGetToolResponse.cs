using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the TaskGet tool.
    /// </summary>
    public class TaskGetToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the task, or null when no task has the requested ID.
        /// </summary>
        [JsonPropertyName("task")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public TaskDetailInfo? Task { get; set; }

        #endregion

    }

}
