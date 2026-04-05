using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Contains information about a PDF file read by the Claude Code Read tool,
    /// including base64-encoded data and file size.
    /// </summary>
    public class PdfReadToolFileInfo
    {

        /// <summary>
        /// Gets or sets the path to the PDF file.
        /// </summary>
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the base64-encoded PDF data.
        /// </summary>
        [JsonPropertyName("base64")]
        public string Base64 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the original file size in bytes.
        /// </summary>
        [JsonPropertyName("originalSize")]
        public int OriginalSize { get; set; }

    }

}
