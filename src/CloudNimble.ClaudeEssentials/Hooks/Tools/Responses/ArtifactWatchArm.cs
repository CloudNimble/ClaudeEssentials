using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the connection state of an artifact watch.
    /// </summary>
    public class ArtifactWatchArm
    {

        #region Properties

        /// <summary>
        /// Gets or sets when the state was recorded, in epoch milliseconds.
        /// </summary>
        [JsonPropertyName("at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? At { get; set; }

        /// <summary>
        /// Gets or sets further detail about the state.
        /// </summary>
        [JsonPropertyName("detail")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Detail { get; set; }

        /// <summary>
        /// Gets or sets the number of failures so far.
        /// </summary>
        [JsonPropertyName("failures")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Failures { get; set; }

        /// <summary>
        /// Gets or sets the most recent failure.
        /// </summary>
        [JsonPropertyName("last_failure")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LastFailure { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of failures allowed.
        /// </summary>
        [JsonPropertyName("max_failures")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? MaxFailures { get; set; }

        /// <summary>
        /// Gets or sets the seconds until the next attempt.
        /// </summary>
        [JsonPropertyName("next_in_s")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? NextInS { get; set; }

        /// <summary>
        /// Gets or sets the delivery rail.
        /// </summary>
        [JsonPropertyName("rail")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Rail { get; set; }

        /// <summary>
        /// Gets or sets the reason for the state.
        /// </summary>
        [JsonPropertyName("reason")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the connection will reconnect.
        /// </summary>
        [JsonPropertyName("reconnect")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Reconnect { get; set; }

        /// <summary>
        /// Gets or sets the message from the server.
        /// </summary>
        [JsonPropertyName("server_message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ServerMessage { get; set; }

        /// <summary>
        /// Gets or sets the connection state.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the artifact's URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        #endregion

    }

}
