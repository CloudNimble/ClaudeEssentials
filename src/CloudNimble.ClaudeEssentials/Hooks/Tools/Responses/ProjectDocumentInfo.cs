using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a document in a project.
    /// </summary>
    public class ProjectDocumentInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the creation timestamp, which may be null.
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the document path.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        #endregion

    }

}
