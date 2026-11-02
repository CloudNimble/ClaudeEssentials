using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ScheduleWakeup tool, which schedules when a dynamic loop resumes.
    /// </summary>
    public class ScheduleWakeupToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the seconds from now to wake up, clamped to the range 60 to 3600 by the runtime. Required unless <c>stop</c> is true.
        /// </summary>
        [JsonPropertyName("delaySeconds")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? DelaySeconds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether nothing changed since the last tick. Required unless <c>stop</c> is true.
        /// </summary>
        [JsonPropertyName("noop")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Noop { get; set; }

        /// <summary>
        /// Gets or sets the loop input to fire on wake-up. Required unless <c>stop</c> is true.
        /// </summary>
        [JsonPropertyName("prompt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Prompt { get; set; }

        /// <summary>
        /// Gets or sets one short sentence explaining the chosen delay. Required unless <c>stop</c> is true.
        /// </summary>
        [JsonPropertyName("reason")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to end the dynamic loop immediately instead of scheduling another wakeup.
        /// </summary>
        [JsonPropertyName("stop")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Stop { get; set; }

        #endregion

    }

}
