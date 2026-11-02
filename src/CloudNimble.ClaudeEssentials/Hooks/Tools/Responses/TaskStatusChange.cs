using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a task's status transition.
    /// </summary>
    public class TaskStatusChange
    {

        #region Properties

        /// <summary>
        /// Gets or sets the previous status.
        /// </summary>
        [JsonPropertyName("from")]
        public string From { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new status.
        /// </summary>
        [JsonPropertyName("to")]
        public string To { get; set; } = string.Empty;

        #endregion

    }

}
