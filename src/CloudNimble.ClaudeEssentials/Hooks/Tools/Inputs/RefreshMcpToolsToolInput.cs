using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the RefreshMcpTools tool, which re-queries the tool lists of connected MCP servers.
    /// </summary>
    public class RefreshMcpToolsToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the optional server name. Omit to refresh all connected servers.
        /// </summary>
        [JsonPropertyName("server")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Server { get; set; }

        #endregion

    }

}
