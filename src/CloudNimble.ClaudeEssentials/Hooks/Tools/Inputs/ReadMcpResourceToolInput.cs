using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ReadMcpResource tool.
    /// The ReadMcpResource tool reads the content of a specific MCP (Model Context Protocol) resource.
    /// </summary>
    public class ReadMcpResourceToolInput
    {

        /// <summary>
        /// Gets or sets the MCP server name.
        /// </summary>
        [JsonPropertyName("server")]
        public string Server { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the resource URI to read.
        /// </summary>
        [JsonPropertyName("uri")]
        public string Uri { get; set; } = string.Empty;

    }

}
