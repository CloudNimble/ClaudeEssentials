using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the result of the Artifact tool's pin and unpin actions.
    /// </summary>
    public class ArtifactPinResult
    {

        #region Properties

        /// <summary>
        /// Gets or sets the operation that was performed.
        /// </summary>
        [JsonPropertyName("action")]
        public ArtifactPinAction Action { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the artifact is now pinned.
        /// </summary>
        [JsonPropertyName("pinned")]
        public bool Pinned { get; set; }

        /// <summary>
        /// Gets or sets the artifact's title.
        /// </summary>
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the artifact's URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        #endregion

    }

}
