using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the outcome of refreshing the tool list of an MCP server.
    /// </summary>
    public enum RefreshMcpToolsStatus
    {

        /// <summary>
        /// The tool list was re-queried and applied.
        /// </summary>
        [JsonStringEnumMemberName("refreshed")]
        Refreshed,

        /// <summary>
        /// The re-query failed and the previous tool set was kept.
        /// </summary>
        [JsonStringEnumMemberName("error")]
        Error,

        /// <summary>
        /// The server has no live connection to query. This tool never dials.
        /// </summary>
        [JsonStringEnumMemberName("not_connected")]
        NotConnected

    }

}
