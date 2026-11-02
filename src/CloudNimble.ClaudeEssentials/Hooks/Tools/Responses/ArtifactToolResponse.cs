using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Artifact tool.
    /// </summary>
    /// <remarks>
    /// The schema defines one untagged shape per action, each identified by which keys are present and with no key shared in conflicting ways. This type flattens those shapes, so only the properties for the action that ran are populated.
    /// </remarks>
    public class ArtifactToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the state of each watch connection.
        /// </summary>
        [JsonPropertyName("arms")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ArtifactWatchArm>? Arms { get; set; }

        /// <summary>
        /// Gets or sets the artifact's ID.
        /// </summary>
        [JsonPropertyName("artifact_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ArtifactId { get; set; }

        /// <summary>
        /// Gets or sets the artifact and version that now count as viewed.
        /// </summary>
        [JsonPropertyName("artifactRead")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactReadInfo? ArtifactRead { get; set; }

        /// <summary>
        /// Gets or sets the artifacts returned by a list action.
        /// </summary>
        [JsonPropertyName("artifacts")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ArtifactListItem>? Artifacts { get; set; }

        /// <summary>
        /// Gets or sets the result of a delete_asset action.
        /// </summary>
        [JsonPropertyName("asset_delete")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactAssetDelete? AssetDelete { get; set; }

        /// <summary>
        /// Gets or sets the result of a list_assets action.
        /// </summary>
        [JsonPropertyName("asset_list")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactAssetList? AssetList { get; set; }

        /// <summary>
        /// Gets or sets the result of a read_asset action.
        /// </summary>
        [JsonPropertyName("asset_read")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactAssetRead? AssetRead { get; set; }

        /// <summary>
        /// Gets or sets the result of uploading one asset.
        /// </summary>
        [JsonPropertyName("asset_upload")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactAssetUpload? AssetUpload { get; set; }

        /// <summary>
        /// Gets or sets the result of uploading several assets.
        /// </summary>
        [JsonPropertyName("asset_uploads")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactAssetUploadBatch? AssetUploads { get; set; }

        /// <summary>
        /// Gets or sets the artifact's audience.
        /// </summary>
        [JsonPropertyName("audience")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Audience { get; set; }

        /// <summary>
        /// Gets or sets the capabilities declared by this publish.
        /// </summary>
        [JsonPropertyName("capabilities")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? Capabilities { get; set; }

        /// <summary>
        /// Gets or sets the artifact's runtime version.
        /// </summary>
        [JsonPropertyName("contract")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Contract { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether artifacts from other organizations were listed.
        /// </summary>
        [JsonPropertyName("external_listed")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? ExternalListed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a favicon was sent.
        /// </summary>
        [JsonPropertyName("faviconSent")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? FaviconSent { get; set; }

        /// <summary>
        /// Gets or sets the URL a status action was filtered to.
        /// </summary>
        [JsonPropertyName("filter_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FilterUrl { get; set; }

        /// <summary>
        /// Gets or sets the artifact's icon.
        /// </summary>
        [JsonPropertyName("icon")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the icon was dropped.
        /// </summary>
        [JsonPropertyName("iconDropped")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IconDropped { get; set; }

        /// <summary>
        /// Gets or sets the live subscription state.
        /// </summary>
        [JsonPropertyName("liveSubscription")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LiveSubscription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user owns the artifact.
        /// </summary>
        [JsonPropertyName("owned")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Owned { get; set; }

        /// <summary>
        /// Gets or sets the path of the file that was published.
        /// </summary>
        [JsonPropertyName("path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the result of a pin or unpin action.
        /// </summary>
        [JsonPropertyName("pin")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactPinResult? Pin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the artifact is pinned.
        /// </summary>
        [JsonPropertyName("pinned")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Pinned { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pinning is available.
        /// </summary>
        [JsonPropertyName("pins_enabled")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? PinsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the publishes remaining in the current window.
        /// </summary>
        [JsonPropertyName("publishesRemaining")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? PublishesRemaining { get; set; }

        /// <summary>
        /// Gets or sets when the publish window resets.
        /// </summary>
        [JsonPropertyName("publishesResetAt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? PublishesResetAt { get; set; }

        /// <summary>
        /// Gets or sets the result of a read action.
        /// </summary>
        [JsonPropertyName("read")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactReadResult? Read { get; set; }

        /// <summary>
        /// Gets or sets the scope of the listing.
        /// </summary>
        [JsonPropertyName("scope")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactListScope? Scope { get; set; }

        /// <summary>
        /// Gets or sets the publish sequence number.
        /// </summary>
        [JsonPropertyName("seq")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? Seq { get; set; }

        /// <summary>
        /// Gets or sets the capability declaration stored on the artifact.
        /// </summary>
        [JsonPropertyName("stored")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactStoredDeclaration? Stored { get; set; }

        /// <summary>
        /// Gets or sets the artifact's title.
        /// </summary>
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the total number of artifacts.
        /// </summary>
        [JsonPropertyName("total")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Total { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the total is a lower bound.
        /// </summary>
        [JsonPropertyName("total_at_least")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? TotalAtLeast { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the list was truncated.
        /// </summary>
        [JsonPropertyName("truncated")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Truncated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the content was unchanged.
        /// </summary>
        [JsonPropertyName("unchanged")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Unchanged { get; set; }

        /// <summary>
        /// Gets or sets the result of an unwatch action.
        /// </summary>
        [JsonPropertyName("unwatch")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactUnwatchResult? Unwatch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an existing artifact was updated.
        /// </summary>
        [JsonPropertyName("updated")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Updated { get; set; }

        /// <summary>
        /// Gets or sets the published artifact's URL.
        /// </summary>
        [JsonPropertyName("url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the published version.
        /// </summary>
        [JsonPropertyName("version")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Version { get; set; }

        /// <summary>
        /// Gets or sets any warnings from the publish.
        /// </summary>
        [JsonPropertyName("warnings")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Warnings { get; set; }

        /// <summary>
        /// Gets or sets the result of a watch action.
        /// </summary>
        [JsonPropertyName("watch")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactWatchResult? Watch { get; set; }

        /// <summary>
        /// Gets or sets the watches returned by a status action.
        /// </summary>
        [JsonPropertyName("watches")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ArtifactWatchEntry>? Watches { get; set; }

        /// <summary>
        /// Gets or sets the artifact written by an asset action.
        /// </summary>
        [JsonPropertyName("written")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactWrittenInfo? Written { get; set; }

        #endregion

    }

}
