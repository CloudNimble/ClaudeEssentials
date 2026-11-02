using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents a session-scoped cron task that will wake a session later.
    /// </summary>
    public class SessionCronSummary
    {

        #region Properties

        /// <summary>
        /// Gets or sets the task ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the prompt text submitted when the task fires, capped at 1000 characters.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the task re-fires on every match rather than being a one-shot wakeup.
        /// </summary>
        [JsonPropertyName("recurring")]
        public bool Recurring { get; set; }

        /// <summary>
        /// Gets or sets the cron expression.
        /// </summary>
        [JsonPropertyName("schedule")]
        public string Schedule { get; set; } = string.Empty;

        #endregion

    }

}
