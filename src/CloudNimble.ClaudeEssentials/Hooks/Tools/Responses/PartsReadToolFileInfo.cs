using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Contains information about a PDF file read in parts by the Claude Code Read tool,
    /// where individual pages are extracted as images.
    /// </summary>
    public class PartsReadToolFileInfo
    {

        /// <summary>
        /// Gets or sets the path to the PDF file.
        /// </summary>
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the original file size in bytes.
        /// </summary>
        [JsonPropertyName("originalSize")]
        public int OriginalSize { get; set; }

        /// <summary>
        /// Gets or sets the number of pages extracted.
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the directory containing extracted page images.
        /// </summary>
        [JsonPropertyName("outputDir")]
        public string OutputDir { get; set; } = string.Empty;

    }

}
