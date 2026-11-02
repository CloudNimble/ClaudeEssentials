using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the result of the Artifact tool's watch action.
    /// </summary>
    public class ArtifactWatchResult
    {

        #region Properties

        /// <summary>
        /// Gets or sets further detail about the outcome.
        /// </summary>
        [JsonPropertyName("detail")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Detail { get; set; }

        /// <summary>
        /// Gets or sets when the durable watch started.
        /// </summary>
        [JsonPropertyName("durable_since")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DurableSince { get; set; }

        /// <summary>
        /// Gets or sets why a durable watch was skipped.
        /// </summary>
        [JsonPropertyName("durable_skip_reason")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DurableSkipReason { get; set; }

        /// <summary>
        /// Gets or sets the events being watched.
        /// </summary>
        [JsonPropertyName("events")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Events { get; set; }

        /// <summary>
        /// Gets or sets a note about the watch.
        /// </summary>
        [JsonPropertyName("note")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Note { get; set; }

        /// <summary>
        /// Gets or sets the outcome of the request.
        /// </summary>
        [JsonPropertyName("outcome")]
        public string Outcome { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the delivery rail.
        /// </summary>
        [JsonPropertyName("rail")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Rail { get; set; }

        /// <summary>
        /// Gets or sets the reason for the outcome.
        /// </summary>
        [JsonPropertyName("reason")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets when the live watch started, in epoch milliseconds.
        /// </summary>
        [JsonPropertyName("since")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? Since { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status of the request.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets the ID of the background task delivering the watch.
        /// </summary>
        [JsonPropertyName("task_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TaskId { get; set; }

        /// <summary>
        /// Gets or sets when the watch token expires, in epoch milliseconds.
        /// </summary>
        [JsonPropertyName("token_expires_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? TokenExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets the ID of the durable trigger.
        /// </summary>
        [JsonPropertyName("trigger_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TriggerId { get; set; }

        /// <summary>
        /// Gets or sets the artifact's URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the artifact is now being watched.
        /// </summary>
        [JsonPropertyName("watching")]
        public bool Watching { get; set; }

        #endregion

    }

}
