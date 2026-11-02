using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the Artifact tool, which publishes and manages artifacts hosted on claude.ai.
    /// </summary>
    public class ArtifactToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the action to perform. Defaults to publish.
        /// </summary>
        [JsonPropertyName("action")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactAction? Action { get; set; }

        /// <summary>
        /// Gets or sets the <c>next</c> value from a previous list_assets result.
        /// </summary>
        [JsonPropertyName("after")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? After { get; set; }

        /// <summary>
        /// Gets or sets the asset's ID for read_asset and delete_asset operations.
        /// </summary>
        [JsonPropertyName("asset_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AssetId { get; set; }

        /// <summary>
        /// Gets or sets the runtime capabilities the page declares, as a map of name to configuration.
        /// </summary>
        [JsonPropertyName("capabilities")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, JsonElement>? Capabilities { get; set; }

        /// <summary>
        /// Gets or sets the artifact's runtime version. Omit to keep the current version, use <c>latest</c> to upgrade, or give a specific version to pin.
        /// </summary>
        [JsonPropertyName("contract")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Contract { get; set; }

        /// <summary>
        /// Gets or sets the one-sentence subtitle shown on the gallery card.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the favicon. This value is deprecated: use <c>icon</c> instead.
        /// </summary>
        [JsonPropertyName("favicon")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Favicon { get; set; }

        /// <summary>
        /// Gets or sets the path of the file to publish or upload.
        /// </summary>
        [JsonPropertyName("file_path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FilePath { get; set; }

        /// <summary>
        /// Gets or sets several local files to upload in place of <c>file_path</c>. Between 1 and 25 items.
        /// </summary>
        [JsonPropertyName("file_paths")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? FilePaths { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to overwrite and discard a newer published version as a last resort.
        /// </summary>
        [JsonPropertyName("force")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Force { get; set; }

        /// <summary>
        /// Gets or sets one short generic word for the artifact's browser-tab icon.
        /// </summary>
        [JsonPropertyName("icon")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets a short name for this publish, at most 60 characters.
        /// </summary>
        [JsonPropertyName("label")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Label { get; set; }

        /// <summary>
        /// Gets or sets the maximum artifacts to return for list operations. Defaults to 25.
        /// </summary>
        [JsonPropertyName("limit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Limit { get; set; }

        /// <summary>
        /// Gets or sets the directory to save into for read_asset operations.
        /// </summary>
        [JsonPropertyName("out_dir")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OutDir { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to also pin the published artifact to the user's sidebar.
        /// </summary>
        [JsonPropertyName("pin")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Pin { get; set; }

        /// <summary>
        /// Gets or sets what to extract from an artifact shared with the user. Applies to read operations.
        /// </summary>
        [JsonPropertyName("prompt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Prompt { get; set; }

        /// <summary>
        /// Gets or sets which artifacts to return for list operations.
        /// </summary>
        [JsonPropertyName("scope")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactListScope? Scope { get; set; }

        /// <summary>
        /// Gets or sets the title for the artifact, used only when the file has no title of its own.
        /// </summary>
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the claude.ai link of the existing artifact to act on.
        /// </summary>
        [JsonPropertyName("url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Url { get; set; }

        #endregion

    }

}
