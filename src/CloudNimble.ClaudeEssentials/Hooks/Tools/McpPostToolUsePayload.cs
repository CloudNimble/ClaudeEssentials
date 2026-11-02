using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after an MCP tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// MCP (Model Context Protocol) tool execution results are text, content blocks, or a structured object, as modeled by <see cref="McpToolResponse"/>.
    /// Your hook receives both the dynamic input parameters and the result.
    /// </para>
    /// </remarks>
    public sealed class McpPostToolUsePayload : PostToolUseHookInput<McpToolInput, McpToolResponse>
    {
    }
}
