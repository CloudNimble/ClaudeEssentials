using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Monitor tool.
    /// </summary>
    public class MonitorToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the monitor has no timeout and runs until stopped or the session ends.
        /// </summary>
        [JsonPropertyName("persistent")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Persistent { get; set; }

        /// <summary>
        /// Gets or sets the ID of the background monitor task.
        /// </summary>
        [JsonPropertyName("taskId")]
        public string TaskId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the timeout deadline in milliseconds, or 0 when persistent.
        /// </summary>
        [JsonPropertyName("timeoutMs")]
        public long TimeoutMs { get; set; }

        #endregion

    }

}
