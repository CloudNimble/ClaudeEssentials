using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the result of the Artifact tool's list_assets action.
    /// </summary>
    public class ArtifactAssetList
    {

        #region Properties

        /// <summary>
        /// Gets or sets the assets in the store. At most 1000 items.
        /// </summary>
        [JsonPropertyName("assets")]
        public List<ArtifactAssetInfo> Assets { get; set; } = [];

        /// <summary>
        /// Gets or sets a value indicating whether other people have written to the store.
        /// </summary>
        [JsonPropertyName("cowritten")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Cowritten { get; set; }

        /// <summary>
        /// Gets or sets the value to pass as <c>after</c> to continue the listing.
        /// </summary>
        [JsonPropertyName("next")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Next { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether someone outside the organization has written to the store.
        /// </summary>
        [JsonPropertyName("outside_writer")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? OutsideWriter { get; set; }

        /// <summary>
        /// Gets or sets the artifact's URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the asset store's usage and limits.
        /// </summary>
        [JsonPropertyName("usage")]
        public ArtifactAssetUsage Usage { get; set; } = new();

        #endregion

    }

}
