using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Write tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the Write tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "Write".
    /// </para>
    /// <para>
    /// The Write tool creates or overwrites files on the local filesystem. Your hook receives both
    /// the intended content and confirmation of what was written, enabling logging or auditing.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="WriteToolInput"/>) - The parameters given to the Write tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="WriteToolResponse"/>) - What the Write tool produced</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;WritePostToolUsePayload&gt;(json);
    /// Console.WriteLine($"Wrote {payload.ToolResponse.Type} to {payload.ToolResponse.FilePath}");
    /// </code>
    /// </example>
    public sealed class WritePostToolUsePayload : PostToolUseHookInput<WriteToolInput, WriteToolResponse>
    {
    }
}
