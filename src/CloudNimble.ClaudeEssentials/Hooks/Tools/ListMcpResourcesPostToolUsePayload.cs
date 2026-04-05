using System.Collections.Generic;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ListMcpResources tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ListMcpResources tool lists available MCP resources. Your hook receives
    /// the bare array of <see cref="McpResourceInfo"/> items discovered, enabling filtering or logging.
    /// </para>
    /// </remarks>
    public sealed class ListMcpResourcesPostToolUsePayload : PostToolUseHookInput<ListMcpResourcesToolInput, List<McpResourceInfo>>
    {
    }
}
