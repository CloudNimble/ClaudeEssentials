using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the content of a single MCP (Model Context Protocol) resource as returned by the ReadMcpResource tool.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each content entry corresponds to a resource identified by its URI. Text resources include
    /// their content inline, while binary resources are saved to disk and the path is provided.
    /// </para>
    /// <para>
    /// Example JSON:
    /// <code>
    /// {
    ///   "uri": "file:///workspace/config.json",
    ///   "mimeType": "application/json",
    ///   "text": "{ \"key\": \"value\" }"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class McpResourceContent
    {

        /// <summary>
        /// Gets or sets the resource URI.
        /// </summary>
        [JsonPropertyName("uri")]
        public string Uri { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the MIME type of the content.
        /// </summary>
        [JsonPropertyName("mimeType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? MimeType { get; set; }

        /// <summary>
        /// Gets or sets the text content of the resource.
        /// </summary>
        [JsonPropertyName("text")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the path where binary blob content was saved.
        /// </summary>
        /// <remarks>
        /// This is populated instead of <see cref="Text"/> when the resource contains binary data
        /// that cannot be represented as text.
        /// </remarks>
        [JsonPropertyName("blobSavedTo")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BlobSavedTo { get; set; }

    }

}
