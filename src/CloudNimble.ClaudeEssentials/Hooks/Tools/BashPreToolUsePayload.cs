using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Bash tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the Bash tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Bash".
    /// </para>
    /// <para>
    /// The Bash tool executes shell commands. Your hook can inspect the command before execution,
    /// enabling security checks, command filtering, or logging of potentially dangerous operations.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="BashToolInput"/>) - The parameters given to the Bash tool</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context and tool input</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PreToolUseHookOutput{TToolInput}"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;BashPreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Executing command: {payload.ToolInput.Command}");
    /// </code>
    /// </example>
    public sealed class BashPreToolUsePayload : PreToolUseHookInput<BashToolInput>
    {
    }
}
