using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents an artifact returned by the Artifact tool's list action.
    /// </summary>
    public class ArtifactListItem
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the artifact is from another organization.
        /// </summary>
        [JsonPropertyName("external")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? External { get; set; }

        /// <summary>
        /// Gets or sets the artifact's favicon.
        /// </summary>
        [JsonPropertyName("favicon")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Favicon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether people outside the organization may have written to the artifact.
        /// </summary>
        [JsonPropertyName("outsidersMayHaveWritten")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? OutsidersMayHaveWritten { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the artifact is pinned.
        /// </summary>
        [JsonPropertyName("pinned")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Pinned { get; set; }

        /// <summary>
        /// Gets or sets how the artifact relates to the user.
        /// </summary>
        [JsonPropertyName("rel")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactRelationship? Rel { get; set; }

        /// <summary>
        /// Gets or sets the user's role on the artifact.
        /// </summary>
        [JsonPropertyName("role")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactRole? Role { get; set; }

        /// <summary>
        /// Gets or sets the artifact's title.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets when the artifact was last updated.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the artifact's URL.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        #endregion

    }

}
