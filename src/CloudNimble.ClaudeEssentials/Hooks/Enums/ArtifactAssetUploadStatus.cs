using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the outcome of uploading one file to an artifact's asset store.
    /// </summary>
    public enum ArtifactAssetUploadStatus
    {

        /// <summary>
        /// The file was uploaded.
        /// </summary>
        [JsonStringEnumMemberName("uploaded")]
        Uploaded,

        /// <summary>
        /// The upload failed.
        /// </summary>
        [JsonStringEnumMemberName("failed")]
        Failed,

        /// <summary>
        /// The upload was not attempted.
        /// </summary>
        [JsonStringEnumMemberName("not_attempted")]
        NotAttempted

    }

}
