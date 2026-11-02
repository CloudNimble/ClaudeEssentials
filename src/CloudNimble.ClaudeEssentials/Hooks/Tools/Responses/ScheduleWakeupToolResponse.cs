using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the ScheduleWakeup tool.
    /// </summary>
    public class ScheduleWakeupToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets how many pending dynamic-loop wakeups the stop request cancelled.
        /// </summary>
        [JsonPropertyName("cancelledWakeups")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CancelledWakeups { get; set; }

        /// <summary>
        /// Gets or sets the actual delay used after clamping to the runtime bounds.
        /// </summary>
        [JsonPropertyName("clampedDelaySeconds")]
        public double ClampedDelaySeconds { get; set; }

        /// <summary>
        /// Gets or sets the epoch milliseconds when the next wakeup will fire.
        /// </summary>
        [JsonPropertyName("scheduledFor")]
        public long ScheduledFor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the model ended the loop.
        /// </summary>
        [JsonPropertyName("stopped")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Stopped { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the requested delay was outside the allowed range.
        /// </summary>
        [JsonPropertyName("wasClamped")]
        public bool WasClamped { get; set; }

        #endregion

    }

}
