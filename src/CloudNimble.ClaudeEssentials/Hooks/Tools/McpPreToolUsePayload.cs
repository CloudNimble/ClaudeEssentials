using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when an MCP tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// MCP (Model Context Protocol) tools are dynamically defined by external servers.
    /// The tool input is a dynamic dictionary since the schema varies by MCP server and tool.
    /// Your hook can inspect the parameters before the MCP tool executes.
    /// </para>
    /// </remarks>
    public sealed class McpPreToolUsePayload : PreToolUseHookInput<McpToolInput>
    {
    }
}
