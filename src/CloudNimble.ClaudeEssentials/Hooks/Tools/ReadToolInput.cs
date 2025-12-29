using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the input parameters for the Read tool.
    /// The Read tool reads files from the local filesystem.
    /// </summary>
    /// <remarks>
    /// <para>
    /// By default, reads up to 2000 lines starting from the beginning of the file.
    /// You can optionally specify a line offset and limit for reading specific portions.
    /// </para>
    /// <para>
    /// The Read tool can also read images (PNG, JPG, etc.), PDF files, and Jupyter notebooks (.ipynb).
    /// </para>
    /// </remarks>
    public class ReadToolInput
    {

        /// <summary>
        /// Gets or sets the absolute path to the file to read.
        /// </summary>
        /// <remarks>
        /// Must be an absolute path, not a relative path.
        /// </remarks>
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the line number to start reading from.
        /// </summary>
        /// <remarks>
        /// Optional. Only provide if the file is too large to read at once.
        /// </remarks>
        [JsonPropertyName("offset")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Offset { get; set; }

        /// <summary>
        /// Gets or sets the number of lines to read.
        /// </summary>
        /// <remarks>
        /// Optional. Only provide if the file is too large to read at once.
        /// </remarks>
        [JsonPropertyName("limit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Limit { get; set; }

    }

}
