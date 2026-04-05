using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a single MCP (Model Context Protocol) resource entry returned by the ListMcpResources tool.
    /// </summary>
    /// <remarks>
    /// <para>
    /// MCP resources are data endpoints exposed by MCP servers that Claude can read to gain context.
    /// Each resource has a URI that uniquely identifies it and is served by a specific MCP server.
    /// </para>
    /// <para>
    /// Example JSON:
    /// <code>
    /// {
    ///   "uri": "file:///workspace/config.json",
    ///   "name": "config.json",
    ///   "mimeType": "application/json",
    ///   "description": "Project configuration file",
    ///   "server": "my-mcp-server"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class McpResourceInfo
    {

        /// <summary>
        /// Gets or sets the resource URI.
        /// </summary>
        [JsonPropertyName("uri")]
        public string Uri { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the resource name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the MIME type of the resource.
        /// </summary>
        [JsonPropertyName("mimeType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? MimeType { get; set; }

        /// <summary>
        /// Gets or sets the resource description.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the MCP server that provides this resource.
        /// </summary>
        [JsonPropertyName("server")]
        public string Server { get; set; } = string.Empty;

    }

}
