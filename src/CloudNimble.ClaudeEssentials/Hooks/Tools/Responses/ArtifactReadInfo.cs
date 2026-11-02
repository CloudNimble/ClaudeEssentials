using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents an artifact source and the version of it that now counts as viewed.
    /// </summary>
    public class ArtifactReadInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the entry was seeded at startup.
        /// </summary>
        [JsonPropertyName("seeded")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Seeded { get; set; }

        /// <summary>
        /// Gets or sets the artifact's slug.
        /// </summary>
        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the version that was viewed.
        /// </summary>
        [JsonPropertyName("ver")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Ver { get; set; }

        #endregion

    }

}
