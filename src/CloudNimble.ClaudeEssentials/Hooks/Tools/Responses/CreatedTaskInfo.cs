using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a task created by the TaskCreate tool.
    /// </summary>
    public class CreatedTaskInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the task ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the task subject.
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        #endregion

    }

}
