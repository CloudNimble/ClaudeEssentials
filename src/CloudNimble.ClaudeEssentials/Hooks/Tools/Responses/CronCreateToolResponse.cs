using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the CronCreate tool.
    /// </summary>
    public class CronCreateToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the job survives restarts.
        /// </summary>
        [JsonPropertyName("durable")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Durable { get; set; }

        /// <summary>
        /// Gets or sets the human-readable schedule.
        /// </summary>
        [JsonPropertyName("humanSchedule")]
        public string HumanSchedule { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the job ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the epoch milliseconds of the job's next run, including the scheduler's per-job offset.
        /// </summary>
        [JsonPropertyName("nextFireAt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? NextFireAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the job recurs.
        /// </summary>
        [JsonPropertyName("recurring")]
        public bool Recurring { get; set; }

        #endregion

    }

}
