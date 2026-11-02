using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the CronDelete tool, which removes a scheduled job.
    /// </summary>
    public class CronDeleteToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the job ID returned by the CronCreate tool.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        #endregion

    }

}
