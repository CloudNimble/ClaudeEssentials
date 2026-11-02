using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Projects tool response for the <c>project_write</c> method.
    /// </summary>
    public class WriteProjectsToolResponse : ProjectsToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        [JsonPropertyName("doc_uuid")]
        public string DocUuid { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the local file that was uploaded.
        /// </summary>
        [JsonPropertyName("local_path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LocalPath { get; set; }

        /// <summary>
        /// Gets or sets the document path.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the document was marked as the file the user needs to see.
        /// </summary>
        [JsonPropertyName("present_to_user")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? PresentToUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an existing document was replaced.
        /// </summary>
        [JsonPropertyName("replaced")]
        public bool Replaced { get; set; }

        #endregion

    }

}
