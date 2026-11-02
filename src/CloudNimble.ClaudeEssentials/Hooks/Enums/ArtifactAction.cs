using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the action performed by the Artifact tool.
    /// </summary>
    public enum ArtifactAction
    {

        /// <summary>
        /// Publishes a file. This is the default action.
        /// </summary>
        [JsonStringEnumMemberName("publish")]
        Publish,

        /// <summary>
        /// Lists artifacts.
        /// </summary>
        [JsonStringEnumMemberName("list")]
        List,

        /// <summary>
        /// Reads a published artifact.
        /// </summary>
        [JsonStringEnumMemberName("read")]
        Read,

        /// <summary>
        /// Lists the available artifact types.
        /// </summary>
        [JsonStringEnumMemberName("list_types")]
        ListTypes,

        /// <summary>
        /// Starts watching an artifact for new versions.
        /// </summary>
        [JsonStringEnumMemberName("watch")]
        Watch,

        /// <summary>
        /// Stops watching an artifact.
        /// </summary>
        [JsonStringEnumMemberName("unwatch")]
        Unwatch,

        /// <summary>
        /// Reports the artifact watches of this session.
        /// </summary>
        [JsonStringEnumMemberName("status")]
        Status,

        /// <summary>
        /// Uploads files to an artifact's asset store.
        /// </summary>
        [JsonStringEnumMemberName("upload_asset")]
        UploadAsset,

        /// <summary>
        /// Lists the files in an artifact's asset store.
        /// </summary>
        [JsonStringEnumMemberName("list_assets")]
        ListAssets,

        /// <summary>
        /// Saves an asset to a local file.
        /// </summary>
        [JsonStringEnumMemberName("read_asset")]
        ReadAsset,

        /// <summary>
        /// Permanently removes an asset.
        /// </summary>
        [JsonStringEnumMemberName("delete_asset")]
        DeleteAsset,

        /// <summary>
        /// Adds an artifact to the user's pinned list.
        /// </summary>
        [JsonStringEnumMemberName("pin")]
        Pin,

        /// <summary>
        /// Removes an artifact from the user's pinned list.
        /// </summary>
        [JsonStringEnumMemberName("unpin")]
        Unpin

    }

}
