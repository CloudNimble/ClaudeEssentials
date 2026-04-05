using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Read tool response for a PDF file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// PDF content is returned as base64-encoded data. For large PDFs (more than 10 pages),
    /// the <c>pages</c> parameter should be used to read specific
    /// page ranges.
    /// </para>
    /// </remarks>
    public class PdfReadToolResponse : ReadToolResponse
    {

        /// <summary>
        /// Gets or sets the PDF file information containing the path, base64 data, and file size.
        /// </summary>
        [JsonPropertyName("file")]
        public PdfReadToolFileInfo File { get; set; } = new();

    }

}
