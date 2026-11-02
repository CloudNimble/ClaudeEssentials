using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the CronList tool.
    /// </summary>
    public class CronListToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the scheduled jobs.
        /// </summary>
        [JsonPropertyName("jobs")]
        public List<CronJobInfo> Jobs { get; set; } = [];

        #endregion

    }

}
