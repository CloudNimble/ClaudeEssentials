using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the RemoteTrigger tool.
    /// </summary>
    public class RemoteTriggerToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the response body as a JSON string.
        /// </summary>
        [JsonPropertyName("json")]
        public string Json { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the result summary.
        /// </summary>
        [JsonPropertyName("summary")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Summary { get; set; }

        #endregion

    }

}
