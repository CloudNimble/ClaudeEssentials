using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents an asset uploaded to an artifact.
    /// </summary>
    public class ArtifactAssetUpload
    {

        #region Properties

        /// <summary>
        /// Gets or sets the asset's content type.
        /// </summary>
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the asset's file name.
        /// </summary>
        [JsonPropertyName("file_name")]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the asset's ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the SHA-256 hash of the asset.
        /// </summary>
        [JsonPropertyName("sha256")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Sha256 { get; set; }

        /// <summary>
        /// Gets or sets the asset's size in bytes.
        /// </summary>
        [JsonPropertyName("size_bytes")]
        public long SizeBytes { get; set; }

        /// <summary>
        /// Gets or sets the asset's URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        #endregion

    }

}
