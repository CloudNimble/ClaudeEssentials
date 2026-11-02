using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the usage and limits of an artifact's asset store.
    /// </summary>
    public class ArtifactAssetUsage
    {

        #region Properties

        /// <summary>
        /// Gets or sets the number of bytes stored.
        /// </summary>
        [JsonPropertyName("bytes")]
        public long Bytes { get; set; }

        /// <summary>
        /// Gets or sets the number of files stored.
        /// </summary>
        [JsonPropertyName("files")]
        public int Files { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of bytes.
        /// </summary>
        [JsonPropertyName("max_bytes")]
        public long MaxBytes { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of files.
        /// </summary>
        [JsonPropertyName("max_files")]
        public int MaxFiles { get; set; }

        #endregion

    }

}
