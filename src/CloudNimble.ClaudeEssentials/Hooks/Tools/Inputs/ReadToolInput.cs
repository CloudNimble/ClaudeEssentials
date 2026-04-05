using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
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
        /// Gets or sets the page range for PDF files.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Optional. Only applicable to PDF files. Specifies which pages to read,
        /// using formats like <c>"1-5"</c>, <c>"3"</c>, or <c>"10-20"</c>.
        /// </para>
        /// <para>
        /// For large PDFs (more than 10 pages), a page range must be provided.
        /// Maximum 20 pages per request.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// "1-5"
        /// </code>
        /// </example>
        [JsonPropertyName("pages")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Pages { get; set; }

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
