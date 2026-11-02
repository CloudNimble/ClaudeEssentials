using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ReadMcpResourceDir tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The ReadMcpResourceDir tool lists the children of a directory resource on an MCP server. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "ReadMcpResourceDir".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeReadMcpResourceDirPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class ReadMcpResourceDirPreToolUsePayload : PreToolUseHookInput<ReadMcpResourceDirToolInput>
    {
    }

}
