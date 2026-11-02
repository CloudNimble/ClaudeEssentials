using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a file in a project.
    /// </summary>
    public class ProjectFileInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the creation timestamp, which may be null.
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the kind of file.
        /// </summary>
        [JsonPropertyName("file_kind")]
        public string FileKind { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        #endregion

    }

}
