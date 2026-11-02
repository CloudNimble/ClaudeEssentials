using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ReadMcpResourceDir tool, which lists the children of a directory resource on an MCP server.
    /// </summary>
    public class ReadMcpResourceDirToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the MCP server name.
        /// </summary>
        [JsonPropertyName("server")]
        public string Server { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the directory resource URI to list.
        /// </summary>
        [JsonPropertyName("uri")]
        public string Uri { get; set; } = string.Empty;

        #endregion

    }

}
