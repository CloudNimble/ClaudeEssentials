using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a project knowledge-base search hit.
    /// </summary>
    public class ProjectSearchHit
    {

        #region Properties

        /// <summary>
        /// Gets or sets the identifier of the matching document.
        /// </summary>
        [JsonPropertyName("doc_uuid")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DocUuid { get; set; }

        /// <summary>
        /// Gets or sets the name of the matching document.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the matching text.
        /// </summary>
        [JsonPropertyName("text")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; set; }

        #endregion

    }

}
