using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the Projects tool, which reads and writes documents in a claude.ai project.
    /// </summary>
    public class ProjectsToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the inline document text for write operations. Mutually exclusive with <c>local_path</c>.
        /// </summary>
        [JsonPropertyName("content")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the number of hits for search operations. Defaults to 5.
        /// </summary>
        [JsonPropertyName("n")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? HitCount { get; set; }

        /// <summary>
        /// Gets or sets a file inside the working directory to upload for write operations. Mutually exclusive with <c>content</c>.
        /// </summary>
        [JsonPropertyName("local_path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LocalPath { get; set; }

        /// <summary>
        /// Gets or sets the operation to perform.
        /// </summary>
        [JsonPropertyName("method")]
        public ProjectsMethod Method { get; set; }

        /// <summary>
        /// Gets or sets the document path for read, write, and delete operations, or the memory file path for memory reads.
        /// </summary>
        [JsonPropertyName("path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this document is the file the user needs to see. Applies to write operations and defaults to false.
        /// </summary>
        [JsonPropertyName("present_to_user")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? PresentToUser { get; set; }

        /// <summary>
        /// Gets or sets the knowledge-base query for search operations.
        /// </summary>
        [JsonPropertyName("query")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Query { get; set; }

        #endregion

    }

}
