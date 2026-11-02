using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the content returned by the Artifact tool's read action.
    /// </summary>
    public class ArtifactReadResult
    {

        #region Properties

        /// <summary>
        /// Gets or sets the content size in bytes.
        /// </summary>
        [JsonPropertyName("bytes")]
        public long Bytes { get; set; }

        /// <summary>
        /// Gets or sets the HTTP response code.
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the HTTP response code text.
        /// </summary>
        [JsonPropertyName("codeText")]
        public string CodeText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the time taken in milliseconds.
        /// </summary>
        [JsonPropertyName("durationMs")]
        public int DurationMs { get; set; }

        /// <summary>
        /// Gets or sets the content that was read.
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; } = string.Empty;

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
