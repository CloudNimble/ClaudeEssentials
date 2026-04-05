using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Claude Code ReadMcpResource tool
    /// after reading the content of an MCP (Model Context Protocol) resource.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The response contains one or more content entries for the requested resource.
    /// Text resources include their content inline, while binary resources are saved
    /// to disk and the path is provided via <see cref="McpResourceContent.BlobSavedTo"/>.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "contents": [
    ///     {
    ///       "uri": "file:///workspace/config.json",
    ///       "mimeType": "application/json",
    ///       "text": "{ \"key\": \"value\" }"
    ///     }
    ///   ]
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class ReadMcpResourceToolResponse
    {

        /// <summary>
        /// Gets or sets the list of resource content entries.
        /// </summary>
        [JsonPropertyName("contents")]
        public List<McpResourceContent> Contents { get; set; } = [];

    }

}
