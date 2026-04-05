using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after an MCP tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// MCP (Model Context Protocol) tool execution results are returned as a string.
    /// Your hook receives both the dynamic input parameters and the string result.
    /// </para>
    /// </remarks>
    public sealed class McpPostToolUsePayload : PostToolUseHookInput<McpToolInput, object>
    {
    }
}
