using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a project memory file.
    /// </summary>
    public class ProjectMemoryFileInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the memory file path.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file size in bytes.
        /// </summary>
        [JsonPropertyName("size_bytes")]
        public long SizeBytes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the file was truncated.
        /// </summary>
        [JsonPropertyName("truncated")]
        public bool Truncated { get; set; }

        /// <summary>
        /// Gets or sets the last update timestamp, which may be null.
        /// </summary>
        [JsonPropertyName("updated_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? UpdatedAt { get; set; }

        #endregion

    }

}
