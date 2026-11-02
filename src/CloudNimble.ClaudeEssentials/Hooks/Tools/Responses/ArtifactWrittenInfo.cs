using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the artifact written by an asset action.
    /// </summary>
    public class ArtifactWrittenInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the artifact's URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        #endregion

    }

}
