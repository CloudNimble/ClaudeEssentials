using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ReadMcpResource tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ReadMcpResource tool reads the content of a specific MCP resource. Your hook receives
    /// both the request parameters and the resource content, enabling filtering or logging.
    /// </para>
    /// </remarks>
    public sealed class ReadMcpResourcePostToolUsePayload : PostToolUseHookInput<ReadMcpResourceToolInput, ReadMcpResourceToolResponse>
    {
    }
}
