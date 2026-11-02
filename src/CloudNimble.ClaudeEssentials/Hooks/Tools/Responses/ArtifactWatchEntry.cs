using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents one watch returned by the Artifact tool's status action.
    /// </summary>
    /// <remarks>
    /// The schema defines three shapes, identified by <c>rail</c>. A live watch has no rail, and <c>since</c> is an epoch number on live watches and a timestamp string on durable ones.
    /// </remarks>
    public class ArtifactWatchEntry
    {

        #region Properties

        /// <summary>
        /// Gets or sets how the watch was armed.
        /// </summary>
        [JsonPropertyName("armed_via")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ArmedVia { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a live watch is connected.
        /// </summary>
        [JsonPropertyName("connected")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Connected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a live watch is connecting.
        /// </summary>
        [JsonPropertyName("connecting")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Connecting { get; set; }

        /// <summary>
        /// Gets or sets the events being watched.
        /// </summary>
        [JsonPropertyName("events")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Events { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the watch was requested explicitly.
        /// </summary>
        [JsonPropertyName("explicit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Explicit { get; set; }

        /// <summary>
        /// Gets or sets the delivery rail. Absent on a live watch.
        /// </summary>
        [JsonPropertyName("rail")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactWatchRail? Rail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a durable watch was restored.
        /// </summary>
        [JsonPropertyName("restored")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Restored { get; set; }

        /// <summary>
        /// Gets or sets when the watch started, as an epoch number or a timestamp string depending on the rail.
        /// </summary>
        [JsonPropertyName("since")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? Since { get; set; }

        /// <summary>
        /// Gets or sets why a live watch stopped.
        /// </summary>
        [JsonPropertyName("stop_kind")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StopKind { get; set; }

        /// <summary>
        /// Gets or sets the ID of the background task delivering a live watch.
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

        #endregion

    }

}
