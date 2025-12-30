using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Glob tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the Glob tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Glob".
    /// </para>
    /// <para>
    /// The Glob tool searches for files matching a pattern. Your hook can inspect the pattern
    /// and search path before the search occurs, enabling directory access controls.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="GlobToolInput"/>) - The parameters given to the Glob tool</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context and tool input</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PreToolUseHookOutput{TToolInput}"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;GlobPreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Searching for pattern: {payload.ToolInput.Pattern}");
    /// </code>
    /// </example>
    public sealed class GlobPreToolUsePayload : PreToolUseHookInput<GlobToolInput>
    {
    }
}
