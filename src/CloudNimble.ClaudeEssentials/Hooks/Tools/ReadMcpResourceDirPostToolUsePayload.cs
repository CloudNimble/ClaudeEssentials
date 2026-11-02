using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ReadMcpResourceDir tool has executed.
    /// </summary>
    /// <remarks>
    /// The ReadMcpResourceDir tool lists the children of a directory resource on an MCP server. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeReadMcpResourceDirPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class ReadMcpResourceDirPostToolUsePayload : PostToolUseHookInput<ReadMcpResourceDirToolInput, ReadMcpResourceDirToolResponse>
    {
    }

}
