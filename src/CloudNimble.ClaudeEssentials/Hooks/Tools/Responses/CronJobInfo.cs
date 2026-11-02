using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a scheduled job returned by the CronList tool.
    /// </summary>
    public class CronJobInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the cron expression.
        /// </summary>
        [JsonPropertyName("cron")]
        public string Cron { get; set; } = string.Empty;

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
        /// Gets or sets the prompt enqueued at each fire time.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the job recurs.
        /// </summary>
        [JsonPropertyName("recurring")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Recurring { get; set; }

        #endregion

    }

}
