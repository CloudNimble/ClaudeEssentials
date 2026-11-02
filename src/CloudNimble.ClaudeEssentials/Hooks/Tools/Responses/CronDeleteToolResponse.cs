using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the CronDelete tool.
    /// </summary>
    public class CronDeleteToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the ID of the deleted job.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        #endregion

    }

}
