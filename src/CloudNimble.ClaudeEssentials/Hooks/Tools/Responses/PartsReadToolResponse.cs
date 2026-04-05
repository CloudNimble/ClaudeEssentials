using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Read tool response for a PDF file that was split into individual page images.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When a PDF is too large to return as a single base64 blob, it may be split into
    /// individual page images stored in an output directory.
    /// </para>
    /// </remarks>
    public class PartsReadToolResponse : ReadToolResponse
    {

        /// <summary>
        /// Gets or sets the file information containing the path, page count, and output directory.
        /// </summary>
        [JsonPropertyName("file")]
        public PartsReadToolFileInfo File { get; set; } = new();

    }

}
