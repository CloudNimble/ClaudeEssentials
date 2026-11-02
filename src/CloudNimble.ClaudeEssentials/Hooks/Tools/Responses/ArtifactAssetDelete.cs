using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the result of the Artifact tool's delete_asset action.
    /// </summary>
    public class ArtifactAssetDelete
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the asset was deleted.
        /// </summary>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the asset's ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        #endregion

    }

}
