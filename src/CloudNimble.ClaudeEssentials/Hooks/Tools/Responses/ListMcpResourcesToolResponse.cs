using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Claude Code ListMcpResources tool
    /// after listing available MCP (Model Context Protocol) resources.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ListMcpResources tool returns an array of available resources from MCP servers.
    /// This wrapper class contains the list of <see cref="McpResourceInfo"/> items.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// [
    ///   {
    ///     "uri": "file:///workspace/config.json",
    ///     "name": "config.json",
    ///     "mimeType": "application/json",
    ///     "description": "Project configuration file",
    ///     "server": "my-mcp-server"
    ///   }
    /// ]
    /// </code>
    /// </para>
    /// </remarks>
    [Obsolete("ListMcpResourcesToolResponse is no longer needed. The Claude Code response is a bare array of McpResourceInfo items. Use List<McpResourceInfo> directly.")]
    public class ListMcpResourcesToolResponse
    {

        /// <summary>
        /// Gets or sets the list of available MCP resources.
        /// </summary>
        [JsonPropertyName("resources")]
        public List<McpResourceInfo> Resources { get; set; } = [];

    }

}
