using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Bash tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the Bash tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "Bash".
    /// </para>
    /// <para>
    /// The Bash tool executes shell commands. Your hook receives both the command that was run
    /// and its stdout/stderr output, enabling logging, output filtering, or error detection.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="BashToolInput"/>) - The parameters given to the Bash tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="BashToolResponse"/>) - What the Bash tool produced (stdout, stderr, exit status)</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;BashPostToolUsePayload&gt;(json);
    /// if (payload.ToolResponse.Interrupted)
    ///     Console.WriteLine($"Command timed out: {payload.ToolInput.Command}");
    /// </code>
    /// </example>
    public sealed class BashPostToolUsePayload : PostToolUseHookInput<BashToolInput, BashToolResponse>
    {
    }
}
