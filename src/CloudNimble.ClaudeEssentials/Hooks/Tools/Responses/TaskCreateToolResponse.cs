using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the TaskCreate tool.
    /// </summary>
    public class TaskCreateToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the task that was created.
        /// </summary>
        [JsonPropertyName("task")]
        public CreatedTaskInfo Task { get; set; } = new();

        #endregion

    }

}
