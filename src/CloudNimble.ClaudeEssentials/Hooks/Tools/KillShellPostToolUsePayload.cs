using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the KillShell tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the KillShell tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "KillShell".
    /// </para>
    /// <para>
    /// The KillShell tool terminates background shell processes. Your hook receives both the shell ID
    /// that was targeted and whether the termination succeeded.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="KillShellToolInput"/>) - The parameters given to the KillShell tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="KillShellToolResponse"/>) - What the KillShell tool produced (success/error status)</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;KillShellPostToolUsePayload&gt;(json);
    /// if (!payload.ToolResponse.Success)
    ///     Console.WriteLine($"Failed to kill shell: {payload.ToolResponse.Error}");
    /// </code>
    /// </example>
    public sealed class KillShellPostToolUsePayload : PostToolUseHookInput<KillShellToolInput, KillShellToolResponse>
    {
    }
}
