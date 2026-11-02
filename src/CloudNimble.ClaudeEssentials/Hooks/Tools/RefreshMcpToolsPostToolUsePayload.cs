using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using System.Collections.Generic;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the RefreshMcpTools tool has executed.
    /// </summary>
    /// <remarks>
    /// The RefreshMcpTools tool re-queries the tool lists of connected MCP servers. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeRefreshMcpToolsPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class RefreshMcpToolsPostToolUsePayload : PostToolUseHookInput<RefreshMcpToolsToolInput, List<RefreshMcpToolsServerResult>>
    {
    }

}
