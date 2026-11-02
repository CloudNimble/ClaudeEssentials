using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the CronCreate tool, which schedules a prompt on a cron expression.
    /// </summary>
    public class CronCreateToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the standard 5-field cron expression in local time.
        /// </summary>
        [JsonPropertyName("cron")]
        public string Cron { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to persist the job so that it survives restarts. The default keeps it in memory for this session only.
        /// </summary>
        [JsonPropertyName("durable")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Durable { get; set; }

        /// <summary>
        /// Gets or sets the prompt to enqueue at each fire time.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to fire on every match until deleted or expired after 7 days (the default) rather than once.
        /// </summary>
        [JsonPropertyName("recurring")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Recurring { get; set; }

        #endregion

    }

}
