using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for an MCP (Model Context Protocol) tool execution.
    /// MCP tool inputs are dynamic and vary based on the specific MCP server and tool being invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Because MCP tools are defined by external servers, the input schema is not known at compile time.
    /// This class uses a dictionary of <see cref="JsonElement"/> values to represent the arbitrary
    /// key-value pairs that make up the tool input.
    /// </para>
    /// <para>
    /// Example JSON:
    /// <code>
    /// {
    ///   "server": "my-mcp-server",
    ///   "tool": "search_docs",
    ///   "query": "authentication"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class McpToolInput
    {

        /// <summary>
        /// Gets or sets the dynamic key-value pairs that make up the MCP tool input.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? ExtensionData { get; set; }

    }

}
