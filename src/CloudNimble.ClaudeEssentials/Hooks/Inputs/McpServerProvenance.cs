using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the MCP server that provides a tool.
    /// </summary>
    public class McpServerProvenance
    {

        #region Properties

        /// <summary>
        /// Gets or sets the server name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the configured source of the server. This is an open set, so treat unknown values as an unrecognized source.
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;

        #endregion

    }

}
