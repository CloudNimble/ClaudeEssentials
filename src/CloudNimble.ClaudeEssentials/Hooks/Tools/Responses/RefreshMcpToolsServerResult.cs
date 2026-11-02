using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the result of refreshing the tool list of a single MCP server.
    /// </summary>
    public class RefreshMcpToolsServerResult
    {

        #region Properties

        /// <summary>
        /// Gets or sets the tool names this refresh added.
        /// </summary>
        [JsonPropertyName("added")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Added { get; set; }

        /// <summary>
        /// Gets or sets why the refresh failed or the server was unavailable.
        /// </summary>
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the tool names this refresh removed.
        /// </summary>
        [JsonPropertyName("removed")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Removed { get; set; }

        /// <summary>
        /// Gets or sets the server name.
        /// </summary>
        [JsonPropertyName("server")]
        public string Server { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the outcome of the refresh.
        /// </summary>
        [JsonPropertyName("status")]
        public RefreshMcpToolsStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the number of tools now available from this server.
        /// </summary>
        [JsonPropertyName("toolCount")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ToolCount { get; set; }

        #endregion

    }

}
