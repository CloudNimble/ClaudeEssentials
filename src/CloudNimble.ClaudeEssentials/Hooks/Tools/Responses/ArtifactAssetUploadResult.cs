using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the result of uploading one file in a batch.
    /// </summary>
    /// <remarks>
    /// The asset properties are set when <c>status</c> is uploaded, and <c>reason</c> and <c>message</c> are set otherwise.
    /// </remarks>
    public class ArtifactAssetUploadResult
    {

        #region Properties

        /// <summary>
        /// Gets or sets the asset's content type.
        /// </summary>
        [JsonPropertyName("content_type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ContentType { get; set; }

        /// <summary>
        /// Gets or sets the asset's file name.
        /// </summary>
        [JsonPropertyName("file_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the local file path.
        /// </summary>
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the asset's ID.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the file may have been stored despite the failure.
        /// </summary>
        [JsonPropertyName("may_be_stored")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? MayBeStored { get; set; }

        /// <summary>
        /// Gets or sets the message describing the failure.
        /// </summary>
        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets why the file was not uploaded.
        /// </summary>
        [JsonPropertyName("reason")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Reason { get; set; }

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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? SizeBytes { get; set; }

        /// <summary>
        /// Gets or sets the outcome of the upload.
        /// </summary>
        [JsonPropertyName("status")]
        public ArtifactAssetUploadStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the asset's URL.
        /// </summary>
        [JsonPropertyName("url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Url { get; set; }

        #endregion

    }

}
