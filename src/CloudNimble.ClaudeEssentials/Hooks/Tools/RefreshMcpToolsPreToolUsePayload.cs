using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the RefreshMcpTools tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The RefreshMcpTools tool re-queries the tool lists of connected MCP servers. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "RefreshMcpTools".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeRefreshMcpToolsPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class RefreshMcpToolsPreToolUsePayload : PreToolUseHookInput<RefreshMcpToolsToolInput>
    {
    }

}
