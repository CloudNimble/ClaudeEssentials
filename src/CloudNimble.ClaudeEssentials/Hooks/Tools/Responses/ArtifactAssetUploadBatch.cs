using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the result of uploading several assets to an artifact.
    /// </summary>
    public class ArtifactAssetUploadBatch
    {

        #region Properties

        /// <summary>
        /// Gets or sets the result for each file. Between 1 and 25 items.
        /// </summary>
        [JsonPropertyName("results")]
        public List<ArtifactAssetUploadResult> Results { get; set; } = [];

        /// <summary>
        /// Gets or sets the artifact's URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        #endregion

    }

}
