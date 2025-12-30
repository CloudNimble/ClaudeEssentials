using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the KillShell tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the KillShell tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "KillShell".
    /// </para>
    /// <para>
    /// The KillShell tool terminates background shell processes. Your hook can inspect which shell
    /// is being terminated before the kill occurs.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="KillShellToolInput"/>) - The parameters given to the KillShell tool</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context and tool input</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PreToolUseHookOutput{TToolInput}"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;KillShellPreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Killing shell: {payload.ToolInput.ShellId}");
    /// </code>
    /// </example>
    public sealed class KillShellPreToolUsePayload : PreToolUseHookInput<KillShellToolInput>
    {
    }
}
