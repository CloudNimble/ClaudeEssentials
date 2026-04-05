using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ReadMcpResource tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ReadMcpResource tool reads the content of a specific MCP resource. Your hook can inspect
    /// the server name and resource URI before the read occurs.
    /// </para>
    /// </remarks>
    public sealed class ReadMcpResourcePreToolUsePayload : PreToolUseHookInput<ReadMcpResourceToolInput>
    {
    }
}
