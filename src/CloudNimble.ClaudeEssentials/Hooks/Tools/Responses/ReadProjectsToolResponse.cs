using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Projects tool response for the <c>project_read</c> method.
    /// </summary>
    public class ReadProjectsToolResponse : ProjectsToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the document content.
        /// </summary>
        [JsonPropertyName("content")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the creation timestamp, which may be null.
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the kind of file that was read.
        /// </summary>
        [JsonPropertyName("file_kind")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FileKind { get; set; }

        /// <summary>
        /// Gets or sets the local file holding the content.
        /// </summary>
        [JsonPropertyName("local_file")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LocalFile { get; set; }

        /// <summary>
        /// Gets or sets the document path.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        #endregion

    }

}
