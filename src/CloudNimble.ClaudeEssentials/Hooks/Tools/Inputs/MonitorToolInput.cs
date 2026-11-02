using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the Monitor tool, which streams events from a background command or WebSocket.
    /// </summary>
    public class MonitorToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the shell command or script. Each stdout line is an event, and exiting ends the watch.
        /// </summary>
        [JsonPropertyName("command")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Command { get; set; }

        /// <summary>
        /// Gets or sets a short human-readable description of what is being monitored.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the deadline in milliseconds after which the monitor is killed. Deadlines above 1800000 are capped.
        /// </summary>
        [JsonPropertyName("timeout_ms")]
        public long TimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the WebSocket to open. Cannot be combined with <c>command</c>.
        /// </summary>
        [JsonPropertyName("ws")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MonitorWebSocketOptions? WebSocket { get; set; }

        #endregion

    }

}
