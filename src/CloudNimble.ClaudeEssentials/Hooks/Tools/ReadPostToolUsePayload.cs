using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Read tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the Read tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "Read".
    /// </para>
    /// <para>
    /// The Read tool reads files from the local filesystem. Your hook receives both the file path
    /// that was requested and the content that was read, enabling logging, auditing, or post-processing.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="ReadToolInput"/>) - The parameters given to the Read tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="ReadToolResponse"/>) - What the Read tool produced</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;ReadPostToolUsePayload&gt;(json);
    /// Console.WriteLine($"Read {payload.ToolResponse.File?.NumLines} lines from {payload.ToolInput.FilePath}");
    /// </code>
    /// </example>
    public sealed class ReadPostToolUsePayload : PostToolUseHookInput<ReadToolInput, ReadToolResponse>
    {
    }
}
