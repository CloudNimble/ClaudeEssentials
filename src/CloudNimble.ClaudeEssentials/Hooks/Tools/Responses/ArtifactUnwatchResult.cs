using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the result of the Artifact tool's unwatch action.
    /// </summary>
    public class ArtifactUnwatchResult
    {

        #region Properties

        /// <summary>
        /// Gets or sets the artifact's URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the artifact was being watched.
        /// </summary>
        [JsonPropertyName("was_watching")]
        public bool WasWatching { get; set; }

        #endregion

    }

}
