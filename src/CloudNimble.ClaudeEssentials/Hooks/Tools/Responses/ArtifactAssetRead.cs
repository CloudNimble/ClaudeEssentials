using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the result of the Artifact tool's read_asset action.
    /// </summary>
    public class ArtifactAssetRead
    {

        #region Properties

        /// <summary>
        /// Gets or sets the asset's content type.
        /// </summary>
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether other people have written to the store.
        /// </summary>
        [JsonPropertyName("cowritten")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Cowritten { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the asset belongs to an artifact the user does not own.
        /// </summary>
        [JsonPropertyName("foreign")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Foreign { get; set; }

        /// <summary>
        /// Gets or sets the asset's ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether someone outside the organization has written to the store.
        /// </summary>
        [JsonPropertyName("outside_writer")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? OutsideWriter { get; set; }

        /// <summary>
        /// Gets or sets the local path the asset was saved to.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the asset was read through a public path.
        /// </summary>
        [JsonPropertyName("public_read")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? PublicRead { get; set; }

        /// <summary>
        /// Gets or sets the SHA-256 hash of the asset.
        /// </summary>
        [JsonPropertyName("sha256")]
        public string Sha256 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the asset's size in bytes.
        /// </summary>
        [JsonPropertyName("size_bytes")]
        public long SizeBytes { get; set; }

        #endregion

    }

}
